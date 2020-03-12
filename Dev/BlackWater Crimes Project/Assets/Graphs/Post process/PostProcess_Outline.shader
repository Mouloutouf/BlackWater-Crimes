// le nom du shader est son chemin d'accès dans l'inspector d'un material
Shader "PostProcess_Outline"
{
    Properties
    {
        // on déclare ici les variables qui seront tweakables dans l'inspector
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _NormalMult ("Normal Outline Multiplier", Range(0,10)) = 1
        _NormalBias ("Normal Outline Bias", Range(1,10)) = 1
        _DepthMult ("Depth Outline Multiplier", Range(0,10)) = 1
        _DepthBias ("Depth Outline Bias", Range(1,10)) = 1
    }
    SubShader
    {
        
        Cull Off
        ZWrite Off 
        ZTest Always

        Pass
        {
            CGPROGRAM
            // on annonce le nom des fonctions de traitement de la géométrie puis de l'image flat
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            // on va chercher le fichier dans l'install d'unity qui contient les fonctions préexistantes
            #include "UnityCG.cginc"

            // struct contenant les infos de géométrie initiale
            struct appdata
            {
                // les mots en capslock = "semantics", ce sont les infos qui permettent à Unity d'auto-initialiser la variable avec les bonnes infos
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

            };

            // struct contenant les données de l'objet flattened
            struct v2f{
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
            };

            // le type sample2D, en HLSL, ça veut dire "texture" en C#
            sampler2D _MainTex;
            sampler2D _CameraDepthNormalsTexture;
            float4 _CameraDepthNormalsTexture_TexelSize;

            //Déclaration de variable pré-déclarée en C
            // Même nom que dans sa déclaration en shaderlab
            float4 _OutlineColor;
            float _NormalMult;
            float _NormalBias;
            float _DepthMult;
            float _DepthBias;


            // Traitement géométrie
            v2f vert (appdata v)
            {
                // plein de fonctions mathématiques à connaître : lerp et abs en sont deux exemples,
                // le reste est sur la doc de nvidia (doc officielle du HLSL)
                v2f o;

                //convert the vertex positions from object space to clip space so they can be rendered
                o.position = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            void Compare(inout float depthOutline, inout float normalOutline, 
                 float baseDepth, float3 baseNormal, float2 uv, float2 offset){
                //read neighbor pixel
                float4 neighborDepthnormal = tex2D(_CameraDepthNormalsTexture, 
                  uv + _CameraDepthNormalsTexture_TexelSize.xy * offset);
                float3 neighborNormal;
                float neighborDepth;
                DecodeDepthNormal(neighborDepthnormal, neighborDepth, neighborNormal);
                neighborDepth = neighborDepth * _ProjectionParams.z;

                float depthDifference = baseDepth - neighborDepth;
                depthOutline = depthOutline + depthDifference;

                float3 normalDifference = baseNormal - neighborNormal;
                normalDifference = normalDifference.r + normalDifference.g + normalDifference.b;
                normalOutline = normalOutline + normalDifference;
            }

            // Traitement image flat
            fixed4 frag (v2f i) : SV_Target
            {
                //read depthnormal
                float4 depthnormal = tex2D(_CameraDepthNormalsTexture, i.uv);

                //decode depthnormal
                float3 normal;
                float depth;
                DecodeDepthNormal(depthnormal, depth, normal);

                //get depth as distance from camera in units 
                depth = depth * _ProjectionParams.z;

                float depthDifference = 0;
                float normalDifference = 0;

                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(1, 0));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, 1));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, -1));
                Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(-1, 0));

                depthDifference = depthDifference * _DepthMult;
                depthDifference = saturate(depthDifference);
                depthDifference = pow(depthDifference, _DepthBias);

                normalDifference = normalDifference * _NormalMult;
                normalDifference = saturate(normalDifference);
                normalDifference = pow(normalDifference, _NormalBias);

                float outline = normalDifference + depthDifference;
                float4 sourceColor = tex2D(_MainTex, i.uv);
                float4 color = lerp(sourceColor, _OutlineColor, outline);
                return color;
            }
            ENDCG
        }
    }
}
