Shader"My/flatTextureShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TopCol("Top colour", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

float4 _TopCol, _BaseCol;
sampler2D _MainTex;

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};
            
struct v2f
{
    float4 vertex : SV_POSITION;
    float3 worldPos : TEXCOORD0;
    float2 uv : TEXCOORD1;
};

v2f vert(appdata v)
{
    v2f o;

    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    o.worldPos = mul(unity_ObjectToWorld, v.vertex);

    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float3 x = ddx(i.worldPos);
    float3 y = ddy(i.worldPos);

    fixed4 textureColor = tex2D(_MainTex, i.uv);

    float3 norm = -normalize(cross(x, y));
    float l = saturate(dot(norm, float3(0, 1, 0)));
    fixed4 col = lerp(textureColor, _TopCol, l);

    return col;
}
            ENDCG
        }
    }
}