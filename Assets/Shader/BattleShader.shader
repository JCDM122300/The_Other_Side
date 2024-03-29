Shader "Unlit/BattleShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Transparency("Transparency", Range(0, 1)) = 0.25
        _VertexX("X Offset", Range(-1, 1)) = 0.0
        _TintColor("Tint Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
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
            float _VertexX;

            v2f vert(appdata v)
            {
                v2f o;

                v.vertex.x += _VertexX;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                   
                //If the pixel has not alpha, it doesnt render
                //Outside of the Sprite, pixels will extend to the 'quad'
                //These pixels have no alpha as they arent part of actual Sprite
                col.rgb *= col.a;
                clip(col.a-1);
                col.a = _Transparency;
                col *= _TintColor;
                return col;
            }
            ENDCG
        }
    }
}
