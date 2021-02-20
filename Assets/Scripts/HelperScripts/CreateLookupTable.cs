using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateLookupTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/Resources/rawLookupTable.txt";

        List<string> lines = new List<string>();

        string line;
        System.IO.StreamReader file = new System.IO.StreamReader(path, System.Text.Encoding.GetEncoding("iso-8859-1"));
        int counter = 0;
        while ((line = file.ReadLine()) != null)
        {
            if (line.Trim() != "")
            {
                Debug.Log("Line: " + line);
                string newLine = "{ ";
                newLine += counter;
                newLine += ", new List<List<int>>() {";

                string[] splitted = line.Split(',');
                foreach(string triangle in splitted)
                {
                    if (triangle != "")
                    {
                        Debug.Log("Triangle: " + triangle);
                        List<int> edgeIds = new List<int>();
                        string[] triangleSplitted = triangle.Split(' ');
                        foreach (string edgeId in triangleSplitted)
                        {
                            int edgeIdInt;
                            if (int.TryParse(edgeId, out edgeIdInt) && edgeIds.Count < 3)
                            {
                                edgeIds.Add(edgeIdInt);
                            }
                        }
                        newLine += "new List<int>() {";
                        newLine += edgeIds[0] + ", ";
                        newLine += edgeIds[1] + ", ";
                        newLine += edgeIds[2] + "}, ";
                    }
                }
                newLine = newLine.TrimEnd(',');
                newLine += "} },";
                counter++;

                lines.Add(newLine);
            }
        }
        file.Close();

        
        using (System.IO.StreamWriter file2 =
            new System.IO.StreamWriter("Assets/Resources/tmp.txt"))
        {
            foreach (string line2 in lines)
            {
                file2.WriteLine(line2);
            }
        }
        
    }

}
