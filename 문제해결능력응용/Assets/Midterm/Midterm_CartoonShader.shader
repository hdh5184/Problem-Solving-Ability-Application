Shader "Custom/Midterm_CartoonShader"
{
    Properties
    {
        _AmbientColor("AmbientColor", Color) = (1,1,1,1)
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (1,1,1,0)
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            float4 _AmbientColor;
            float4 _DiffuseColor;
            float4 _LightDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 ambient = _AmbientColor;

                float lightDir = normalize(_LightDirection);
                float lightIntensity = max(dot(i.normal, lightDir),0);
                float4 diffuse = _DiffuseColor * lightIntensity * 1;

                // Phong
                float3 reflectDir = normalize(reflect(-lightDir, i.normal));
				float spec = pow(max(dot(normalize(_WorldSpaceCameraPos), reflectDir), 0), 4);
				float4 specular = spec * 0.5;

                float4 col = ambient + diffuse + specular;


                //Cartoon
                float threshold = 0.2;
				float3 banding = floor(col / threshold);
				float3 finalIntensity = banding * threshold;
				col = float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0);

                return col;
            }
            ENDCG
        }
    }
}
