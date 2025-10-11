using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Iterables
    public int lifes;
    public int score = 0;

    // UI updating
    public GameObject[] lifeUI;
    public TextMeshProUGUI scoreUi;

    // Combo logic
    private float comboEndTime = 0.8f;
    private float comboTimer;
    private bool inCombo;
    private int scoreToAdd;

    // Difficulty ( 0 -> 4 )
    public int difficulty;

    private void Start()
    {
        if (DifficultyTransfert.Instance != null)
        {
            // Transférer la valeur stockée dans la variable locale 'difficulty'
            difficulty = DifficultyTransfert.Instance.difficultyLevel;
        }

        lifes = lifeUI.Length;

        foreach (GameObject life in lifeUI)
        {
            life.SetActive(true);
        }

        inCombo = false;
        scoreToAdd = 1;
        comboTimer = comboEndTime;
    }

    private void Update()
    {
        if (lifes <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("MainMenu");
        }

        ManageCombo();
    }

    public void LoseLife()
    {
        if (lifes == 0) { return; }
        lifes--;
        lifeUI[lifes].SetActive(false);
    }

    // On difficulty 0 and 1, missing a fruit doesn't lose a life
    public void FruitFall()
    {
        if (difficulty >= 2)
        {
            LoseLife();
        }
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
