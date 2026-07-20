using UnityEngine;

public class PrefabConfig : MonoBehaviour
{
    [Header("Identifiant initial des bordures")]
    public string bordureNord;
    public string bordureEst;
    public string bordureSud;
    public string bordureOuest;

    [Header("Données supplémentaires")]
    public string BiomeType;
    public int dimension;

    /// <summary>
    /// Renvoie la valeur d'une bordure après avoir appliqué une rotation virtuelle de X fois 90°.
    /// Permet de tester la tuile sans modifier directement ses variables.
    /// </summary>
    public string ObtenirBordureApresRotation(string directionDemandee, int nbRotations)
    {

        // Dans le sens des aiguilles d'une montre : Nord (0), Est (1), Sud (2), Ouest (3)

        int indexBordure = 0;
        if (directionDemandee == "Nord") indexBordure = 0;
        else if (directionDemandee == "Est") indexBordure = 1;
        else if (directionDemandee == "Sud") indexBordure = 2;
        else if (directionDemandee == "Ouest") indexBordure = 3;

        int indexApresRotation = (indexBordure - nbRotations) % 4;
        if (indexApresRotation < 0) indexApresRotation += 4;

        switch (indexApresRotation)
        {
            case 0:
                return bordureNord;
            case 1:
                return bordureEst;
            case 2:
                return bordureSud;
            case 3:
                return bordureOuest;
            default: 
                return "";
        }
    }
}