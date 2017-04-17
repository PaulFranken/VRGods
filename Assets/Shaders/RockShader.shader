Shader "Custom/RockShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_MossTex("Moss or Snow (RGBA)", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
#pragma target 3.0

		sampler2D _MainTex;
	sampler2D _MossTex;

	struct Input {
		float2 uv_MainTex;
		float2 uv_MossTex;
		float3 VertexDirection;
	};

	half _Glossiness;
	half _Metallic;
	fixed4 _Color;

	void vert(inout appdata_full v, out Input o) {
		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.VertexDirection = v.normal;
	}

	void surf(Input IN, inout SurfaceOutputStandard o) {
		// Albedo comes from a texture tinted by color
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		//Now grab the moss texture
		fixed4 m = tex2D(_MossTex, IN.uv_MossTex) * _Color;
		//now lets make the texture!
		o.Albedo = (m.rgb *m.a) + (c.rgb * (1 - m.a));


		// Metallic and smoothness come from slider variables
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}