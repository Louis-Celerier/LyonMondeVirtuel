using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ReaderJSON
{
    public JSONNode CreateFromJSON(string filename)
    {
        Debug.Log("Loading data");
        string path = Application.dataPath + "/Resources/" + filename;
        string jsonString = File.ReadAllText(path);
        return JSON.Parse(jsonString);
    }

    public Dictionary<int, List<List<Vector3>>> Read(JSONNode data, int precision, int nbS1, int nbS2, int nbS3)
    {
        Debug.Log("Creation polygones");
        int cptS1 = 0;
        int cptS2 = 0;
        int cptS3 = 0;

        int cptS1P = 0;
        int cptS2P = 0;
        int cptS3P = 0;

        bool runningS1 = cptS1 < nbS1;
        bool runningS2 = cptS2 < nbS2;
        bool runningS3 = cptS3 < nbS3;

        Dictionary<int, List<List<Vector3>>> dico = new Dictionary<int, List<List<Vector3>>>(); 

        foreach (JSONNode features in data["features"])
        {
            if ((runningS1 && features["properties"]["strate"] == 1) || (runningS2 && features["properties"]["strate"] == 2) || (runningS3 && features["properties"]["strate"] == 3))
            {
                List<Vector3> points = new List<Vector3>();

                if (runningS1 && features["properties"]["strate"] == 1)
                {
                    cptS1P = 1;
                }
                else if (runningS2 && features["properties"]["strate"] == 2)
                {
                    cptS2P = 1;
                }
                else if (runningS3 && features["properties"]["strate"] == 3)
                {
                    cptS3P = 1;
                }
                else
                {
                    break;
                }

                if (!dico.ContainsKey(features["properties"]["strate"]))
                {
                    dico[features["properties"]["strate"]] = new List<List<Vector3>>();
                }

                foreach (JSONNode geometry in features["geometry"])
                {
                    foreach (JSONNode first in geometry.Children)
                    {

                        foreach (JSONNode second in first.Children)
                        {
                            int j = 0;
                            foreach (JSONNode third in second.Children)
                            {
                                if (j == precision)
                                {
                                    //Debug.Log(third.Children.ToArray()[0] + " " + third.Children.ToArray()[2] + " " + third.Children.ToArray()[1]);
                                    points.Add(new Vector3(1844620 - third.Children.ToArray()[0], 0, 5171660 - third.Children.ToArray()[1]));
                                    j = 0;
                                }
                                else
                                {
                                    j++;
                                }
                            }
                        }
                    }
                }
                if (points.Count < 3)
                {
                    cptS1P = 0;
                    cptS2P = 0;
                    cptS3P = 0;
                }
                else
                {
                    points.Add(points[0]);
                    dico[features["properties"]["strate"]].Add(points);
                }

                cptS1 += cptS1P;
                cptS2 += cptS2P;
                cptS3 += cptS3P;
                runningS1 = cptS1 < nbS1;
                runningS2 = cptS2 < nbS2;
                runningS3 = cptS3 < nbS3;
            }
            else
            {
                if (!runningS1 && !runningS2 && !runningS3)
                {
                    break;
                }
            }
        }
        return dico;
    }

    public void PlantTrees(Dictionary<int, List<List<Vector3>>> dico, Transform transform, GameObject a1, GameObject a2, GameObject a3, float scale)
    {
        Debug.Log("Plantation trees");

        foreach (int strate in dico.Keys)
        {
            foreach (List<Vector3> list in dico[strate])
            {
                (Vector3, Vector3) tuple = getMinAndMaxVector3FromList(list);
                float lenghtX = Mathf.Abs(Mathf.Abs(tuple.Item1.x) - Mathf.Abs(tuple.Item2.x));
                float lenghtY = Mathf.Abs(Mathf.Abs(tuple.Item1.z) - Mathf.Abs(tuple.Item2.z));
                int aire = (int)((lenghtX * lenghtY) / 100);

                for (int i = 0; i < aire; i++)
                {
                    float ranromX = Random.Range(tuple.Item1.x, tuple.Item2.x);
                    float ranromZ = Random.Range(tuple.Item1.z, tuple.Item2.z);
                    Vector3 randomPosition = new Vector3(ranromX, 0, ranromZ);
                    if (GeometryUtils.PointInPolygon(randomPosition, list))
                    {
                       // GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                       // go.transform.parent = transform;
                       // go.transform.position = randomPosition;
                        float ranromHeight = Random.Range(2, 5);
                        if(strate == 1)
                        {
                            GameObject go = GameObject.Instantiate(a1, Vector3.zero, Quaternion.identity);
                            go.transform.parent = transform;
                            go.transform.localPosition = randomPosition;
                            go.transform.localScale = Vector3.one * scale;

                        }
                        else if (strate == 2)
                        {
                            GameObject go = GameObject.Instantiate(a2, Vector3.zero, Quaternion.Euler(-90,0,0));
                            go.transform.parent = transform;
                            go.transform.localPosition = randomPosition;
                            go.transform.localScale = Vector3.one * scale;
                        }
                        else
                        {
                            GameObject go = GameObject.Instantiate(a3, Vector3.zero, Quaternion.identity);
                            go.transform.parent = transform;
                            go.transform.localPosition = randomPosition;
                            go.transform.localScale = Vector3.one * scale;
                        }
                    }
                }
            }
        }
    }

    public (Vector3, Vector3) getMinAndMaxVector3FromList(List<Vector3> list) {
        Vector3 min_val = new Vector3(list[0].x, 0, list[0].z);
        Vector3 max_val = new Vector3(list[0].x, 0, list[0].z);

        foreach (Vector3 v in list)
        {
            if(min_val.x > v.x)
            {
                min_val.x = v.x;
            }
            if (min_val.z > v.z)
            {
                min_val.z = v.z;
            }

            if (max_val.x < v.x)
            {
                max_val.x = v.x;
            }
            if (max_val.z < v.z)
            {
                max_val.z = v.z;
            }
        }
        return (min_val, max_val);
    }

}

