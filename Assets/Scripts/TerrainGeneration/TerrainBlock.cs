using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainGenerator;

public class TerrainBlock : MonoBehaviour
{
    // Generator
    public TerrainGenerator TerrainGenerator;

    // Attributes
    public MeshData MeshData;
    public Vector3 Coordinates;
    public Vector3 WorldPosition;
    public Bounds Bounds;

    // Biome
    public int[,] BiomeValues;
    public Dictionary<int, float>[,] InterpolatedBiomeValues;

    public HashSet<BiomeType> Biomes = new HashSet<BiomeType>();

    // Components
    MeshFilter MeshFilter;
    MeshRenderer MeshRenderer;

    // Should be called right after being instantiated
    public void Initialize(Vector3 coordinates, Vector3 worldPosition, float size, TerrainGenerator generator, MeshData meshData)
    {
        Coordinates = coordinates;
        WorldPosition = worldPosition;
        TerrainGenerator = generator;

        Bounds = new Bounds(new Vector3(worldPosition.x + size / 2, worldPosition.y + size / 2, worldPosition.z + size / 2), new Vector3(size, size, size));

        gameObject.name = "TerrainChunk " + Coordinates.x + "/" + Coordinates.y + "/" + coordinates.z;
        gameObject.transform.parent = generator.transform;
        //gameObject.transform.position = new Vector3(WorldPosition.x, WorldPosition.y, WorldPosition.z);

        MeshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter = gameObject.AddComponent<MeshFilter>();

        SetVisible(false);

        MeshFilter.mesh = meshData.CreateMesh();

        //TerrainGenerator.RequestMeshData(OnMeshDataReceived, WorldPosition);
    }

    public void ApplyTexture(Shader shader)
    {
        Material material = new Material(shader);

        // Set terrain block range in shader
        Vector4 worldRange = new Vector4(
        WorldPosition.x,
        WorldPosition.z,
        WorldPosition.x + BlockSize,
        WorldPosition.z + BlockSize);
        material.SetVector("_WorldSpaceRange", worldRange);

        material.SetFloat("_TextureScale", 1f);

        int counter = 1;
        foreach(BiomeType type in Biomes)
        {
            Biome biome = BiomeList[(int)type];

            // Set textures in shader
            material.SetTexture("_DiffuseMapTop" + counter, biome.LandTexture);
            material.SetTexture("_DiffuseMapSide" + counter, biome.CliffTexture);

            // Create height maps for biome texture mixing for shader
            Texture2D colorTex = new Texture2D(BiomeValues.GetLength(0), BiomeValues.GetLength(1));
            colorTex.filterMode = FilterMode.Point;

            for (int x = 0; x < BiomeValues.GetLength(0); x++)
            {
                for (int y = 0; y < BiomeValues.GetLength(1); y++)
                {
                    float biomeStrength = 0;
                    if(InterpolatedBiomeValues[x, y].ContainsKey(biome.Id)) biomeStrength = InterpolatedBiomeValues[x, y][biome.Id]; ;
                    colorTex.SetPixel(x, y, new Color(biomeStrength, biomeStrength, biomeStrength));
                }
            }
            colorTex.Apply();

            material.SetTexture("_ColorizeMap" + counter, colorTex);

            counter++;
        }

        MeshRenderer.material = material;
    }

    /*
    void OnMeshDataReceived(MeshData meshData)
    {
        if (meshData.Triangles.Count == 0)
        {
            TerrainGenerator.TerrainBlockDictionary[Coordinates] = null;
            Destroy(gameObject);
        }
        else
        {
            MeshFilter.mesh = meshData.CreateMesh();
        }
    }
    */

    public void UpdateChunk()
    {
        float viewerDistanceFromNearestEdge = Mathf.Sqrt(Bounds.SqrDistance(TerrainGenerator.ViewerPosition));
        bool visible = viewerDistanceFromNearestEdge < TerrainGenerator.MaxViewDistance;

        // Change this back if blocks should unload when far away
        SetVisible(true);
        //SetVisible(visible);
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public bool IsVisible()
    {
        return gameObject.activeSelf;
    }
}
