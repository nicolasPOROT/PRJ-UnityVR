using UnityEngine;
using UnityEngine.InputSystem;

public class CanonShoot : MonoBehaviour
{
    public Transform shootPos;
    private float shootForce = 10f;
    [SerializeField] private float bombProb = 0.2f; // Probability of bomb spawn
    [SerializeField] private GameObject[] fruits;
    [SerializeField] private GameObject[] bombs;


    public void Shoot()
    {
        GameObject projectile;

        // Random fruit or bomb
        float randomFloat = Random.Range(0f, 1f);

        //random rotation
        float randomX = Random.Range(0f, 360f);
        float randomY = Random.Range(0f, 360f);
        float randomZ = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(randomX, randomY, randomZ);

        if (randomFloat <= bombProb) // Shoot Random Bomb with probability
        {
            int randomBomb = Random.Range(0, bombs.Length);
            projectile = Instantiate(bombs[randomBomb], shootPos.position, randomRotation);
        }
        else // Shoot Random Fruit
        {
            int randomFruit = Random.Range(0, fruits.Length);
            projectile = Instantiate(fruits[randomFruit], shootPos.position, randomRotation);
        }

        projectile.GetComponent<Rigidbody>().AddForce(shootPos.up * shootForce, ForceMode.Impulse);
    }
}