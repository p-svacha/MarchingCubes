Shader "Custom/TriplanarShader"
{
    Properties
    {
        _DiffuseMapTop1("Diffuse Map Top 1", 2D) = "white" {}
        _DiffuseMapTop2("Diffuse Map Top 2", 2D) = "white" {}
        _DiffuseMapSide1("Diffuse Map Side 1", 2D) = "white" {}
        _DiffuseMapSide2("Diffuse Map Side 2", 2D) = "white" {}
        _TextureScale("Texture Scale",float) = 1
        _TriplanarBlendSharpness("Blend Sharpness",float) = 1

        // Triplanar shading
        _SideStartSteepness("Side Texture Start Steepness",float) = 0.3 // The steepness where side texture starts to show through
        _SideOnlySteepness("Side Texture Only Steepness",float) = 0.7 // The steepness where only side texture is shown

        // Biome texturing
        _ColorizeMap1("Colorize Map 1", 2D) = "black" {}
        _ColorizeMap2("Colorize Map 2", 2D) = "black" {}
        _WorldSpaceRange("World Space Range", Vector) = (0,0,33,33)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        #pragma target 3.0
        #pragma surface surf Lambert

        sampler2D _DiffuseMapTop1;
        sampler2D _DiffuseMapTop2;

        sampler2D _DiffuseMapSide1;
        sampler2D _DiffuseMapSide2;

        float _TextureScale;
        float _TriplanarBlendSharpness;
        float _SideStartSteepness;
        float _SideOnlySteepness;

        sampler2D _ColorizeMap1;
        sampler2D _ColorizeMap2;
        fixed4 _WorldSpaceRange;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Find our UVs for each axis based on world position of the fragment.
            half2 yUV = IN.worldPos.xz / _TextureScale;
            half2 xUV = IN.worldPos.zy / _TextureScale;
            half2 zUV = IN.worldPos.xy / _TextureScale;

            // Get the absolute value of the world normal.
            // Put the blend weights to the power of BlendSharpness: The higher the value, the sharper the transition between the planar maps will be.
            half3 blendWeights = pow(abs(IN.worldNormal), _TriplanarBlendSharpness);

            // ----- BIOME ------
            // Create UV's for color map (depending on block position and world position)
            float2 colorizedMapUV = (IN.worldPos.xz - _WorldSpaceRange.xy)
                / (_WorldSpaceRange.zw - _WorldSpaceRange.xy);

            float texture1Strength = tex2D(_ColorizeMap1, colorizedMapUV);

            // ----- TRIPLANAR ------
            // Divide our blend mask by the sum of it's components, this will make x+y+z=1
            blendWeights = blendWeights / (blendWeights.x + blendWeights.y + blendWeights.z);

            // Define how much of alpha is top texture
            float steepness = 1 - blendWeights.y;
            float topTextureStrength;
            if (steepness < _SideStartSteepness)
                topTextureStrength = 1;
            else if (steepness < _SideOnlySteepness)
                topTextureStrength = 1 - ((steepness - _SideStartSteepness) * (1 / (_SideOnlySteepness - _SideStartSteepness)));
            else
                topTextureStrength = 0;

            // Now do texture samples from our diffuse maps with each of the 3 UV set's we've just made.
            // Blend top with side texture according to how much the surface normal points vertically (y-direction)
            half3 yDiff = (1 - topTextureStrength) * tex2D(_DiffuseMapSide1, yUV) + 
                topTextureStrength * (texture1Strength * tex2D(_DiffuseMapTop1, yUV) + (1 - texture1Strength) * tex2D(_DiffuseMapTop2, yUV));

            half3 xDiff = (1 - topTextureStrength) * tex2D(_DiffuseMapSide1, xUV) + 
                topTextureStrength * (texture1Strength * tex2D(_DiffuseMapTop1, xUV) + (1 - texture1Strength) * tex2D(_DiffuseMapTop2, xUV));

            half3 zDiff = (1 - topTextureStrength) * tex2D(_DiffuseMapSide1, zUV) + 
                topTextureStrength * (texture1Strength * tex2D(_DiffuseMapTop1, zUV) + (1 - texture1Strength) * tex2D(_DiffuseMapTop2, zUV));

            o.Albedo = xDiff * blendWeights.x + yDiff * blendWeights.y + zDiff * blendWeights.z;
        }
        ENDCG
    }
}