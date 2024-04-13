Shader "Custom/StarMeshShader"
{
    Properties
    {
        //_MainTex ("Texture", 2D) = "white" {}
        _AmbientColor("AmbientColor", Color) = (1,1,1,1)
        _DiffuseColor("DiffuseColor", Color) = (1,1,1,1)
        _LightDirection("LightDirection", Vector) = (1,1,1,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" } //불투명 : Opaque
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
                //fixed4 col = float4(1,1,0,1);

                float4 ambient = _AmbientColor;

                float lightDir = normalize(_LightDirection);
                float lightIntensity = max(dot(i.normal, lightDir),0);
                float4 diffuse = _DiffuseColor * lightIntensity * 1;

                // Phong
                float3 reflectDir = normalize(reflect(-lightDir, i.normal));
				float spec = pow(max(dot(normalize(_WorldSpaceCameraPos), reflectDir), 0), 4);
				float4 specular = spec * 0.5;

                // Blinn Phong
                //float3 halfDir = normalize(lightDir + _WorldSpaceCameraPos);
                //float spec = pow(max(0, dot(i.normal, halfDir)), 8);
                //float4 specular = spec * (1,1,1,1);

                float4 col = ambient + diffuse + specular;


                // [any] -> Cartoon
                float threshold = 0.3;
				float3 banding = floor(col / threshold);
				float3 finalIntensity = banding * threshold;
				col = float4(finalIntensity.x, finalIntensity.y, finalIntensity.z, 1.0);


                //return float4(i.normal, 1.0f);
                return col;
            }
            ENDCG
        }
    }
}
