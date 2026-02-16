Shader "Unlit/BcakGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        //速度
        _Speed("Speed",Float) = 0.5
      
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
            // make fog work
            #pragma multi_compile_fog

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
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
              //中心を基準に座標をずらす
              float2 center = float2(0.5,0.5);
              float2 uv = i.uv - center;
             
              //時間でスケーリング→中心に吸い込まれるように見える
              float scale = 1.0 - _Time.y * _Speed;
              uv *= scale;

              //中心に戻す
              uv += center;

              return tex2D(_MainTex,uv);

            }
            ENDCG
        }
    }
}
