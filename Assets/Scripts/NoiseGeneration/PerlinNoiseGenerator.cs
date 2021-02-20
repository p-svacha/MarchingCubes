using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TerrainGenerator;

public class PerlinNoiseGenerator : NoiseGenerator
{
    private float Scale;
    private int NumOctaves;
    private float Persistance;
    private float Lacunarity;

    private Vector3[] OctaveOffsets;

    public PerlinNoiseGenerator(
        float scale = 10f, 
        int numOctaves = 6, 
        float persistance = 0.5f, 
        float lacunarity = 2f) : base()
    {
        Scale = scale;
        NumOctaves = numOctaves;
        Persistance = persistance;
        Lacunarity = lacunarity;

        OctaveOffsets = new Vector3[NumOctaves];
        for (int i = 0; i < NumOctaves; i++)
        {
            float offsetX = RNG.Next(-100, 100);
            float offsetY = RNG.Next(-100, 100);
            float offsetZ = RNG.Next(-100, 100);
            OctaveOffsets[i] = new Vector3(offsetX, offsetY, offsetZ);
        }
    }

    public override float GetNoiseValueAt(Vector3 worldPosition)
    {
        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        // Generate value for each pixel going through n octaves with x persistance and y lacunarity
        for (int i = 0; i < NumOctaves; i++)
        {
            float sampleX = worldPosition.x / Scale * frequency + OctaveOffsets[i].x;
            float sampleY = worldPosition.y / Scale * frequency + OctaveOffsets[i].y;
            float sampleZ = worldPosition.z / Scale * frequency + OctaveOffsets[i].z;

            float perlinValue = Perlin3D(new Vector3(sampleX, sampleY, sampleZ));
            noiseHeight += perlinValue * amplitude;

            amplitude *= Persistance;
            frequency *= Lacunarity;
        }

        return noiseHeight;
    }

    private float Perlin3D(Vector3 point)
    {
        int ix0 = Mathf.FloorToInt(point.x);
        int iy0 = Mathf.FloorToInt(point.y);
        int iz0 = Mathf.FloorToInt(point.z);
        float tx0 = point.x - ix0;
        float ty0 = point.y - iy0;
        float tz0 = point.z - iz0;
        float tx1 = tx0 - 1f;
        float ty1 = ty0 - 1f;
        float tz1 = tz0 - 1f;
        ix0 &= hashMask;
        iy0 &= hashMask;
        iz0 &= hashMask;
        int ix1 = ix0 + 1;
        int iy1 = iy0 + 1;
        int iz1 = iz0 + 1;

        int h0 = hash[ix0];
        int h1 = hash[ix1];
        int h00 = hash[h0 + iy0];
        int h10 = hash[h1 + iy0];
        int h01 = hash[h0 + iy1];
        int h11 = hash[h1 + iy1];
        Vector3 g000 = gradients3D[hash[h00 + iz0] & gradientsMask3D];
        Vector3 g100 = gradients3D[hash[h10 + iz0] & gradientsMask3D];
        Vector3 g010 = gradients3D[hash[h01 + iz0] & gradientsMask3D];
        Vector3 g110 = gradients3D[hash[h11 + iz0] & gradientsMask3D];
        Vector3 g001 = gradients3D[hash[h00 + iz1] & gradientsMask3D];
        Vector3 g101 = gradients3D[hash[h10 + iz1] & gradientsMask3D];
        Vector3 g011 = gradients3D[hash[h01 + iz1] & gradientsMask3D];
        Vector3 g111 = gradients3D[hash[h11 + iz1] & gradientsMask3D];

        float v000 = Dot(g000, tx0, ty0, tz0);
        float v100 = Dot(g100, tx1, ty0, tz0);
        float v010 = Dot(g010, tx0, ty1, tz0);
        float v110 = Dot(g110, tx1, ty1, tz0);
        float v001 = Dot(g001, tx0, ty0, tz1);
        float v101 = Dot(g101, tx1, ty0, tz1);
        float v011 = Dot(g011, tx0, ty1, tz1);
        float v111 = Dot(g111, tx1, ty1, tz1);

        float tx = Smooth(tx0);
        float ty = Smooth(ty0);
        float tz = Smooth(tz0);
        return Mathf.Lerp(
            Mathf.Lerp(Mathf.Lerp(v000, v100, tx), Mathf.Lerp(v010, v110, tx), ty),
            Mathf.Lerp(Mathf.Lerp(v001, v101, tx), Mathf.Lerp(v011, v111, tx), ty),
            tz);
    }

    private static float Dot(Vector3 g, float x, float y, float z)
    {
        return g.x * x + g.y * y + g.z * z;
    }

    private static Vector3[] gradients3D = {
        new Vector3( 1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3( 1f,-1f, 0f),
        new Vector3(-1f,-1f, 0f),
        new Vector3( 1f, 0f, 1f),
        new Vector3(-1f, 0f, 1f),
        new Vector3( 1f, 0f,-1f),
        new Vector3(-1f, 0f,-1f),
        new Vector3( 0f, 1f, 1f),
        new Vector3( 0f,-1f, 1f),
        new Vector3( 0f, 1f,-1f),
        new Vector3( 0f,-1f,-1f),

        new Vector3( 1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3( 0f,-1f, 1f),
        new Vector3( 0f,-1f,-1f)
    };

    private const int gradientsMask3D = 15;



}