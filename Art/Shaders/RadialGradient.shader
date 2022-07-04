Shader "Custom/RadialGradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
        _NoiseForce("Noise Force", Range(0, 500)) = 10
        _NoiseScale("Noise Scale", Vector) = (10, 10, 0, 0)
        _ColorFirst ("Color First", Color) = (1, 0, 1, 1)
        _ColorSecond ("Color Second", Color) = (1, 1, 0, 1)
        _ColorThird ("Color Third", Color) = (1, 0, 1, 1)
        _ColorFourth ("Color Fourth", Color) = (1, 1, 0, 1)
        _RadiusMultiplier ("Radius", Range(0, 5)) = 1
        _GradientPivot ("Position", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }

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
	        sampler2D _NoiseTex;
	        float _NoiseForce;
	        float2 _NoiseScale;
            float4 _ColorFirst;
            float4 _ColorSecond;
            float4 _ColorThird;
            float4 _ColorFourth;
            float _RadiusMultiplier;
            float4 _GradientPivot;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float3 getNoise(float2 uv)
            {
                float2 scale = 10;
                float force = _NoiseForce;
                return tex2D(_NoiseTex, uv * _NoiseScale)/force;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 pixelPoint = fixed2(i.uv.x, i.uv.y);
                float xDist = pow(pixelPoint.x - _GradientPivot.x, 2);
                float yDist = pow(pixelPoint.y - _GradientPivot.y, 2);
                float dist = sqrt(xDist + yDist) / _RadiusMultiplier;
                dist = clamp(dist, 0, 1);
                fixed4 gr3_4 = lerp(_ColorThird, _ColorFourth, dist);
                fixed4 gr2_34 = lerp(_ColorSecond, gr3_4, dist);
                fixed4 col = lerp(_ColorFirst, gr2_34, dist);
                col.rgb += getNoise(i.uv);
                return col;
            }
            ENDCG
        }
    }
}