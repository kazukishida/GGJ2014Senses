Shader "unityCookie/Rim Lighting" {
    Properties {
        _MainTex ("Diffuse Texture", 2D) = "white" {}
        _BumpTex ("Normal Map", 2D) = "bump" {}
		_Color ("Overlay Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (1,1,1,1) //The color tint of our rim light
        _RimPower ("Rim Power", Range(0.1,10)) = 3.0 //The power of our rim, note that we start from 0.1 as we don't really want 0.
    }
    Subshader {
        Tags { "RenderType" = "Opaque"}
        CGPROGRAM
        #pragma surface surf Lambert
		uniform float4 _Color;
        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpTex;
            float3 viewDir; //We want to get the viewing angle
        };
        sampler2D _MainTex;
        sampler2D _BumpTex;
        float4 _RimColor;
        float _RimPower;
        void surf (Input IN, inout SurfaceOutput o) {
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 c = tex * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal (tex2D (_BumpTex, IN.uv_BumpTex));
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal)); // we take the view angle and place it in a 0-1 half value
            o.Emission = _RimColor.rgb * pow (rim, _RimPower); // we take our rim output (0-1) and take it to the power of our rim power.
        }
        ENDCG
    }
    Fallback "Diffuse"
}