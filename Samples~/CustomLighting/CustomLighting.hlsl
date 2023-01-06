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
	float rimThreshold;
	float ambientOcclusion;

	float3 bakedGI;
	float4 shadowMask;
	float fogFactor;

	UnityTexture2D diffuseQuantizationTexture;
	UnityTexture2D specularQuantizationTexture;
	UnityTexture2D rimQuantizationTexture;
	UnitySamplerState SS;
};

struct CustomLightingResult {
	float3 color;

	float diffuse;
	float specular1;
	float specular2;
	float rim1;
	float rim2;
};

float GetSmoothnessPower(float rawSmoothness) {
	return exp2(10 * rawSmoothness + 1);
}

float GetLightIntensity(float3 lightColor) {
	return max(lightColor.x, max(lightColor.y, lightColor.z));
}

void CustomLightHandlingInternal(float3 lightColor, float radiance, float3 lightDirection, CustomLightingData d, inout CustomLightingResult r) {

	/*float diffuse = saturate(dot(d.normalWS, lightDirection));
	diffuse = SAMPLE_TEXTURE2D(d.diffuseQuantizationTexture, d.SS, float2(diffuse, 0)).x;

	float specularDot = saturate(dot(d.normalWS, normalize(lightDirection + d.viewDirectionWS)));
	float specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse;
	specular = SAMPLE_TEXTURE2D(d.specularQuantizationTexture, d.SS, float2(specular, 0)).x;

	float rim = (1 - dot(d.viewDirectionWS, d.normalWS)) * pow(diffuse, d.rimThreshold);
	rim = SAMPLE_TEXTURE2D(d.rimQuantizationTexture, d.SS, float2(rim, 0)).x;


	r.color += d.albedo * lightColor * radiance * (diffuse + max(specular, rim));

	float intensity = dot(d.albedo, float3(0.2126729, 0.7151522, 0.0721750)) * dot(lightColor, float3(0.2126729, 0.7151522, 0.0721750)) * radiance;

	r.diffuse += diffuse * intensity;
	r.specular1 += specular * intensity;
	r.specular2 += (rim <= specular) ? specular * intensity : 0;
	r.rim1 += rim * intensity;
	r.rim2 += (rim > specular) ? rim * intensity : 0;*/


	float intensity = /*dot(d.albedo, float3(0.2126729, 0.7151522, 0.0721750)) * dot(lightColor, float3(0.2126729, 0.7151522, 0.0721750)) * */ radiance;

	float diffuse = saturate(dot(d.normalWS, lightDirection)) * intensity;
	diffuse = SAMPLE_TEXTURE2D(d.diffuseQuantizationTexture, d.SS, float2(diffuse, 0)).x;

	float specularDot = saturate(dot(d.normalWS, normalize(lightDirection + d.viewDirectionWS)));
	float specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse;
	specular = SAMPLE_TEXTURE2D(d.specularQuantizationTexture, d.SS, float2(specular, 0)).x;

	float rim = (1 - dot(d.viewDirectionWS, d.normalWS)) * pow(diffuse, d.rimThreshold);
	rim = SAMPLE_TEXTURE2D(d.rimQuantizationTexture, d.SS, float2(rim, 0)).x;


	r.color += /* d.albedo * */lightColor * (diffuse + max(specular, rim));


	r.diffuse += diffuse * intensity;
	r.specular1 += specular * intensity;
	r.specular2 += (rim <= specular) ? specular * intensity : 0;
	r.rim1 += rim * intensity;
	r.rim2 += (rim > specular) ? rim * intensity : 0;


	// float diffuse = saturate(dot(d.normalWS, lightDirection)) * radiance;
	// diffuse = SAMPLE_TEXTURE2D(d.diffuseQuantizationTexture, d.SS, float2(diffuse, 0)).x;
	// 
	// float specularDot = saturate(dot(d.normalWS, normalize(lightDirection + d.viewDirectionWS)))/* * radiance*/;
	// float specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse;
	// specular = SAMPLE_TEXTURE2D(d.specularQuantizationTexture, d.SS, float2(specular, 0)).x;
	// 
	// float rim = (1 - dot(d.viewDirectionWS, d.normalWS)) * pow(diffuse, d.rimThreshold)/* * radiance*/;
	// rim = SAMPLE_TEXTURE2D(d.rimQuantizationTexture, d.SS, float2(rim, 0)).x;
	// 
	// r.color += d.albedo * lightColor/* * radiance*/ * (diffuse + max(specular, rim));
	// 
	// float intensity = radiance;
	// 
	// r.diffuse += diffuse;
	// r.specular += specular;
	// r.rim1 += rim;
	// r.rim2 += (rim > specular) ? rim : 0;

	/*float diffuse = saturate(dot(d.normalWS, lightDirection)) * radiance;
	diffuse = SAMPLE_TEXTURE2D(d.diffuseQuantizationTexture, d.SS, float2(diffuse, 0)).x;

	float specularDot = saturate(dot(d.normalWS, normalize(lightDirection + d.viewDirectionWS)));
	float specular = pow(specularDot, GetSmoothnessPower(d.smoothness)) * diffuse * radiance;
	specular = SAMPLE_TEXTURE2D(d.specularQuantizationTexture, d.SS, float2(specular, 0)).x;

	float rim = (1 - dot(d.viewDirectionWS, d.normalWS)) * pow(diffuse, d.rimThreshold) * radiance;
	rim = SAMPLE_TEXTURE2D(d.rimQuantizationTexture, d.SS, float2(rim, 0)).x;

	r.color += d.albedo * lightColor * (diffuse + max(specular, rim));*/
}

