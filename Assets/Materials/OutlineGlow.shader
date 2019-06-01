Shader "OutlineGlow" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Glow ("Intensity", Range(0, 3)) = 1
    }
    SubShader {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        LOD 100
        Cull Off
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                sampler2D _MainTex;
                half4 _MainTex_ST;
                fixed4 _Color;
                half _Glow;

                struct vertIn {
                    float4 pos : POSITION;
                    half2 tex : TEXCOORD0;
                };

                struct v2f {
                    float4 pos : SV_POSITION;
                    half2 tex : TEXCOORD0;
                };

                v2f vert (vertIn v) {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.pos);
                    o.tex = v.tex * _MainTex_ST.xy + _MainTex_ST.zw;
                    return o;
                }

				float4 _MainTex_TexelSize;

                fixed4 frag (v2f f) : SV_Target {
                    fixed4 col = tex2D(_MainTex, f.tex);
					half4 outlineC = _Color;
					col *= col.a;
                    // col *= _Glow;
					outlineC.a *= ceil(col.a);
					outlineC.rgb *= outlineC.a;
					outlineC *= _Glow;

					fixed upAlpha = tex2D(_MainTex, f.tex + fixed2(0, _MainTex_TexelSize.y)).a;
					fixed downAlpha	= tex2D(_MainTex, f.tex - fixed2(0, _MainTex_TexelSize.y)).a;
					fixed rightAlpha = tex2D(_MainTex, f.tex + fixed2(_MainTex_TexelSize.x, 0)).a;
					fixed leftAlpha = tex2D(_MainTex, f.tex - fixed2( _MainTex_TexelSize.x, 0)).a;

                    return lerp(outlineC, col, ceil(upAlpha * downAlpha* rightAlpha * leftAlpha));
                }
            ENDCG
        }
    }
}