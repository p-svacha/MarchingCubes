using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TerrainGenerator;

public class BiomeGenerator : AreaNoiseGenerator
{
    // Attributes
    float AverageBiomeSize;
    int Steps;
    int InterpolationRange; // The area around a biome pixel that affects the interpolated biomes

    float BiomeSizeAtSmallestStep;

    private int NumBiomes;
    private int[] BiomeHash;
    private int BiomeHashMask;

    private int NumDirections;
    private int[] DirectionHash;
    private int DirectionHashMask;

    private Dictionary<Vector2, int> BiomeValuesSecondLastStep = new Dictionary<Vector2, int>();
    private Dictionary<Vector2, int> BiomeValues = new Dictionary<Vector2, int>();

    private Dictionary<Vector2, Dictionary<int, float>> InterpolatedBiomeValues = new Dictionary<Vector2, Dictionary<int, float>>();
    private Dictionary<Vector2, Dictionary<int, float>> BilinearInterpolatedBiomeValues = new Dictionary<Vector2, Dictionary<int, float>>();

    // For testing
    private int[,] Values;

    public BiomeGenerator(
        float avgBiomeSize = 500f,
        int steps = 6,
        int interpolationRange = 3
        ) : base()
    {
        AverageBiomeSize = avgBiomeSize;
        Steps = steps;
        InterpolationRange = interpolationRange;

        BiomeSizeAtSmallestStep = (float)(AverageBiomeSize / Math.Pow(2, Steps));

        int hashRepeats = 256;

        // Fill biome hash
        NumBiomes = Enum.GetValues(typeof(BiomeType)).Length;
        BiomeHash = new int[NumBiomes * hashRepeats * 2];
        BiomeHashMask = (hashRepeats * NumBiomes) - 1;
        
        int counter = 0;
        List<int> hashValues = new List<int>();
        for(int i = 0; i < NumBiomes * hashRepeats; i++)
        {
            hashValues.Add(i);
        }
        while(hashValues.Count > 0)
        {
            int value = hashValues[(int)RNG.Next(0, hashValues.Count)];
            BiomeHash[counter] = value;
            BiomeHash[(NumBiomes * hashRepeats) + counter] = value;
            hashValues.Remove(value);
            counter++;
        }

        // Fill direction hash
        NumDirections = 4;
        DirectionHash = new int[NumDirections * hashRepeats * 2];
        DirectionHashMask = (hashRepeats * NumDirections) - 1;

        counter = 0;
        hashValues.Clear();
        for (int i = 0; i < NumDirections * hashRepeats; i++)
        {
            hashValues.Add(i);
        }
        while (hashValues.Count > 0)
        {
            int value = hashValues[(int)RNG.Next(0, hashValues.Count)];
            DirectionHash[counter] = value;
            DirectionHash[(NumDirections * hashRepeats) + counter] = value;
            hashValues.Remove(value);   
            counter++;
        }
        //foreach (int value in DirectionHash) Debug.Log(value);

        // Testing only
        Values = new int[2, 2];
        for (int i = 0; i < Values.GetLength(0); i++)
        {
            for (int j = 0; j < Values.GetLength(1); j++)
            {
                Values[i, j] = UnityEngine.Random.Range(1, 10);
            }
        }
    }

    public void SetBiomesInBlock(TerrainBlock block)
    {
        block.BiomeValues = new int[CellsPerBlock + 1, CellsPerBlock + 1];
        block.InterpolatedBiomeValues = new Dictionary<int, float>[CellsPerBlock + 1, CellsPerBlock + 1];

        for (int y = 0; y < CellsPerBlock + 1; y++)
        {
            for(int x = 0; x < CellsPerBlock + 1; x++)
            {
                Vector3 cellWorldPosition = new Vector3(block.WorldPosition.x + x * CellSize, 0, block.WorldPosition.z + y * CellSize);

                block.BiomeValues[x, y] = (int)GetNoiseValueAt(cellWorldPosition);

                block.InterpolatedBiomeValues[x, y] = GetBilinearInterpolatedBiomeValuesAt(cellWorldPosition);
                foreach (int biomeId in block.InterpolatedBiomeValues[x, y].Keys) block.Biomes.Add((BiomeType)biomeId);
            }
        }
    }

    public override int GetNoiseValueAt(Vector3 point)
    {
        int xBiomeCoord = (int)(point.x / BiomeSizeAtSmallestStep);
        int yBiomeCoord = (int)(point.z / BiomeSizeAtSmallestStep);
        Vector2 biomeCoord = new Vector2(xBiomeCoord, yBiomeCoord);

        if (BiomeValues.ContainsKey(biomeCoord)) return BiomeValues[biomeCoord];
        else
        {
            int value = RecursiveBiomeValueAt(new Vector2(point.x, point.z), Steps);
            BiomeValues.Add(biomeCoord, value);
            return value;
        }
    }