#ifndef SHADERGRAPH_PREVIEW
void CustomGlobalIllumination(CustomLightingData d, inout CustomLightingResult r) {
	//float indirectDiffuse = d.ambientOcclusion;
	//indirectDiffuse = SAMPLE_TEXTURE2D(d.diffuseQuantizationTexture, d.SS, float2(indirectDiffuse, 0)).x;
	

	/*float3 reflectVector = reflect(-d.viewDirectionWS, d.normalWS);
	float fresnel = Pow4(1 - saturate(dot(d.viewDirectionWS, d.normalWS)));
	float3 indirectSpecular = GlossyEnvironmentReflection(reflectVector,
		RoughnessToPerceptualRoughness(1 - d.smoothness),
		d.ambientOcclusion) * fresnel;*/
	
	//r.color += indirectDiffuse * d.albedo * d.bakedGI /* + indirectSpecular*/;

	float indirectDiffuse = d.ambientOcclusion * d.albedo * d.bakedGI;

	r.color += indirectDiffuse/* + indirectSpecular*/;

	r.diffuse += indirectDiffuse;
}

void CustomLightHandling(CustomLightingData d, Light light, inout CustomLightingResult r) {
	float radiance = light.shadowAttenuation * light.distanceAttenuation;
	CustomLightHandlingInternal(light.color, radiance, light.direction, d, r);
}
#endif

void CalculateCustomLighting(CustomLightingData d, inout CustomLightingResult r) {

#ifdef SHADERGRAPH_PREVIEW
	CustomLightHandlingInternal(float3(1, 1, 1), 1, float3(0.5, 0.5, 0), d, r);
#else
	Light mainLight = GetMainLight(d.shadowCoord, d.positionWS, d.shadowMask);

	MixRealtimeAndBakedGI(mainLight, d.normalWS, d.bakedGI);
	CustomGlobalIllumination(d, r);

	CustomLightHandling(d, mainLight, r);

#ifdef _ADDITIONAL_LIGHTS
	uint AdditionalLightsCount = GetAdditionalLightsCount();
	for (uint lightIndex = 0; lightIndex < AdditionalLightsCount; lightIndex++) {
		Light light = GetAdditionalLight(lightIndex, d.positionWS, d.shadowMask);
		CustomLightHandling(d, light, r);
	}
#endif

	r.color = MixFog(r.color, d.fogFactor);

#endif
}

void CalculateCustomLighting_float(float3 Position, float3 Normal, float3 ViewDirection,
	float3 Albedo, float Smoothness, float RimThreshold, float AmbientOcclusion,
	float2 LightmapUV, 
	UnityTexture2D DiffuseQuantizationTexture,
	UnityTexture2D SpecularQuantizationTexture,
	UnityTexture2D RimQuantizationTexture,
	UnitySamplerState SS,
	out float3 Color,
	out float Diffuse, out float Specular1, out float Specular2, out float Rim1, out float Rim2) {

	CustomLightingData d;

	d.positionWS = Position;
	d.normalWS = Normal;
	d.viewDirectionWS = ViewDirection;
	
	d.albedo = Albedo;
	d.rimThreshold = RimThreshold;
	d.smoothness = Smoothness;
	d.ambientOcclusion = AmbientOcclusion;

	d.diffuseQuantizationTexture = DiffuseQuantizationTexture;
	d.specularQuantizationTexture = SpecularQuantizationTexture;
	d.rimQuantizationTexture = RimQuantizationTexture;
	d.SS = SS;


	CustomLightingResult r;
	r.color = float3(0, 0, 0);

	r.diffuse = 0;
	r.specular1 = 0;
	r.specular2 = 0;
	r.rim1 = 0;
	r.rim2 = 0;


#ifdef SHADERGRAPH_PREVIEW
	d.shadowCoord = 0;
	d.bakedGI = 0;
	d.shadowMask = 0;
	d.fogFactor = 0;
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

	d.shadowMask = SAMPLE_SHADOWMASK(lightmapUV);

	d.fogFactor = ComputeFogFactor(positionCS.z);
#endif

	CalculateCustomLighting(d, r);

	Color = r.color;

	Diffuse = r.diffuse;
	Specular1 = r.specular1;
	Specular2 = r.specular2;
	Rim1 = r.rim1;
	Rim2 = r.rim2;
}

#endif