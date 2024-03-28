Shader "Unlit/ScrollingTexture"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
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
                v.vertex.x += _VertexX;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv.y += _Time.y* _ScrollSpeed * _ScrollDir;
                i.uv = frac(i.uv);

                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.a = _Transparency;
                col *= _TintColor;
                return col;
            }
            ENDCG
        }
    }
}
