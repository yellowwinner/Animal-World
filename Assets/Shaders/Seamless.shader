// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/Seamless" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Scale("Texture Scale Multiplier", Float) = 0.1
		_Curvature("Curvature", Float) = 0.001
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert vertex:vert addshadow

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		uniform float _Curvature;
		float _Scale;

		struct Input {
			float2 uv_MainTex; // unused
			float3 worldNormal;
			float3 worldPos;
		};

		// This is where the curvature is applied
		void vert(inout appdata_full v)
		{
			// Transform the vertex coordinates from model space into world space
			float4 vv = mul(unity_ObjectToWorld, v.vertex);

			// Now adjust the coordinates to be relative to the camera position
			vv.xyz -= _WorldSpaceCameraPos.xyz;

			// Reduce the y coordinate (i.e. lower the "height") of each vertex based
			// on the square of the distance from the camera in the z axis, multiplied
			// by the chosen curvature factor
			vv = float4(0.0f, (vv.z * vv.z) * -_Curvature, 0.0f, 0.0f);

			// Now apply the offset back to the vertices in model space
			v.vertex += mul(unity_WorldToObject, vv);
		}

		void surf(Input IN, inout SurfaceOutput o) {
			// Guess correct planar map from normal. 0.5 is an arbitrary cutoff
			float2 UV;
			// NOTE: assuming no bottom-facing, otherwise use abs()
			if (IN.worldNormal.y>0.5) UV = IN.worldPos.xz; // top
			else if (abs(IN.worldNormal.x)>0.5) UV = IN.worldPos.yz; // side
			else UV = IN.worldPos.xy; // front

			//half4 c = tex2D(_MainTex,  UV* _Scale);
			half4 c = tex2D(_MainTex, UV * _Scale);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}

		ENDCG
	}

	Fallback "Diffuse"
}
