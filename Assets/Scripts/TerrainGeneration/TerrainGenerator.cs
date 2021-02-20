using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public const int WorldSizeX = 20; // in Blocks
    public const int WorldSizeY = 10; // in Blocks
    public const int WorldSizeZ = 20; // in Blocks

    // Terrain attributes
    private float SeaLevel = -10f;

    // Unity assets
    public Material WaterMaterial;
    public Shader TriplanarShader;

    // Generation
    public const int CellsPerBlock = 32; // Never change this!
    public const float BlockSize = 64f; // Don't change for now
    public const float CellSize = BlockSize / CellsPerBlock;

    private PerlinNoiseGenerator PerlinNoiseTerrainBase;
    private PerlinNoiseGenerator PerlinNoiseTerrainFeatures;

    // View
    public const float MaxViewDistance = 200; // Unity units in every direction
    public Transform Viewer;
    public Vector3 ViewerPosition;
    public int BlocksVisibleInViewDistance;
    public Dictionary<Vector3, TerrainBlock> TerrainBlockDictionary = new Dictionary<Vector3, TerrainBlock>();
    private List<TerrainBlock> terrainBlocksVisibleLastUpdate = new List<TerrainBlock>();

    // Biomes
    public static List<Biome> BiomeList;
    public GameObject BiomeQuad;

    // Generators
    public static RandomNumberGenerator RNG;
    private TerrainBlockMeshGenerator MeshGenerator;

    private WaterGenerator WaterGenerator;
    private BiomeGenerator BiomeGenerator;

    /*
    // Thrading
    Queue<ThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<ThreadInfo<MeshData>>();
    */

    // Start is called before the first frame update
    void Start()
    {
        // Initialize
        RNG = new RandomNumberGenerator();
        MeshGenerator = new TerrainBlockMeshGenerator(this);
        WaterGenerator = new WaterGenerator(this, WaterMaterial, SeaLevel);
        BiomeGenerator = new BiomeGenerator(avgBiomeSize: 500f, steps: 8, interpolationRange: 5);

        PerlinNoiseTerrainBase = new PerlinNoiseGenerator(scale: 250f, numOctaves: 2);
        PerlinNoiseTerrainFeatures = new PerlinNoiseGenerator(scale: 25f, numOctaves: 4);

        // Calculatations
        BlocksVisibleInViewDistance = Mathf.RoundToInt(MaxViewDistance / BlockSize);

        // Create biome list
        BiomeList = new List<Biome>()
        {
            new Biome_Grassland(),
            new Biome_Desert()
        };

        // BiomePreview
        if(BiomeQuad != null) BiomeGenerator.TexturePlaneAtWorldPosition(BiomeQuad, 200);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (meshDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < meshDataThreadInfoQueue.Count; i++)
            {
                ThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.Callback(threadInfo.Parameter);
            }
        }
        */

        // Update terrain blocks
        ViewerPosition = new Vector3(Viewer.position.x, Viewer.position.y, Viewer.position.z);
        UpdateVisibleBlocks();
    }

    /// <summary>
    /// Renders nearby blocks visible and far away ones invisible.
    /// Blocks that haven't appeared so far will be newly created.
    /// </summary>
    void UpdateVisibleBlocks()
    {
        //SetAllTerrainBlocksInvisible();

        // Set now visible chunks visible
        int currentChunkCoordX = Mathf.RoundToInt(ViewerPosition.x / BlockSize + WorldSizeX/2);
        int currentChunkCoordY = Mathf.RoundToInt(ViewerPosition.y / BlockSize + WorldSizeY/2);
        int currentChunkCoordZ = Mathf.RoundToInt(ViewerPosition.z / BlockSize + WorldSizeZ/2);

        for (int yOffset = -BlocksVisibleInViewDistance; yOffset <= BlocksVisibleInViewDistance; yOffset++)
        {
            for(int zOffset = -BlocksVisibleInViewDistance; zOffset <= BlocksVisibleInViewDistance; zOffset++) 
            {
                for (int xOffset = -BlocksVisibleInViewDistance; xOffset <= BlocksVisibleInViewDistance; xOffset++)
                {
                    // Coordinates of current chunk in loop
                    Vector3 currentBlockCoordinates = new Vector3(
                        currentChunkCoordX + xOffset, 
                        currentChunkCoordY + yOffset, 
                        currentChunkCoordZ + zOffset);

                    // Only load chunk if it is in world map bounds
                    if (currentBlockCoordinates.x >= 0 && currentBlockCoordinates.x < WorldSizeX && 
                        currentBlockCoordinates.y >= 0 && currentBlockCoordinates.y < WorldSizeY &&
                        currentBlockCoordinates.z >= 0 && currentBlockCoordinates.z < WorldSizeZ)
                    {
                        if (TerrainBlockDictionary.ContainsKey(currentBlockCoordinates))
                        {
                            TerrainBlock existingBlock = TerrainBlockDictionary[currentBlockCoordinates];
                            if (existingBlock != null) // Block can be null if it's fully in the terrain or in the air
                            {
                                existingBlock.UpdateChunk();
                                if (existingBlock.IsVisible()) terrainBlocksVisibleLastUpdate.Add(existingBlock);
                            }
                        }
                        else
                        {
                            Vector3 blockWorldPosition = GetCellCornerWorldPosition(currentBlockCoordinates, Vector3.zero);

                            TerrainBlock newTerrainBlock = MeshGenerator.CreateBlock(currentBlockCoordinates, blockWorldPosition);

                            if (newTerrainBlock != null)
                            {
                                BiomeGenerator.SetBiomesInBlock(newTerrainBlock);
                                newTerrainBlock.ApplyTexture(TriplanarShader);
                                WaterGenerator.AddWaterToBlock(newTerrainBlock);
                            }

                            TerrainBlockDictionary.Add(currentBlockCoordinates, newTerrainBlock);
                        }
                    }
                }
            }
        }
    }

    private void SetAllTerrainBlocksInvisible()
    {
        for (int i = 0; i < terrainBlocksVisibleLastUpdate.Count; i++)
        {
            terrainBlocksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainBlocksVisibleLastUpdate.Clear();
    }

    /// <summary>
    /// Returns the exact world position of a cell corner inside a block as a Vector3, given its coordinates
    /// </summary>
    public static Vector3 GetCellCornerWorldPosition(Vector3 blockCoordinates, Vector3 cornerCoordinates)
    {
        return new Vector3(
            -(WorldSizeX * BlockSize / 2) + blockCoordinates.x * BlockSize + cornerCoordinates.x * CellSize,
            -(WorldSizeY * BlockSize / 2) + blockCoordinates.y * BlockSize + cornerCoordinates.y * CellSize,
            -(WorldSizeZ * BlockSize / 2) + blockCoordinates.z * BlockSize + cornerCoordinates.z * CellSize);
    }

    /// <summary>
    /// Negative Value = Empty Space
    /// Positive Value = Inside Terrain
    /// PerformDictionaryLookup shout be set to true for positions that are used by more than one block (where 2 blocks meet at edges)
    /// </summary>
    public float GetDensityAt(Vector3 blockCoordinates, Vector3 cornerCoordinates)
    {
        Vector3 worldPosition = GetCellCornerWorldPosition(blockCoordinates, cornerCoordinates);

        /*
        Dictionary<int, float> Biomes = BiomeGenerator.GetBilinearInterpolatedBiomeValuesAt(worldPosition);
        float value = 0;
        foreach(KeyValuePair<int, float> kvp in Biomes)
        {
            Biome biome = BiomeList[kvp.Key];
            value += kvp.Value * biome.GetDensityAt(worldPosition);
        }
        return value;
        */

        float height = 
            100f * PerlinNoiseTerrainBase.GetNoiseValueAt(worldPosition) + 
            1f * PerlinNoiseTerrainFeatures.GetNoiseValueAt(worldPosition);

        return -worldPosition.y + height;
    }
}
