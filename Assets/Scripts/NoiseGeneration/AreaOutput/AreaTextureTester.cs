using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTextureTester : MonoBehaviour
{
    public GameObject Quad;

    private AreaNoiseGenerator AreaNoiseGenerator;

    // Start is called before the first frame update
    void Start()
    {
        //AreaNoiseGenerator = new BiomeGenerator(avgBiomeSize: 10f);
        AreaNoiseGenerator = new Voronoi();
    }

    // Update is called once per frame
    void Update()
    {
        AreaNoiseGenerator.TexturePlaneAtWorldPosition(Quad, 200);
    }
}
