using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Slider difficultySlider;

    void Update()
    {
        // Enregistrement de la difficulté dans le singleton
        if (DifficultyTransfert.Instance != null)
        {
            DifficultyTransfert.Instance.difficultyLevel = Mathf.RoundToInt(difficultySlider.value);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
