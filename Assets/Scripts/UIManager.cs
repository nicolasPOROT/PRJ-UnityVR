using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] lifeImages; // 3 images représentant les vies

    private GameManager gm;

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
        // Vies
        for (int i = 0; i < 3; i++)
        {
            lifeImages[i].SetActive(true);
        }
    }

    public void UpdateUI()
    {
        if (gm == null) return;

        // Score
        scoreText.text = "Score : " + gm.score;

        
    }
}
