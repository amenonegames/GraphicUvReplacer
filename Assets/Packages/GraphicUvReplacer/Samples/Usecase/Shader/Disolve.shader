Shader "Custom/VertexDissolve"
{
    Properties
    {
        // テクスチャが一致した場合にバッチングが効くようにするため、
        // テクスチャは Material から直接ではなく MaterialPropertyBlock 経由で設定する
        // （なお、UI シェーダーでは基本的に MaterialPropertyBlock を使うことはできない）
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _DissolveTex ("Dissolve Texture", 2D) = "white" {}
        
        // 色
        _Color ("Tint", Color) = (1,1,1,1)
        
        [Toggle(_STENCIL)] _EnableStencil("Enable Stencil Testing", Float) = 0.0
        _StencilReference("Stencil Reference", Range(0, 255)) = 0
        [Enum(UnityEngine.Rendering.CompareFunction)]_StencilComparison("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)]_StencilOperation("Stencil Operation", Int) = 0
        
        
        
    }

    SubShader
    {
        // タグを使っていつ/どのようにレンダリングするかを指定する
        // https://docs.unity3d.com/ja/current/Manual/SL-SubShaderTags.html
        Tags
        {
            // UI 用なので RenderQueue は Transparent
            "Queue"="Transparent"

            // Projector コンポーネントの影響を受けない
            "IgnoreProjector"="True"

            // シェーダの分類。RenderQueue とは別
            // Shader Replacement を使わないなら必要ないが一応書いておく
            "RenderType"="Transparent"

            // Inspector の下のマテリアルビューの表示方式
            // デフォルトは Sphere（球体）だが Plane（2D）または Skybox（スカイボックス）が選べる
            "PreviewType"="Plane"

            // このシェーダーが Sprite 用かつアトラス化された場合には動作しないことを明示したい場合には False にする
            // 基本的には True で良い
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref[_StencilReference]
            Comp[_StencilComparison]
            Pass[_StencilOperation]
            
            
        }

        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // UI なのでカリング不要
        Cull Off

        // レガシーな固定機能ライティング（非推奨）
        // https://docs.unity3d.com/ja/current/Manual/SL-Material.html
        // 現在では（UI に限らず）基本的には Off で良い
        Lighting Off

        // 深度バッファへの書き込み
        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // Transparent なので ZWrite は不要
        ZWrite Off

        // 深度テストの方法
        // https://docs.unity3d.com/ja/current/Manual/SL-CullAndDepth.html
        // Canvas が Overlay なら Always（常に描画）
        // それ以外なら LEqual（描画済みオブジェクトとの距離が距離が等しいまたはより近い場合に描画）
        ZTest [unity_GUIZTestMode]

        // https://docs.unity3d.com/ja/current/Manual/SL-Blend.html
        // Unity 2020.1 からピクセルブレンドは乗算済み透明（Premultiplied transparency）になった
	    // https://issuetracker.unity3d.com/issues/transparent-ui-gameobject-ignores-opaque-ui-gameobject-when-using-rendertexture
        // 以前のブレンドにしたいなら Blend SrcAlpha OneMinusSrcAlpha にしてフラグメントシェーダーの乗算済み透明の処理を消す
	    Blend SrcAlpha OneMinusSrcAlpha//One OneMinusSrcAlpha

        Pass
        {
            // UsePass で使う名前
            // https://docs.unity3d.com/ja/current/Manual/SL-Name.html
            Name "Default"

            // Cg/HLSL 開始
            CGPROGRAM
            // HLSL スニペット
            // https://docs.unity3d.com/ja/2018.4/Manual/SL-ShaderPrograms.html

            // 頂点シェーダーの関数名を指定
            #pragma vertex vert

            // フラグメントシェーダーの関数名を指定
            #pragma fragment frag
            
            // ターゲットレベルは全プラットフォーム向け
            // https://docs.unity3d.com/ja/current/Manual/SL-ShaderCompileTargets.html
            #pragma target 2.0

            // インクルードファイルの指定
            // インクルードファイルの場所は (Unity のインストール先)/Editor/Data/CGIncludes
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_instancing


            // メッシュの頂点データの定義
            struct appdata_t
            {
                // 位置
                float4 vertex   : POSITION;

                // 頂点カラー
                float4 color    : COLOR;

                // 1 番目の UV 座標
                float4 texcoord : TEXCOORD0;

                // インスタンシングが有効な場合に
                //    uint instanceID : SV_InstanceID
                // という定義が付け加えられる。
                // 詳細は UnityInstancing.cginc を参照のこと
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // 頂点シェーダーからフラグメントシェーダーに渡すデータ
            struct v2f
            {
                // 頂点のクリップ座標
                // システムが使う（GPU がラスタライズに使う）値なので SV (System Value) が付く
                float4 vertex   : SV_POSITION;

                // 色
                fixed4 color    : COLOR;

                // 1 番目の UV 座標
                float4 texcoord  : TEXCOORD0;

                // 2 番目の UV 座標に頂点のワールド空間での位置を格納して渡す
                float4 worldPosition : TEXCOORD1;

                // フラグメントシェーダにインスタンスIDを受け渡す
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // テクスチャデータを参照するためにはテクスチャサンプラ型の値をプロパティ経由で受け取る
            sampler2D _MainTex;
            sampler2D _DissolveTex;
            
            // 色
            UNITY_INSTANCING_BUFFER_START(Props)
               UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
            UNITY_INSTANCING_BUFFER_END(Props)
            // テクスチャ変数名 に _ST を追加すると Tiling と Offset の値が入ってくる
            // x, y は Tiling 値の x, y で、z, w は Offset 値の z, w が入れられる
            float4 _MainTex_ST;
            
            // UI 用に Unity によって自動的に設定される。
            // 使用するテククチャが Alpha8 型なら (1,1,1,0) 、それ以外なら (0,0,0,0) になる
            fixed4 _TextureSampleAdd;

            // 頂点シェーダー
            // appdata_t を受け取って v2f を返す
            v2f vert(appdata_t v)
            {
                // フラグメントシェーダーに渡す変数
                v2f OUT;

                // VR 用の目の情報と、GPU インスタンシングのためのインスタンシングごとの座標を反映させる
                // UnityInstancing.cginc を参照のこと
                UNITY_SETUP_INSTANCE_ID(v);

                // VR 用のテクスチャ配列の目を GPU に伝える
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                // オブジェクト空間の頂点の座標をカメラのクリップ空間に変換する
                // UnityShaderUtilities.cginc より
                // mul(UNITY_MATRIX_VP, float4(mul(unity_ObjectToWorld, float4(inPos, 1.0)).xyz, 1.0));
                //   UNITY_MATRIX_VP : 現在のビュー * プロジェクション行列
                //   unity_ObjectToWorld : 現在のモデル行列
                //   https://docs.unity3d.com/ja/2018.4/Manual/SL-UnityShaderVariables.html
                // 実態としては mul(UNITY_MATRIX_MVP, v.vertex) と等しい
                float4 vPosition = UnityObjectToClipPos(v.vertex);

                // 2 番目の UV 座標に頂点のワールド空間を渡す
                OUT.worldPosition = v.vertex;

                // 変換した頂点のクリップ座標を渡す
                OUT.vertex = vPosition;
                
                float intensity = v.texcoord.z;
                float powedIntensity = pow(intensity , 2);
                float antiCurvedIntensity = -pow(intensity-1 , 2) +1;
                OUT.texcoord = float4( v.texcoord.xy , powedIntensity , antiCurvedIntensity);
                //OUT.texcoord.w  = pow ( OUT.texcoord.z , 2);
                
                // 頂点カラーにプロパティのカラーを乗算
                OUT.color = v.color * _Color;

                return OUT;
            }

            // フラグメントシェーダー
            fixed4 frag(v2f IN) : SV_Target
            {
                // テクスチャから色のサンプリング
                half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd);
                half4 dissolve = tex2D(_DissolveTex, IN.texcoord);

                //IN.texcoord.zにIntensittyを格納して渡している
                // float intensity =  IN.texcoord.z;;
                float powedIntensity = IN.texcoord.z;
                float antiCurvedIntensity = IN.texcoord.w;
                float disolve = smoothstep( powedIntensity , antiCurvedIntensity , dissolve.a); 
                //return float4(disolve,disolve,disolve,1);
                color.a *= disolve;
                color *= IN.color;
                
                return color;
            }
        // Cg/HLSL 終了
        ENDCG
            
            
        }
        
        
    }
}
