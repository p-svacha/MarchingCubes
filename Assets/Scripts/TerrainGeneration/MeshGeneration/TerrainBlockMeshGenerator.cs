using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TerrainGenerator;

public class TerrainBlockMeshGenerator
{
    private TerrainGenerator TerrainGenerator;

    public TerrainBlockMeshGenerator(TerrainGenerator tg)
    {
        TerrainGenerator = tg;
    }

    public TerrainBlock CreateBlock(Vector3 blockCoordinates, Vector3 blockPosition)
    {
        // Make a quick check if block is empty (completely inside or outside terrain)
        if (IsBlockEmpty(blockCoordinates))
        {
            return null;
        }

        MeshData meshData = new MeshData();
        GameObject newTerrainBlockObject = new GameObject();
        TerrainBlock newTerrainBlock = newTerrainBlockObject.AddComponent<TerrainBlock>();

        // Iterate through all corners within a block
        int NumCornersPerSide = CellsPerBlock + 1;
        float[,,] blockDensityValues = new float[NumCornersPerSide, NumCornersPerSide, NumCornersPerSide];
        for (int z = 0; z < NumCornersPerSide; z++)
        {
            for (int y = 0; y < NumCornersPerSide; y++)
            {
                for (int x = 0; x < NumCornersPerSide; x++)
                {
                    Vector3 cornerCoordinates = new Vector3(x, y, z);

                    // Get Density value for every cell corner in the block(note that there is 1 more edge per side than there are cells)
                    float density = TerrainGenerator.GetDensityAt(blockCoordinates, cornerCoordinates);

                    // Temporarily save density value in current block
                    blockDensityValues[x, y, z] = density;

                    // Create mesh for cell cornering this edge
                    if (x > 0 && y > 0 && z > 0)
                    {
                        Vector3 cellStartCornerCoordinates = new Vector3(x - 1, y - 1, z - 1);

                        // Calculate world (start) position for this cell
                        Vector3 cellWorldPos = GetCellCornerWorldPosition(blockCoordinates, cellStartCornerCoordinates);

                        // Get density values for relevant corners
                        List<float> cellDensityValues = new List<float>()
                        {
                            blockDensityValues[x - 1, y - 1, z - 1],
                            blockDensityValues[x - 1, y    , z - 1],
                            blockDensityValues[x    , y    , z - 1],
                            blockDensityValues[x    , y - 1, z - 1],
                            blockDensityValues[x - 1, y - 1, z    ],
                            blockDensityValues[x - 1, y    , z    ],
                            blockDensityValues[x    , y    , z    ],
                            blockDensityValues[x    , y - 1, z    ],
                        };

                        // Convert density values to bits (negative = 0, positive = 1)
                        List<bool> cellBits = new List<bool>();
                        foreach (float f in cellDensityValues) cellBits.Insert(0, f >= 0);

                        // Get caseNr
                        int caseNr = Convert.ToInt32(string.Join("", cellBits.Select(b => b ? 1 : 0)), 2);

                        // Get triangles within cell
                        List<List<int>> meshes = LookupTables.GetTrianglesForCaseNr(caseNr);
                        foreach (List<int> triangleEdgeIds in meshes)
                        {
                            // Get vertices within triangle
                            List<Vector3> trianglePointsRelativePosWithinCell = new List<Vector3>();
                            trianglePointsRelativePosWithinCell = GetTrianglePointsWithinCell(triangleEdgeIds, cellDensityValues);

                            // Convert relative vertex positions to world position
                            List<Vector3> trianglePointsWorldPosition = new List<Vector3>();
                            foreach (Vector3 vertexRelPos in trianglePointsRelativePosWithinCell)
                            {
                                trianglePointsWorldPosition.Add(cellWorldPos + vertexRelPos * CellSize);
                            }

                            // Add triangle to mesh
                            meshData.AddTriangle(trianglePointsWorldPosition[0], trianglePointsWorldPosition[1], trianglePointsWorldPosition[2]);
                        }
                    }
                }
            }
        }
        
        newTerrainBlock.Initialize(blockCoordinates, blockPosition, BlockSize, TerrainGenerator, meshData);

        return newTerrainBlock;
    }

