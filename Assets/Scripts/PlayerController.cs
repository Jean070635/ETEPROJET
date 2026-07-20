using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Réglages du mouvement")]
    public float moveSpeed = 5.0f;     // Vitesse de déplacement (mètres par seconde)
    public float rotateSpeed = 120.0f; // Vitesse de rotation (degrés par seconde)

    void Start()
    {
        transform.position = new Vector3(0, 3, 0);
    }
    void Update()
    {
        // 1. Récupérer les entrées du joueur (-1 à 1)      
        float moveInput = Input.GetAxis("Vertical"); // Vertical = Flèches Haut/Bas (ou Z/S)
        float rotateInput = Input.GetAxis("Horizontal"); // Horizontal = Flèches Gauche/Droite (ou Q/D)

        // 2. Calculer et appliquer le déplacement (Avancer / Reculer)        
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.deltaTime; // transform.forward correspond à la direction "devant" locale du joueur
        transform.position += movement;

        // 3. Calculer et appliquer la rotation (Pivoter à gauche / droite)        
        float rotationAmount = rotateInput * rotateSpeed * Time.deltaTime; // On tourne autour de l'axe vertical (Y)
        transform.Rotate(0, rotationAmount, 0);
    }
}