Shader "Unlit/ScrollingTexture"
{
    Properties
    {
        _MainTex("BaseTexture", 2D) = "white" {}
        _VertexX("X Offset", Range(-1, 1)) = 0.0
        _ScrollDir("Scroll Direction", Int) = -1
        _ScrollSpeed("Scroll Speed", Range(0, 5.0)) = 1.0
        _Transparency("Transparency", Range(0, 1)) = 0.25
        _TintColor("Tint Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        LOD 100
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }

        ZWrite Off
        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
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
            float4 _MainTex_ST;
            float4 _TintColor;
            float _Transparency;
            float _ScrollSpeed;
            int _ScrollDir;
            float _VertexX;

            v2f vert (appdata v)
            {
                v2f o;
                
                //Hit Offset
                v.vertex.x += _VertexX;

                //Vertex to Clip Space
                o.vertex = UnityObjectToClipPos(v.vertex);

                //UV to world space
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                //TODO: Find a away to clip uv if outside of sprite shape

                //Scrolling UV
                float2 baseUV = i.uv;
                i.uv.y += _Time.y * _ScrollSpeed * _ScrollDir;

                //Loops the texture | DO BEFORE SAMPLING
                i.uv = frac(i.uv);
            

                //Sample the texture
                fixed4 baseCol = tex2D(_MainTex, baseUV); //Base uv
                fixed4 scrollCol = tex2D(_MainTex, i.uv); //Scrolling uv

                //Removes parts of sprite that have no alpha
                clip(scrollCol.a - 1);
                
                //ALPHA MASKING YEAHH
                if (baseCol.a < 1)
                {
                    discard;
                }
                
                // Apply scrolling texture only within the outline of the sprite
                 
                //Transparency and Color
                scrollCol.a = _Transparency;
                scrollCol *= _TintColor;


                return scrollCol;
            }
            ENDCG
        }
    }
}
