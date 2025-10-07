using UnityEngine;
using UnityEngine.InputSystem;

public class CanonShoot : MonoBehaviour
{
    public Transform shootPos;
    private float shootForce = 10f;
    private float bombProb = 0.2f; // Probability of bomb spawn
    [SerializeField] private GameObject fruit;
    [SerializeField] private GameObject bomb;


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

        if (randomFloat <= bombProb)
        {
            projectile = Instantiate(bomb, shootPos.position, randomRotation);
        }
        else
        {
            projectile = Instantiate(fruit, shootPos.position, randomRotation);
        }

        projectile.GetComponent<Rigidbody>().AddForce(shootPos.up * shootForce, ForceMode.Impulse);
    }
}