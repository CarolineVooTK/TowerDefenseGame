Shader "LaksaMania/toon" 
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.74,0.74,0.74,1) // light grey as ambient color
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.32,0.32,0.32,1) // super light grey as tint reflection   
        _Glossiness("Glossiness", Float) = 20 // control size of reflection
        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1) // white rim color
        _RimAmount("Rim Amount", Range(0, 1)) = 0.7
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.3
    }
    SubShader
    {
        Pass
        {

            Tags
            {
	            "LightMode" = "ForwardBase" 
	            "PassFlags" = "OnlyDirectional" // Only from main directional light
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal); // Transform the normal from object space to world space
                o.viewDir = WorldSpaceViewDir(v.vertex); // Calculate world view direction in the vertex shader
                TRANSFER_SHADOW(o) 
                return o;
            }

            
            float4 _Color;
            float4 _AmbientColor;
            float _Glossiness;
            float4 _SpecularColor;
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;

            fixed4 frag (v2f i) : SV_Target
            {
                // Calculate item's light to be distinct
                float3 normal = normalize(i.worldNormal);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float shadow = SHADOW_ATTENUATION(i); // Consider shadow from other item  
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow); // Toonify blend intensity color from light and dark    
                float4 light = lightIntensity * _LightColor0; // Calculate light color from directional light


                // Calculate Blinn-Phong specular reflection  
                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir); // Vector between the viewing direction and the light source; normalizing the result
                float NdotH = dot(normal, halfVector); // Normal of the surface and the half vector
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness); // Control the size of the specular reflection 
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity); // Toonify reflection  
                float4 specular = specularIntensitySmooth * _SpecularColor;

                // Illumination to the edges (rim lighting)
                float4 rimDot = 1 - dot(viewDir, normal); // surfaces that are facing away from the camera   
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold); // only add rim on lit surfaces, and how far rim extends   
                float rimIntensitySmooth = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity); // toonify rim effect  
                float4 rim = rimIntensitySmooth * _RimColor;

                fixed4 col = tex2D(_MainTex, i.uv);

                return _Color * col * (_AmbientColor + light + specular + rim);
            }
            ENDCG
        }

        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER" // cast shadow
    }
}
