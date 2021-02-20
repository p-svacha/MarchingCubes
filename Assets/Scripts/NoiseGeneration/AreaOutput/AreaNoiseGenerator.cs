using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaNoiseGenerator
{
    public abstract int GetNoiseValueAt(Vector3 worldPosition);

    #region Preview
    public void TexturePlaneAtWorldPosition(GameObject plane, int resolution)
    {
        Texture2D texture = new Texture2D(resolution, resolution);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        Vector3 point00 = plane.transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = plane.transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = plane.transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = plane.transform.TransformPoint(new Vector3(0.5f, 0.5f));

        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                texture.SetPixel(x, y, GetColorFor(GetNoiseValueAt(point)));
                //texture.SetPixel(x, y, new Color(point.x/100f, point.y/100f, point.z/100f));
            }
        }


        texture.Apply();

        MeshRenderer renderer = plane.GetComponent<MeshRenderer>();
        renderer.material.mainTexture = texture;
    }

    protected Color GetColorFor(int id)
    {
        switch (id)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.blue;
            case 2:
                return Color.green;
            case 3:
                return Color.yellow;
            case 4:
                return Color.white;
            case 5:
                return Color.black;
            case 6:
                return new Color(1, 0.65f, 0);
            case 7:
                return new Color(1, 0, 1);
            case 8:
                return new Color(0, 1, 1);
            case 9:
                return new Color(0.3f, 0.9f, 0.4f);
            default:
                return Color.gray;
        }
    }
    #endregion
}
