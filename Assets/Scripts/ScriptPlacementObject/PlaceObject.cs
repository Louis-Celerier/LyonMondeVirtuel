using System.IO;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ToSpawn; // objet a spawn
    public double OffsetCoordX, OffsetCoordY; // offset des coordonn�es
    public float ScaleCoord = 1; // scale des coordonn�es;
    public float Scale = 1;
    public Vector3 initialRotation = new Vector3(0, 0, 0); // rotation initial du model lorsqu'il est spawn
    public string ToRead = "Assets/ScriptPlacementObject/data_parcstationvelo.geojson"; // fichier Json a lire;
    public int OffsetJson = 0; // au cas ou les coordonn�e ne soient pas au m�me endroit a partir de la fin du fichier, 
    void Start()
    {
        StreamReader file = new StreamReader(ToRead);

        // on saute les 6 premi�re ligne du fichier qui ne servent a rien
        for (int i = 0; i < 5; i++)
        {
            string line = file.ReadLine(); 
        }

        //on lis le fichier
        while (!file.EndOfStream)
        {

            string line = file.ReadLine();
            string[] lSplit = line.Split(" ");

            //si la ligne est une ligne de donn�e
            if (lSplit.Length > 2)
            {

                //On r�cup�re les coordonn�e
                double coor1 = FloatParser(lSplit[lSplit.Length - 5 + OffsetJson]); // on parse le string en double avec une fonction perso
                double coor2 = FloatParser(lSplit[lSplit.Length - 4 + OffsetJson]);
                string Name = "";
                int j = 8;
                while(lSplit[j] != "\"commune\":"){
                    Name += lSplit[j];
                    Name += " ";
                    j++;
                }
                Name += lSplit[j+1];
                Debug.Log(Name);
                double[] Coord = { (coor1 - OffsetCoordX) * ScaleCoord, (coor2 - OffsetCoordY) * ScaleCoord }; 

                //on les met dans un vector3
                Vector3 Pos = new Vector3((float)Coord[0], 0, (float)Coord[1]);


                //On instancie notre mod�le
                GameObject go = Instantiate(ToSpawn, Pos, Quaternion.Euler(initialRotation.x, initialRotation.y, initialRotation.z));
                go.name = Name;
                go.transform.localScale = new Vector3(Scale, Scale, Scale);
            }

        }



        file.Close();
    }

    double FloatParser(string inStr) //Parse un string en double parceque les fonction par default ne fontionnent pas.
    {
        string[] inSplit = inStr.Split(".");
        string wholeStr = inSplit[0];
        string decStr = inSplit[1];

        double whole = float.Parse(wholeStr);
        double dec = float.Parse(decStr);

        while (dec >= 1)
        {
            dec = dec / 10;
        }

        return whole + dec;
    }
}

