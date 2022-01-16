Shader "Hidden/GaussianBlur"
{
    Properties
    {
        [HideInInspector] _MainTex ("Texture", 2D) = "white" {}
        _samples("Blur Samples", Range(10, 100)) = 10
        _blurSize("Blur Size", Range(0, 0.1)) = 0
    }
    SubShader
    {
        // No culling or depth
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _samples;
            float _blurSize;

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

            // The Vertex function
            v2f vert (appdata v)
            {
                v2f o;


                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            // The Fragment function
            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = 0;

                for(float index_x = 0; index_x < _samples; index_x++)
                {
                    float2 uv = i.uv + float2(0, (index_x / _samples) * _blurSize);
                    col += tex2D(_MainTex, uv);
                }
                
                col = col / _samples;

                return col;
            }
            ENDCG
        }
    }
}
