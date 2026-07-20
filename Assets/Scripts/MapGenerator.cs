using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Données de la Map")]
    public int widthX = 10;
    public int heightZ = 10;
    public float tailleTuile = 1f;
    public List<GameObject> prefabTiles;
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

        //On crée la liste des futurs tiles possibles
        List<OptionTuile> validTiles = new List<OptionTuile>();

        //On regarde les prefabs corespondants
        foreach (GameObject prefab in prefabTiles)
        {
            //On récupère les caractéristiques du prefab
            PrefabConfig config = prefab.GetComponent<PrefabConfig>();

            //Sécurité si le script est manquant
            if (config == null) continue; 

            //On check les bordures
            for (int i = 0 ; i < 4 ; i++)
            {
                bool estValide = true;

                string bordureSudVirtuelle = config.ObtenirBordureApresRotation("Sud", i);
                string bordureOuestVirtuelle = config.ObtenirBordureApresRotation("Ouest", i);

                //Validation de la contrainte Sud
                if (contrainteSud != "" && bordureSudVirtuelle != contrainteSud) estValide = false;

                //Validation de la contrainte Ouest
                if (contrainteOuest != "" && bordureOuestVirtuelle != contrainteOuest) estValide = false;

                if (estValide) validTiles.Add(new OptionTuile(prefab, i));
            }
                
        }

        if (validTiles.Count > 0)
        {
            // Tirage au sort dans notre liste d'objets OptionTuile
            int randomIndex = Random.Range(0, validTiles.Count);
            OptionTuile optionChoisie = validTiles[randomIndex];

            // Calcul de la position 3D (X, Y=0, Z) 
            Vector3 positionTableau = new Vector3(x * tailleTuile, 0, z * tailleTuile);

            // Calcul de l'angle (0°, 90°, 180° ou 270°) autour de l'axe vertical (Y)
            float angleAngle = optionChoisie.nbRotations * 90f;
            Quaternion rotationFinale = Quaternion.AngleAxis(angleAngle, Vector3.up);

            // Instanciation du GameObject avec le prefab et la rotation calculée
            grilleMap[x, z] = Instantiate(optionChoisie.prefab, positionTableau, rotationFinale);
        }

        else
        {
            Debug.LogWarning($"Blocage aux coordonnées ({x}, {z}) : aucune tuile ne correspond aux contraintes !");
        }
    }
}


public class OptionTuile
{
    public GameObject prefab;  // Le prefab d'origine
    public int nbRotations;    // 0 = 0°, 1 = 90°, 2 = 180°, 3 = 270°

    public OptionTuile(GameObject prefabOriginal, int rotations)
    {
        this.prefab = prefabOriginal;
        this.nbRotations = rotations;
    }
}