Shader "Unlit/YellowShader"
{
    Properties
    {
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
           
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD2;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float3 viewDir : TEXCOORD2;
            };

            float4 _DiffuseColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.viewDir = normalize(UnityWorldSpaceViewDir(v.vertex));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float ambientStrength = 0.4;
                float3 ambient = ambientStrength * _DiffuseColor;

                // Light direction
                float3 lightDir = normalize(UnityWorldSpaceLightDir(i.vertex));

                // Diffuse reflection
                float diff = max(dot(i.normal, lightDir), 0.0);
                float3 diffuse = diff * _DiffuseColor;

                // Specular reflection
                float3 reflectDir = reflect(-lightDir, i.normal);
                float spec = pow(max(dot(i.viewDir, reflectDir), 0.0), 32.0);
                float3 specular = float3(0.5, 0.5, 0.5) * spec;

                // Combine ambient, diffuse, and specular
                float3 result = (ambient + diffuse + specular);

                float threshold = 0.5;
				float3 banding = floor(result / threshold);
				float3 finalIntensity = banding * threshold;

                return float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0);
            }
            ENDCG
        }
    }
}