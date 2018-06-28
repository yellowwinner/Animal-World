Shader "Custom/Seamless" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Scale("Texture Scale Multiplier", Float) = 0.1
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		float _Scale;

		struct Input {
			float2 uv_MainTex; // unused
			float3 worldNormal;
			float3 worldPos;
		};

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