    /// <summary>
    /// Quick checks if a block is empty by checking if some selected points all have the same density.
    /// Checks every corner + 6 points bottom sides
    private bool IsBlockEmpty(Vector3 blockCoordinates)
    {
        int checkedPointsPerSide = 8;

        List<float> densitiesToCompare = new List<float>();

        // Add corners
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(0, 0, 0)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(0, CellsPerBlock, 0)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(0, 0, CellsPerBlock)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(0, CellsPerBlock, CellsPerBlock)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(CellsPerBlock, 0, 0)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(CellsPerBlock, CellsPerBlock, 0)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(CellsPerBlock, 0, CellsPerBlock)));
        densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(CellsPerBlock, CellsPerBlock, CellsPerBlock)));

        // Add side points
        float step = CellSize * ((float)CellsPerBlock / (float)checkedPointsPerSide);
        for(int i = 1; i < checkedPointsPerSide; i++)
        {
            densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(i*step, 0, 0)));
            densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(i*step, 0, CellsPerBlock)));
            densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(0, 0, i * step)));
            densitiesToCompare.Add(TerrainGenerator.GetDensityAt(blockCoordinates, new Vector3(CellsPerBlock, 0, i * step)));
        }

        return (densitiesToCompare.All(x => x >= 0) || densitiesToCompare.All(x => x < 0));
    }

    private List<Vector3> GetTrianglePointsWithinCell(List<int> triangleEdgeIds, List<float> cellDensityValues)
    {
        List<Vector3> triangleVertices = new List<Vector3>();
        foreach (int edgeId in triangleEdgeIds)
        {
            Vector3 positionRelativeInCell = new Vector3(0, 0, 0);
            switch (edgeId)
            {
                case 0:
                    positionRelativeInCell.x = 0;
                    positionRelativeInCell.y = GetRelativePositionWhereValueIs0(cellDensityValues[0], cellDensityValues[1]);
                    positionRelativeInCell.z = 0;
                    break;

                case 1:
                    positionRelativeInCell.x = GetRelativePositionWhereValueIs0(cellDensityValues[1], cellDensityValues[2]);
                    positionRelativeInCell.y = 1;
                    positionRelativeInCell.z = 0;
                    break;

                case 2:
                    positionRelativeInCell.x = 1;
                    positionRelativeInCell.y = GetRelativePositionWhereValueIs0(cellDensityValues[3], cellDensityValues[2]);
                    positionRelativeInCell.z = 0;
                    break;

                case 3:
                    positionRelativeInCell.x = GetRelativePositionWhereValueIs0(cellDensityValues[0], cellDensityValues[3]);
                    positionRelativeInCell.y = 0;
                    positionRelativeInCell.z = 0;
                    break;

                case 4:
                    positionRelativeInCell.x = 0;
                    positionRelativeInCell.y = GetRelativePositionWhereValueIs0(cellDensityValues[4], cellDensityValues[5]);
                    positionRelativeInCell.z = 1;
                    break;

                case 5:
                    positionRelativeInCell.x = GetRelativePositionWhereValueIs0(cellDensityValues[5], cellDensityValues[6]);
                    positionRelativeInCell.y = 1;
                    positionRelativeInCell.z = 1;
                    break;

                case 6:
                    positionRelativeInCell.x = 1;
                    positionRelativeInCell.y = GetRelativePositionWhereValueIs0(cellDensityValues[7], cellDensityValues[6]);
                    positionRelativeInCell.z = 1;
                    break;

                case 7:
                    positionRelativeInCell.x = GetRelativePositionWhereValueIs0(cellDensityValues[4], cellDensityValues[7]);
                    positionRelativeInCell.y = 0;
                    positionRelativeInCell.z = 1;
                    break;

                case 8:
                    positionRelativeInCell.x = 0;
                    positionRelativeInCell.y = 0;
                    positionRelativeInCell.z = GetRelativePositionWhereValueIs0(cellDensityValues[0], cellDensityValues[4]);
                    break;

                case 9:
                    positionRelativeInCell.x = 0;
                    positionRelativeInCell.y = 1;
                    positionRelativeInCell.z = GetRelativePositionWhereValueIs0(cellDensityValues[1], cellDensityValues[5]);
                    break;

                case 10:
                    positionRelativeInCell.x = 1;
                    positionRelativeInCell.y = 1;
                    positionRelativeInCell.z = GetRelativePositionWhereValueIs0(cellDensityValues[2], cellDensityValues[6]);
                    break;

                case 11:
                    positionRelativeInCell.x = 1;
                    positionRelativeInCell.y = 0;
                    positionRelativeInCell.z = GetRelativePositionWhereValueIs0(cellDensityValues[3], cellDensityValues[7]);
                    break;
            }
            triangleVertices.Add(positionRelativeInCell);
        }
        return triangleVertices;
    }

    /// <summary>
    /// Returns the relative position between value1 and value2 where the value is 0.
    /// One of the values needs to be positive and one needs to be negative.
    /// Returns the relative distance (0-1) from value1 to the point 0, whereas value2 is 1 in that scale.
    /// </summary>
    private float GetRelativePositionWhereValueIs0(float value1, float value2)
    {
        float diff = value2 - value1;
        float relPos = Math.Abs(value1 / diff);
        if (relPos < 0 || relPos > 1) throw new Exception("Relative position is outside of edge with " + relPos + ". Value 1: " + value1 + ", Value2: " + value2);
        return relPos;
    }
}
