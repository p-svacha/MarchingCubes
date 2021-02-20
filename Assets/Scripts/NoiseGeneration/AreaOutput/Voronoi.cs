using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voronoi : AreaNoiseGenerator
{
    private Hash Hash;
    private Hash BiomeHash;
    private int Resolution = 50;

    Dictionary<Vector2, int> Values = new Dictionary<Vector2, int>();

    public Voronoi()
    {
        Hash = new Hash(new RandomNumberGenerator(), 256, 10);
        BiomeHash = new Hash(new RandomNumberGenerator(), 10, 20);
    }

    public override int GetNoiseValueAt(Vector3 worldPosition)
    {
        Vector2 worldPosition2D = new Vector2((int)(worldPosition.x), (int)(worldPosition.z));
        if (Values.ContainsKey(worldPosition2D)) return Values[worldPosition2D];
        Vector2 scaledPosition2D = new Vector2((int)(worldPosition.x/Resolution), (int)(worldPosition.z/Resolution));

        List<Vector2> voronoiPoints = GetVoronoiPointsAround(scaledPosition2D, 40);
        if (voronoiPoints.Count == 0) Debug.Log("Range too little");

        Vector2 nearestPoint = new Vector2(0, 0);
        float nearestDistance = float.MaxValue;
        foreach(Vector2 vPoint in voronoiPoints)
        {
            float distance = Vector2.Distance(vPoint, worldPosition2D);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPoint = vPoint;
            }
        }

        int pointHash = BiomeHash.GetHashedValue((int)nearestPoint.x, (int)nearestPoint.y);

        Values.Add(worldPosition2D, pointHash);
        return pointHash;
    }

    private List<Vector2> GetVoronoiPointsAround(Vector2 position, int range)
    {
        List<Vector2> points = new List<Vector2>();
        for(int y = -range; y < range+1; y++)
        {
            for(int x = -range; x < range+1; x++)
            {
                int worldX = (int)position.x + x;
                int worldY = (int)position.y + y;
                int hashedValue = Hash.GetHashedValue(worldX, worldY);
                if (hashedValue <= 0) points.Add(new Vector2(worldX*Resolution, worldY*Resolution));
            }
        }
        return points;
    }


}
