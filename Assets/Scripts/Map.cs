using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] itemsToPickFrom; // tableau des élements à spawn
    public int gridX; //Longueur de la grille
    public int gridZ; //Largeur de la grille
    public Vector3 gridOrigin = Vector3.zero; //Origine de la grille

    void Start()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x, 0, z) + gridOrigin; //On définit la position de chaque GameObject dans la grille (on csd que la taille d'un GameObect est de 1x1)
                PickAndSpawn(spawnPosition, Quaternion.identity); //Génère un GameObect aléatoire à la position SpawnPosition, sans rotation
            }

        }
    }

    private void PickAndSpawn(Vector3 position, Quaternion rotation)
    {
        int randomIndex = Random.Range(0, itemsToPickFrom.Length); //Choisit un indice random dans la grille
        Instantiate(itemsToPickFrom[randomIndex], position, rotation); //Génère un GameObect aléatoire
    }




}
