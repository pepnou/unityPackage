#ifndef SOBELOUTLINES_INCLUDED
#define SOBELOUTLINES_INCLUDED

#include "DecodeDepthNormals.hlsl"

TEXTURE2D(_DepthNormalsTexture);
SAMPLER(sampler_DepthNormalsTexture);

static float2 sobelSamplePoints[9] = {
	float2(-1,1), float2(0,1), float2(1,1),
	float2(-1,0), float2(0,0), float2(1,0),
	float2(-1,-1), float2(0,-1), float2(1,-1)
};

static float sobelXMatrix[9] = {
	1, 0, -1,
	2, 0, -2,
	1, 0, -1
};

static float sobelYMatrix[9] = {
	1, 2, 1,
	0, 0, 0,
	-1, -2, -1
};

void DepthSobel_float(float2 UV, float Thicknes, out float Out) {
	float2 sobel = 0;

	[unroll] for (int i = 0; i < 9; i++) {
		float depth = SHADERGRAPH_SAMPLE_SCENE_DEPTH(UV + sobelSamplePoints[i] * Thicknes);
		sobel += depth * float2(sobelXMatrix[i], sobelYMatrix[i]);
	}

	Out = length(sobel);
}

void ColorSobel_float(float2 UV, float Thicknes, out float Out) {

	float2 sobelR = 0;
	float2 sobelG = 0;
	float2 sobelB = 0;

	[unroll] for (int i = 0; i < 9; i++) {
		float3 rgb = SHADERGRAPH_SAMPLE_SCENE_COLOR(UV + sobelSamplePoints[i] * Thicknes);

		float2 kernel = float2(sobelXMatrix[i], sobelYMatrix[i]);

		sobelR += rgb.r * kernel;
		sobelG += rgb.g * kernel;
		sobelB += rgb.b * kernel;
	}

	Out = max( length(sobelR), max(length(sobelG), length(sobelB)));
}

void GetDepthAndNormal(float2 uv, out float depth, out float3 normal) {
	float4 coded = SAMPLE_TEXTURE2D(_DepthNormalsTexture, sampler_DepthNormalsTexture, uv);
	DecodeDepthNormal(coded, depth, normal);
}

void CalculateDepthNormal_float(float2 UV, out float Depth, out float3 Normal) {
	GetDepthAndNormal(UV, Depth, Normal);
	Normal = Normal * 2 - 1;
}

void NormalSobel_float(float2 UV, float Thicknes, out float Out) {

	float2 sobelX = 0;
	float2 sobelY = 0;
	float2 sobelZ = 0;

	[unroll] for (int i = 0; i < 9; i++) {
		float depth;
		float3 normal;
		GetDepthAndNormal(UV + sobelSamplePoints[i] * Thicknes, depth, normal);

		float2 kernel = float2(sobelXMatrix[i], sobelYMatrix[i]);

		sobelX += normal.x * kernel;
		sobelY += normal.y * kernel;
		sobelZ += normal.z * kernel;
	}

	Out = max(length(sobelX), max(length(sobelY), length(sobelZ)));
}

void ViewDirectionFromScreenUV_float(float2 In, out float3 Out) {
	float2 p11_22 = float2(unity_CameraProjection._11, unity_CameraProjection._22);
	Out = -normalize(float3((In * 2 - 1) / p11_22, -1));
}

#endif