Shader "Custom/GrayscaleShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue" = "Overlay"  "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            ZTest Always Cull Off ZWrite Off
            Fog { Mode Off }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // Convert to grayscale using luminance calculation
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114));
                return fixed4(gray, gray, gray, col.a);  // Return grayscale output
            }
            ENDCG
        }
    }
}
