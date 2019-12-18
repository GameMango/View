Shader "View/ColorReplace"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _ShadeTex ("Shade (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _ShadeTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        
        // Globals
        bool _REPLACE_COLOR_1 = false;
        fixed4 _TO_REPLACE_COLOR_1;
        fixed4 _REPLACE_WITH_COLOR_1;
        
        bool _REPLACE_COLOR_2 = false;
        fixed4 _TO_REPLACE_COLOR_2;
        fixed4 _REPLACE_WITH_COLOR_2;
        
        bool _REPLACE_COLOR_3 = false;
        fixed4 _TO_REPLACE_COLOR_3;
        fixed4 _REPLACE_WITH_COLOR_3;
        
        bool _REPLACE_COLOR_4 = false;
        fixed4 _TO_REPLACE_COLOR_4;
        fixed4 _REPLACE_WITH_COLOR_4;
        
        bool _REPLACE_COLOR_5 = false;
        fixed4 _TO_REPLACE_COLOR_5;
        fixed4 _REPLACE_WITH_COLOR_5;
        
        bool _REPLACE_COLOR_6 = false;
        fixed4 _TO_REPLACE_COLOR_6;
        fixed4 _REPLACE_WITH_COLOR_6;
        
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            
            float tolerance = 0.1;
            
            // Replace color if it matches replace color
            if (_REPLACE_COLOR_1 && length(abs(c.rgb - _TO_REPLACE_COLOR_1.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_1.rgb;
            } 
            else if (_REPLACE_COLOR_2 && length(abs(c.rgb - _TO_REPLACE_COLOR_2.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_2.rgb;
            } 
            else if (_REPLACE_COLOR_3 && length(abs(c.rgb - _TO_REPLACE_COLOR_3.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_3.rgb;
            } 
            else if (_REPLACE_COLOR_4 && length(abs(c.rgb - _TO_REPLACE_COLOR_4.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_4.rgb;
            } 
            else if (_REPLACE_COLOR_5 && length(abs(c.rgb - _TO_REPLACE_COLOR_5.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_5.rgb;
            }
            else if (_REPLACE_COLOR_6 && length(abs(c.rgb - _TO_REPLACE_COLOR_6.rgb)) < tolerance)
            {
                c.rgb = _REPLACE_WITH_COLOR_6.rgb;
            }
            
           
            c = tex2D (_ShadeTex, IN.uv_MainTex) * c;
            
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
