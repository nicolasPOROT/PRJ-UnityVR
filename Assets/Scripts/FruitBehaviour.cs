using UnityEngine;

public class FruitBehaviour : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("fruitKillZone"))
        {
            gm.lifes--;
            Destroy(gameObject);
        }
    }

    // If sliced : gm.score++
}
