using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static TerrainGenerator;

public class Biome_Desert : Biome
{
    private PerlinNoiseGenerator PerlinNoiseTerrainBase;
    private PerlinNoiseGenerator PerlinNoiseTerrainFeatures;

    public Biome_Desert()
    {
        Type = BiomeType.Desert;
        Id = (int)Type;
        LandTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(TexturePath + "Sand/Atex_Sand2.jpg", typeof(Texture2D));
        CliffTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(TexturePath + "Rock/Atex_Rock3.jpg", typeof(Texture2D));

        PerlinNoiseTerrainBase = new PerlinNoiseGenerator(scale: 600f, numOctaves: 2);
        PerlinNoiseTerrainFeatures = new PerlinNoiseGenerator(scale: 25f, numOctaves: 4);
    }

    public override float GetDensityAt(Vector3 worldPosition)
    {
        float height =
            20f * PerlinNoiseTerrainBase.GetNoiseValueAt(worldPosition) +
            1f * PerlinNoiseTerrainFeatures.GetNoiseValueAt(worldPosition);

        return -worldPosition.y + height;
    }
}
