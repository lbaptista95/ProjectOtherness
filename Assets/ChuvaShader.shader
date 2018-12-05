Shader "Custom/Chuva" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Color2 ("Color", Color) = (1,1,1,1)
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex ("Mask (RGB)", 2D) = "white" {}
		_Normal ("Normal", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_GlossMask ("Glossmask", 2D) = "white" {}
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Water ("Water", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _MainTex2;
		sampler2D _MaskTex;
		sampler2D _Normal;
		sampler2D _GlossMask;
		struct Input {
			float2 uv_MainTex;
			float2 uv_Normal;
		};
		half _Water;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _Color2;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 c2 = tex2D (_MainTex2, IN.uv_MainTex)*_Color2;
			fixed4 m = tex2D (_MaskTex, IN.uv_MainTex)*_Water;
			o.Albedo = ((c.rgb*(1-m.rgb))+(c2.rgb*m.rgb));
			o.Normal = UnpackNormal(tex2D(_Normal,IN.uv_Normal));
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness + (tex2D (_GlossMask, IN.uv_MainTex)*_Water);
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