    public Dictionary<int, float> GetBilinearInterpolatedBiomeValuesAt(Vector3 point)
    {
        // Check if stored in cahce
        Vector2 point2D = new Vector2(point.x, point.z);
        if (BilinearInterpolatedBiomeValues.ContainsKey(point2D)) return BilinearInterpolatedBiomeValues[point2D];

        float x = point.x / BiomeSizeAtSmallestStep;
        float y = point.z / BiomeSizeAtSmallestStep;
        int xi = (int)x;
        int yi = (int)y;

        Dictionary<int, float> d00 = GetInterpolatedBiomeValuesAt(new Vector2(xi, yi));
        Dictionary<int, float> d10 = GetInterpolatedBiomeValuesAt(new Vector2(xi + 1, yi));
        Dictionary<int, float> d01 = GetInterpolatedBiomeValuesAt(new Vector2(xi, yi + 1));
        Dictionary<int, float> d11 = GetInterpolatedBiomeValuesAt(new Vector2(xi + 1, yi + 1));

        Dictionary<int, float> bilinearInterpolatedValues = new Dictionary<int, float>();

        for(int i = 0; i < NumBiomes; i++)
        {
            float v00 = d00.ContainsKey(i) ? d00[i] : 0;
            float v10 = d10.ContainsKey(i) ? d10[i] : 0;
            float v01 = d01.ContainsKey(i) ? d01[i] : 0;
            float v11 = d11.ContainsKey(i) ? d11[i] : 0;
            float value = MathUtils.Blerp(v00, v10, v01, v11, x - xi, y - yi);
            if (value > 0) bilinearInterpolatedValues.Add(i, value);
        }

        BilinearInterpolatedBiomeValues.Add(point2D, bilinearInterpolatedValues);
        return bilinearInterpolatedValues;
    }

    // Returns a dictionary which contains which biome is represented to what % at current position
    private Dictionary<int, float> GetInterpolatedBiomeValuesAt(Vector2 biomeCoord)
    {
        // Check if stored in cache
        if (InterpolatedBiomeValues.ContainsKey(biomeCoord)) return InterpolatedBiomeValues[biomeCoord];

        Dictionary<int, int> absoluteOccurences = new Dictionary<int, int>();

        for (int dx = (int)-InterpolationRange; dx < InterpolationRange + 1; dx++)
        {
            for (int dy = (int)-InterpolationRange; dy < InterpolationRange + 1; dy++)
            {
                float x = ((biomeCoord.x * BiomeSizeAtSmallestStep) + (dx * BiomeSizeAtSmallestStep)) + 0.5f * BiomeSizeAtSmallestStep; // maybe doesn't work because of mapping from coordinate to world pos
                float z = ((biomeCoord.y * BiomeSizeAtSmallestStep) + (dy * BiomeSizeAtSmallestStep)) + 0.5f * BiomeSizeAtSmallestStep;

                int value = (int)GetNoiseValueAt(new Vector3(x, 0, z));

                if (absoluteOccurences.ContainsKey(value))
                    absoluteOccurences[value]++;
                else
                    absoluteOccurences.Add(value, 1);
            }
        }

        // Normalize
        Dictionary<int, float> relativeOccurences = absoluteOccurences.ToDictionary(x => x.Key, x => (float)x.Value / (float)absoluteOccurences.Sum(y => y.Value));
        InterpolatedBiomeValues.Add(biomeCoord, relativeOccurences);
        return relativeOccurences;
    }


