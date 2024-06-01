Shader "Custom/YellowPhongShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "red" {}
        _SpecularColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(1, 256)) = 32
        _LightColor ("Light Color", Color) = (1,1,1,1)
        _LightDirection ("Light Direction", Vector) = (-0.5, -1, 0.5)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _SpecularColor;
            float _Shininess;
            fixed4 _LightColor;
            float3 _LightDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Normal 벡터 반전
                o.normal = mul((float3x3)UNITY_MATRIX_IT_MV, -v.normal);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 norm = normalize(i.normal);
                // 빛의 방향을 반전
                float3 lightDir = normalize(_LightDirection);
                // 뷰 방향을 카메라에서 표면으로
                float3 viewDir = normalize(i.vertex.xyz - _WorldSpaceCameraPos);

                // 확산 반사 계산
                float diff = max(dot(norm, lightDir), 0.0);

                // 스펙큘러 반사 계산
                float3 reflectDir = reflect(-lightDir, norm);
                float spec = pow(max(dot(viewDir, reflectDir), 0.0), _Shininess);

                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 ambientColor = 0.3 * texColor; // 환경광 추가
                fixed4 diffuseColor = diff * texColor * _LightColor;
                fixed4 specularColor = spec * _SpecularColor;
                
                float3 result = (ambientColor + diffuseColor + specularColor);
    
                float threshold = 0.4;
                float3 banding = floor(result / threshold) + 0;
                float3 finalIntensity = banding * threshold;
                
                return float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0); // 결과 색상 반환
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}