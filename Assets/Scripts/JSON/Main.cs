using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update

    [Range(1,100)]
    public int precision;

    public Material strate1;
    public Material strate2;
    public Material strate3;

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

    void Start()
    {
        ReaderJSON rd = new ReaderJSON();
        JSONNode data = rd.CreateFromJSON(filename);
        Dictionary<int, List<List<Vector3>>> dico = rd.Read(data, precision, nbS1, nbS2, nbS3);
        rd.PlantTrees(dico, this.transform, strate1, strate2, strate3, arbre1, arbre2, arbre3);

        Debug.Log("End");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