    private int RecursiveBiomeValueAt(Vector2 point, int step)
    {
        // Check if the biome value has already been calculated at this position for second last step
        float biomeSizeThisStep = (float)(AverageBiomeSize / Math.Pow(2, step));
        int xBiomeCoordAtThisStep = (int)(point.x / biomeSizeThisStep);
        int yBiomeCoordAtThisStep = (int)(point.y / biomeSizeThisStep);

        if (step == Steps - 1 && BiomeValuesSecondLastStep.ContainsKey(new Vector2(xBiomeCoordAtThisStep, yBiomeCoordAtThisStep)))
            return BiomeValuesSecondLastStep[new Vector2(xBiomeCoordAtThisStep, yBiomeCoordAtThisStep)];

        // Get biome coordinates
        int xBiomeCoordAtStep0 = (int)(point.x / AverageBiomeSize);
        int yBiomeCoordAtStep0 = (int)(point.y / AverageBiomeSize);

        float biomeSizeLastStep = (float)(AverageBiomeSize / Math.Pow(2, step-1));
        int xBiomeCoordAtLastStep = (int)(point.x / biomeSizeLastStep);
        int yBiomeCoordAtLastStep = (int)(point.y / biomeSizeLastStep);

        // Find which sector of the biome we are in
        float exactBiomeCoordX = (point.x / biomeSizeLastStep);
        float relativePoisitionInBiomeX = exactBiomeCoordX - xBiomeCoordAtLastStep;

        float exactBiomeCoordY = (point.y / biomeSizeLastStep);
        float relativePoisitionInBiomeY = exactBiomeCoordY - yBiomeCoordAtLastStep;

        int dir = GetDirectionHash(xBiomeCoordAtThisStep, yBiomeCoordAtThisStep);
        int value;

        int currentBiomeValue = step == 1 ?
                GetInitialBiomeIdAt(xBiomeCoordAtStep0, yBiomeCoordAtStep0) :
                RecursiveBiomeValueAt(point, step - 1);

        int leftBiomeValue = step == 1 ?
                GetInitialBiomeIdAt(xBiomeCoordAtStep0 - 1, yBiomeCoordAtStep0) :
                RecursiveBiomeValueAt(new Vector2(point.x - biomeSizeLastStep, point.y), step - 1);

        int rightBiomeValue = step == 1 ?
                GetInitialBiomeIdAt(xBiomeCoordAtStep0 + 1, yBiomeCoordAtStep0) :
                RecursiveBiomeValueAt(new Vector2(point.x + biomeSizeLastStep, point.y), step - 1);

        int botBiomeValue = step == 1 ?
                GetInitialBiomeIdAt(xBiomeCoordAtStep0, yBiomeCoordAtStep0 - 1) :
                RecursiveBiomeValueAt(new Vector2(point.x, point.y - biomeSizeLastStep), step - 1);

        int topBiomeValue = step == 1 ?
                GetInitialBiomeIdAt(xBiomeCoordAtStep0, yBiomeCoordAtStep0 + 1) :
                RecursiveBiomeValueAt(new Vector2(point.x, point.y + biomeSizeLastStep), step - 1);

        if (relativePoisitionInBiomeX < 0.5f && relativePoisitionInBiomeY < 0.5f) // botleft corner
        {
            if (dir == 0) value = leftBiomeValue;
            else if (dir == 1) value = botBiomeValue;
            else value = currentBiomeValue;
        }
        else if (relativePoisitionInBiomeX >= 0.5f && relativePoisitionInBiomeY < 0.5f) // botright corner
        {
            if (dir == 0) value = rightBiomeValue;
            else if (dir == 1) value = botBiomeValue;
            else value = currentBiomeValue;
        }
        else if (relativePoisitionInBiomeX >= 0.5f && relativePoisitionInBiomeY >= 0.5f) // topright corner
        {
            if (dir == 0) value = rightBiomeValue;
            else if (dir == 1) value = topBiomeValue;
            else value = currentBiomeValue;
        }
        else if (relativePoisitionInBiomeX < 0.5f && relativePoisitionInBiomeY >= 0.5f) // topleft corner
        {
            if (dir == 0) value = leftBiomeValue;
            else if (dir == 1) value = topBiomeValue;
            else value = currentBiomeValue;
        }
        else throw new Exception("Couldn't fit position into quartile.");

        if (step == Steps - 1) BiomeValuesSecondLastStep.Add(new Vector2(xBiomeCoordAtThisStep, yBiomeCoordAtThisStep), value);
        return value;
    }

    private int GetInitialBiomeIdAt(int x, int y)
    {
        x &= BiomeHashMask;
        y &= BiomeHashMask;
        return BiomeHash[(BiomeHash[x] + y) & BiomeHashMask] % NumBiomes;
    }

    private int GetDirectionHash(int x, int y)
    {
        x &= DirectionHashMask;
        y &= DirectionHashMask;
        return DirectionHash[(DirectionHash[x] + y) & DirectionHashMask] % NumDirections;
    }

