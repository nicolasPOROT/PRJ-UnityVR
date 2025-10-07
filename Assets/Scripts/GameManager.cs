using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Iterables
    public int lifes = 3;
    public int score = 0;

    // Canons
    [SerializeField] private CanonShoot[] canons;

    void Update()
    {
        if (lifes <= 0)
        {
            Debug.Log("Game Over");
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            canons[Random.Range(0, canons.Length)].Shoot();
        }
    }

    private void UpdateUi()
    {
        // Update lifes and score shown somewhere in the scene
    }
}
