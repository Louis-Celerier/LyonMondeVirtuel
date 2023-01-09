using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(1,100)]
    public int precision;

    string filename = "vegetationGratteCiel.geojson";

    public GameObject arbre1;
    public GameObject arbre2;
    public GameObject arbre3;

    [Range(0, 10325)]
    public int nbS1;

    [Range(0, 8597)]
    public int nbS2;

    [Range(0, 9812)]
    public int nbS3;

    public float scale;

    void Start()
    {
        ReaderJSON rd = new ReaderJSON();
        JSONNode data = rd.CreateFromJSON(filename);
        Dictionary<int, List<List<Vector3>>> dico = rd.Read(data, precision, nbS1, nbS2, nbS3);
        rd.PlantTrees(dico, this.transform, arbre1, arbre2, arbre3, scale);

        Debug.Log("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
