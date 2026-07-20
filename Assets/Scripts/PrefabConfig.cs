using UnityEngine;

public class PrefabConfig : MonoBehaviour
{
    [Header("Identifiant des bordures")]
    public string bordureNord;
    public string bordureEst;
    public string bordureSud;
    public string bordureOuest;

    [Header("Données supplémentaires")]
    public string BiomeType;
    public int dimension;
}
