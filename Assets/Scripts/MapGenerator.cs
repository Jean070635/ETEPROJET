using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Donnéesde la Map")]
    public int widthX = 10;
    public int heightZ = 10;
    public float tailleTuile = 1f;
    public List<GameObject> prefabsTiles;

    private GameObject[,] grilleMap;

    void Start()
    {
        GenererMap();
    }

    private void GenererMap()
    {
        grilleMap = new GameObject[widthX, heightZ];

        for (int x = 0; x < widthX; x++)
        {
            for (int z = 0; z < heightZ; z++)
            {
                TrouverEtPlacerTuile(x, z);
            }
        }

  
}
private void TrouverEtPlacerTuile(int x, int z)
    {
        string contrainteSud = "";
        string contrainteOuest = "";

        //On regarde quelles bordures sont nécessaires pour ces coordonnées
        //On regarde au Sud de la tuile -> bordure Nord du voisin Sud
        if (z > 0 && grilleMap[x, z - 1] != null)
        {
            PrefabConfig voisinSud = grilleMap[x, z - 1].GetComponent<PrefabConfig>();
            contrainteSud = voisinSud.bordureNord;
        }

        //On regarde à l'Ouest de la tuile -> bordure Est du voisin Ouest
        if (x > 0 && grilleMap[x - 1, z] != null)
        {
            PrefabConfig voisinOuest = grilleMap[x - 1, z].GetComponent<PrefabConfig>();
            contrainteOuest = voisinOuest.bordureEst;
        }


        //On regarde quels tiles ont les bordures correspondantes
        List<GameObject> validTiles = new List<GameObject>();
       
        foreach (GameObject prefab in prefabsTiles)
        {
            bool estValide = true;

            PrefabConfig config = prefab.GetComponent<PrefabConfig>();
            
            if (config == null) continue; //Sécurité si on a oublié le script sur un prefab
            
            //Vérification de la contrainte Sud
            if (contrainteSud != "" && config.bordureSud != contrainteSud) estValide = false;
            //Vérification de la contrainte Ouest
            if (contrainteOuest != "" && config.bordureOuest != contrainteOuest) estValide = false;

            if (estValide) validTiles.Add(prefab);
        }

        if (validTiles.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, validTiles.Count);

            GameObject prefabChoisi = validTiles[randomIndex];

            Vector3 positionTableau = new Vector3(x * tailleTuile, 0, z * tailleTuile);

            GameObject nouvelleTuile = Instantiate(prefabChoisi, positionTableau, Quaternion.identity);

            grilleMap[x, z] = nouvelleTuile;
        }

        else
        {
            Debug.LogWarning($"Blocage aux coordonnées ({x}, {z}) : aucune tuile ne correspond aux contraintes !");
        }


    }
}