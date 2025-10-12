using System.Collections;
using UnityEngine;

public class CononsManager : MonoBehaviour
{
    // Canons
    [SerializeField] private CanonShoot[] canons;

    private int difficulty = 0;

    private float timer;
    private float timeToShoot;
    
    void Start()
    {
        if (DifficultyTransfert.Instance != null)
        {
            // Comme dans GameManager
            difficulty = DifficultyTransfert.Instance.difficultyLevel;
        }

        if (difficulty == 4) // faster shooting speed for extreme difficulty
        {
            timeToShoot = 0.85f;
        }
        else
        {
            timeToShoot = 1.2f;
        }
        
        timer = timeToShoot;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            RandomCanonShoot();
            timer = timeToShoot;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void RandomCanonShoot()
    {
        if (difficulty == 0 || difficulty == 2) // One fruit at the time for difficulties 0 and 2
        {
            canons[Random.Range(0, canons.Length)].Shoot();
        }
        else  // Two fruits for gamemode 1 and 3
        {
            canons[Random.Range(0, canons.Length)].Shoot();
            StartCoroutine(ShootWithDelay());
        }
    }

    // Shoot with delay in case 2 fruits shoot at the same time from the same canon
    private IEnumerator ShootWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        canons[Random.Range(0, canons.Length)].Shoot();
    }
}
