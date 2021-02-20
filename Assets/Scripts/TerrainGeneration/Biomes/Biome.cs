using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biome
{
    public const string TexturePath = "Assets/Textures/";

    public int Id;
    public BiomeType Type;

    public Texture2D LandTexture;
    public Texture2D CliffTexture;

    public abstract float GetDensityAt(Vector3 worldPosition);
}
