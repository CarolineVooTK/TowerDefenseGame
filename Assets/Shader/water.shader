Shader "LaksaMania/water"
{
    Properties
    {
		_Color("Color", Color) = (1,1,1,1)

		// Water color    
		_WaterColor("Water Color", Color) = (0.325, 0.807, 0.971, 1)

		// Noise texture used to generate waves, Perlin noise
		_SurfaceNoise("Surface Noise", 2D) = "white" {}

		// Speed noise will move 
		_SurfaceNoiseScroll("Surface Noise Scroll Amount", Vector) = (0, 0.5, 0, 0)

		// Values in the noise texture above this cutoff are rendered on the surface  
		_SurfaceNoiseCutoff("Surface Noise Cutoff", Range(0, 1)) = 0.7

		// Red and green channels of this texture are used to offset the noise texture to create distortion 
		_SurfaceDistortion("Surface Distortion", 2D) = "white" {}	

		// Multiplies the distortion by a value
		_SurfaceDistortionAmount("Surface Distortion Amount", Range(0, 1)) = 0.06
    }
    SubShader
    {
		Tags
		{
			
		}

        Pass
        {

            CGPROGRAM
			#define SMOOTHSTEP_AA 0.01

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;	
				float2 noiseUV : TEXCOORD0;
				float2 distortUV : TEXCOORD1;
				float4 screenPosition : TEXCOORD2;
            };

			sampler2D _SurfaceNoise;
			float4 _SurfaceNoise_ST;

			sampler2D _SurfaceDistortion;
			float4 _SurfaceDistortion_ST;

            v2f vert (appdata v)
            {
                v2f o;

                o.vertex = UnityObjectToClipPos(v.vertex);
				o.screenPosition = ComputeScreenPos(o.vertex);
				o.distortUV = TRANSFORM_TEX(v.uv, _SurfaceDistortion);
				o.noiseUV = TRANSFORM_TEX(v.uv, _SurfaceNoise);

                return o;
            }

			float4 _WaterColor;
			float4 _FoamColor;

			float _SurfaceNoiseCutoff;
			float _SurfaceDistortionAmount;

			float2 _SurfaceNoiseScroll;

			sampler2D _CameraDepthTexture;

            float4 frag (v2f i) : SV_Target
            {
				// Set variables
				float4 waterColor = _WaterColor;
				float surfaceNoiseCutoff = _SurfaceNoiseCutoff;

				// Implement water waves  
				float2 distortSample = (tex2D(_SurfaceDistortion, i.distortUV).xy * 2 - 1) * _SurfaceDistortionAmount; // Implement distortion texture  
				float2 noiseUV = float2((i.noiseUV.x + _Time.y * _SurfaceNoiseScroll.x) + distortSample.x, (i.noiseUV.y + _Time.y * _SurfaceNoiseScroll.y) + distortSample.y); // Give animation of movement   
				float surfaceNoiseSample = tex2D(_SurfaceNoise, noiseUV).r; // Implement noise texture  
				float surfaceNoise = smoothstep(surfaceNoiseCutoff - SMOOTHSTEP_AA, surfaceNoiseCutoff + SMOOTHSTEP_AA, surfaceNoiseSample); // Use smooth step for water toony effect

				// Combine color of surface andwater
				return surfaceNoise + waterColor;
            }
            ENDCG
        }
    }
}
