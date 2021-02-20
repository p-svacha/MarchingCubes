using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainGenerator;

public class WaterGenerator
{
    private TerrainGenerator TerrainGenerator;
    private Material WaterMaterial;
    private float SeaLevel;
    private int SeaLevelBlockYCoordinate; // The y coordinate of the blocks that cross sea level

    public WaterGenerator(TerrainGenerator tg, Material waterMat, float seaLevel)
    {
        TerrainGenerator = tg;
        WaterMaterial = waterMat;
        SeaLevel = seaLevel;
        SeaLevelBlockYCoordinate = (int)(WorldSizeY / 2 + (SeaLevel / BlockSize));
    }

    public void AddWaterToBlock(TerrainBlock block)
    {
        if (block.Coordinates.y <= SeaLevelBlockYCoordinate)
        {
            GameObject waterPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            waterPlane.transform.parent = block.transform;
            waterPlane.transform.localScale = new Vector3(1f / 10 * BlockSize, 1f, 1f / 10 * BlockSize);
            waterPlane.transform.position = new Vector3(block.Bounds.center.x, SeaLevel, block.Bounds.center.z);
            waterPlane.GetComponent<MeshRenderer>().material = WaterMaterial;
        }
    }
}
