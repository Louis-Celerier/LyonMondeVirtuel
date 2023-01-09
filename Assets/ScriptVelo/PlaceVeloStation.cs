using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceVeloStation : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ToSpawn; // objet a spawn
    public double OffsetCoordX, OffsetCoordY; // offset des coordonnées
    public float Scale = 1; // scale des coordonnées;
    public Vector3 initialRotation = new Vector3(0, 0, 0); // rotation initial du model lorsqu'il est spawn
    public string ToRead = "Assets/ScriptVelo/data_parcstationvelo_3946.geojson"; // fichier Json a lire;
    public int OffsetJson = 0; // au cas ou les coordonnée ne soient pas au même endroit a partir de la fin du fichier, 
    void Start()
    {
        StreamReader file = new StreamReader("Assets/ScriptVelo/data_parcstationvelo_3946.geojson");

        // on saute les 6 première ligne du fichier qui ne servent a rien
        for (int i = 0; i < 5; i++)
        {
            string line = file.ReadLine(); 
        }

        //on lis le fichier
        while (!file.EndOfStream)
        {

            string line = file.ReadLine();
            string[] lSplit = line.Split(" ");

            //si la ligne est une ligne de donnée
            if (lSplit.Length > 2)
            {

                //On récupère les coordonnée
                double coor1 = FloatParser(lSplit[lSplit.Length - 5]); // on parse le string en double avec une fonction perso
                double coor2 = FloatParser(lSplit[lSplit.Length - 4]);
                double[] Coord = { (coor1 - OffsetCoordX) * Scale, (coor2 - OffsetCoordY) * Scale }; 

                //on les met dans un vector3
                Vector3 Pos = new Vector3((float)Coord[0], 0, (float)Coord[1]);


                //On instancie notre modèle
                Instantiate(ToSpawn, Pos, Quaternion.Euler(initialRotation.x, initialRotation.y, initialRotation.z)).transform.parent = transform;
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