    public Texture2D GetExampleTexture()
    {
        List<Color> colorMap = new List<Color>();
        for(int i = 0; i < Values.GetLength(0); i++)
        {
            for(int j = 0; j < Values.GetLength(1); j++)
            {
                colorMap.Add(GetColorFor(Values[i, j]));
            }
        }

        Texture2D texture = new Texture2D(Values.GetLength(0), Values.GetLength(1));
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap.ToArray());
        texture.Apply();
        return texture;
    }

    /// <summary>
    /// This version makes 4 pixels out of each pixel, whereas each of the new pixels can have the value of the old or of a neighbour pixel;
    /// </summary>
    public void AddStepV1()
    {
        int[,] newValues = new int[Values.GetLength(0)*2,Values.GetLength(1)*2];
        for (int i = 0; i < Values.GetLength(0); i++)
        {
            for (int j = 0; j < Values.GetLength(1); j++)
            {
                newValues[i * 2, j * 2] = GetNewV1ValueFor(i * 2, j * 2);
                newValues[i * 2 + 1, j * 2] = GetNewV1ValueFor(i * 2 + 1, j * 2);
                newValues[i * 2, j * 2 + 1] = GetNewV1ValueFor(i * 2, j * 2 + 1);
                newValues[i * 2 + 1, j * 2 + 1] = GetNewV1ValueFor(i * 2 + 1, j * 2 + 1);
            }
        }

        Values = newValues;
    }

    private int GetNewV1ValueFor(int x, int y)
    {
        int oldX = x / 2;
        int oldY = y / 2;

        List<int> possibleValues = new List<int>();
        possibleValues.Add(Values[oldX, oldY]);
        possibleValues.Add(Values[oldX, oldY]);

        if (x % 2 == 0 && x > 0) possibleValues.Add(Values[oldX - 1, oldY]);
        else if (x % 2 == 1 && x < (Values.GetLength(0) * 2) - 1) possibleValues.Add(Values[oldX + 1, oldY]);

        if (y % 2 == 0 && y > 0) possibleValues.Add(Values[oldX, oldY - 1]);
        else if (y % 2 == 1 && y < (Values.GetLength(1) * 2) - 1) possibleValues.Add(Values[oldX, oldY + 1]);

        return possibleValues[UnityEngine.Random.Range(0, possibleValues.Count)];
    }

    /// <summary>
    /// This version makes 9 pixels out of 4 pixels, where the corners stay, edges have random value of corners and center has random value of edges
    /// </summary>
    public void AddStepV2()
    {
        int[,] newValues = new int[Values.GetLength(0) * 2 - 1, Values.GetLength(1) * 2 - 1];
        for (int i = 0; i < Values.GetLength(0) - 1; i ++)
        {
            for (int j = 0; j < Values.GetLength(1) - 1; j ++)
            {
                // Corners stay
                newValues[i * 2, j * 2] = Values[i, j];
                newValues[(i + 1) * 2, j * 2] = Values[i + 1, j];
                newValues[i * 2, (j + 1) * 2] = Values[i, j + 1];
                newValues[(i + 1) * 2, (j + 1) * 2] = Values[i + 1, j + 1];

                // Edges get random corner value if not yet assigned
                if (newValues[i * 2 + 1, j * 2] == 0) newValues[i * 2 + 1, j * 2] = GetNewV2EdgeValueFor(i * 2 + 1, j * 2);
                if (newValues[i * 2, j * 2 + 1] == 0) newValues[i * 2, j * 2 + 1] = GetNewV2EdgeValueFor(i * 2, j * 2 + 1);
                if (newValues[i * 2 + 1, j * 2 + 2] == 0) newValues[i * 2 + 1, j * 2 + 2] = GetNewV2EdgeValueFor(i * 2 + 1, j * 2 + 2);
                if (newValues[i * 2 + 2, j * 2 + 1] == 0) newValues[i * 2 + 2, j * 2 + 1] = GetNewV2EdgeValueFor(i * 2 + 2, j * 2 + 1);

                // Center
                List<int> possibleValues = new List<int>();
                possibleValues.Add(newValues[i * 2 + 1, j * 2]);
                possibleValues.Add(newValues[i * 2, j * 2 + 1]);
                possibleValues.Add(newValues[i * 2 + 1, j * 2 + 2]);
                possibleValues.Add(newValues[i * 2 + 2, j * 2 + 1]);
                newValues[i * 2 + 1, j * 2 + 1] = possibleValues[UnityEngine.Random.Range(0, possibleValues.Count)];
            }
        }

        Values = newValues;
    }

    private int GetNewV2EdgeValueFor(int x, int y)
    {
        List<int> possibleValues = new List<int>();
        if(x%2 == 0 && y%2 ==1) // top and bottom
        {
            possibleValues.Add(Values[x / 2, y / 2]);
            possibleValues.Add(Values[x / 2, y / 2 + 1]);
        }
        else if(x%2==1 && y%2 == 0) // left and right
        {
            possibleValues.Add(Values[x / 2, y / 2]);
            possibleValues.Add(Values[x / 2 + 1, y / 2]);
        }

        return possibleValues[UnityEngine.Random.Range(0, possibleValues.Count)];
    }


}
