#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

#ifndef SHADERGRAPH_PREVIEW
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
#if (SHADERPASS != SHADERPASS_FORWARD)
#undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
#endif
#endif

struct CustomLightingData {
	float3 positionWS;
	float3 normalWS;
	float3 viewDirectionWS;
	float4 shadowCoord;

	float3 albedo;
	float smoothness;
	float ambientOcclusion;

	float3 bakedGI;

	float3 specular;
	float3 diffuse;
};

float GetSmoothnessPower(float rawSmoothness) {
	return exp2(10 * rawSmoothness + 1);
}

#ifndef SHADERGRAPH_PREVIEW
void CustomGlobalIllumination(inout CustomLightingData d) {
	float3 indirectDiffuse = d.albedo * d.bakedGI * d.ambientOcclusion;

	float3 reflectVector = reflect(-d.viewDirectionWS, d.normalWS);
	float fresnel = Pow4(1 - saturate(dot(d.viewDirectionWS, d.normalWS)));
	float3 indirectSpecular = GlossyEnvironmentReflection(reflectVector,
		RoughnessToPerceptualRoughness(1 - d.smoothness),
		d.ambientOcclusion) * fresnel;

	d.diffuse = indirectDiffuse;
	d.specular = indirectSpecular;
}

void CustomLightHandling(inout CustomLightingData d, Light light) {
	float3 radiance = light.color * (light.shadowAttenuation * light.distanceAttenuation);

	float3 diffuse = saturate(dot(d.normalWS, light.direction));
	float3 specularDot = saturate(dot(d.normalWS, normalize(light.direction + d.viewDirectionWS)));
	float3 specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse;

	d.diffuse += (d.albedo * radiance * diffuse);
	d.specular += (d.albedo * radiance * specular);
}
#endif

void CalculateCustomLighting(inout CustomLightingData d) {
#ifdef SHADERGRAPH_PREVIEW
	float3 lightDir = float3(0.5, 0.5, 0);
	d.diffuse = saturate(dot(d.normalWS, lightDir)) * d.albedo;
	d.specular = pow(saturate(dot(d.normalWS, normalize(d.viewDirectionWS + lightDir))), GetSmoothnessPower(d.smoothness)) * d.albedo;
#else
	Light mainLight = GetMainLight(d.shadowCoord, d.positionWS, 1);

	MixRealtimeAndBakedGI(mainLight, d.normalWS, d.bakedGI);
	CustomGlobalIllumination(d);

	CustomLightHandling(d, mainLight);

	#ifdef _ADDITIONAL_LIGHTS
		uint numAdditionalLights = GetAdditionalLightsCount();
		for (uint lightI = 0; lightI < numAdditionalLights; lightI++) {
			Light light = GetAdditionalLight(lightI, d.positionWS, 1);
			CustomLightHandling(d, light);
		}
	#endif
#endif
}

void CalculateCustomLighting_float(float3 Position, float3 Normal, float3 ViewDirection,
	float3 Albedo, float Smoothness,  float AmbientOcclusion,
	float2 LightmapUV, 
	out float3 DiffuseColor, out float3 SpecularColor) {

	CustomLightingData d;
	d.positionWS = Position;
	d.normalWS = Normal;
	d.viewDirectionWS = ViewDirection;
	d.albedo = Albedo;
	d.smoothness = Smoothness;
	d.ambientOcclusion = AmbientOcclusion;

#ifdef SHADERGRAPH_PREVIEW
	d.shadowCoord = 0;
	d.bakedGI = 0;
#else
	float4 positionCS = TransformWorldToHClip(Position);
	#if SHADOWS_SCREEN
		d.shadowCoord = ComputeScreenPos(positionCS);
	#else
		d.shadowCoord = TransformWorldToShadowCoord(Position);
	#endif

		float3 lightmapUV;
		OUTPUT_LIGHTMAP_UV(LightmapUV, unity_LightmapST, lightmapUV);

		float3 vertexSH;
		OUTPUT_SH(Normal, vertexSH);

		d.bakedGI = SAMPLE_GI(lightmapUV, vertexSH, Normal);
#endif

	CalculateCustomLighting(d);

	DiffuseColor = d.diffuse;
	SpecularColor = d.specular;
}

#endif