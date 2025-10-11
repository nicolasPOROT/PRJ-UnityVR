using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Iterables
    public int lifes;
    public int score = 0;

    // UI updating
    public GameObject[] lifeUI;
    public TextMeshProUGUI scoreUi;

    // Combo logic
    [SerializeField] private float comboEndTime = 1f;
    [SerializeField] private float comboTimer;
    [SerializeField] private bool inCombo;
    [SerializeField] private int scoreToAdd;

    // Canons
    [SerializeField] private CanonShoot[] canons;

    private void Start()
    {
        lifes = lifeUI.Length;

        foreach (GameObject life in lifeUI)
        {
            life.SetActive(true);
        }

        inCombo = false;
        scoreToAdd = 1;
        comboTimer = comboEndTime;
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

        ManageCombo();
    }

    public void LoseLife()
    {
        if (lifes == 0) { return; }
        lifes--;
        lifeUI[lifes].SetActive(false);
    }

    public void GainScore()
    {
        if (inCombo)
        {
            scoreToAdd++;
        }
        else
        {
            scoreToAdd = 1;
            inCombo = true;
            comboTimer = comboEndTime;
        }
    }

    private void ManageCombo()
    {
        if (comboTimer <= 0 && inCombo)
        {
            inCombo = false;

            // Add score at the end of the combo
            if(scoreToAdd == 1)
            {
                score++; // No combo
            }
            else
            {
                score += scoreToAdd * 2; // Combo multiplier
            }
            scoreUi.text = "" + score; // Update UI accordingly
        }
        else
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0) { comboTimer = 0; }
        }
    }
}
