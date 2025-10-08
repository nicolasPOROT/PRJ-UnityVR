using UnityEngine;

public class BombBehaviour : MonoBehaviour
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
            Destroy(gameObject);
        }
    }

    public void IsSliced()
    {
        gm.LoseLife();
    }
}
