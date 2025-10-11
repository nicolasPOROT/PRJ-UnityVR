using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Iterables
    public int lifes = 3;
    public int score = 0;
    public GameObject[] lifeUI;

    // Canons
    [SerializeField] private CanonShoot[] canons;

    private void Start()
    {
        foreach (GameObject life in lifeUI){
            life.SetActive(true);
        }
    }

    void Update()
    {
        if (lifes <= 0)
        {
            Debug.Log("Game Over");
        }

        // Pour tester
        if (Input.GetKeyDown(KeyCode.Space))
        {
            canons[Random.Range(0, canons.Length)].Shoot();
        }
    }

    public void LoseLife()
    {
        if(lifes == 0){ return; }
        lifes--;
        lifeUI[lifes].SetActive(false);
    }
}
