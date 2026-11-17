using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameOverPresentation : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI finalScoreLabel;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CononsManager canonManager;
    [SerializeField] private SliceObject[] blades;
    public bool IsVisible => panel.activeSelf;

    private void Start() => panel.SetActive(false);

    public void Show(int score)
    {
        finalScoreLabel.SetText("Score final : {0}", score);
        panel.SetActive(true);
        canonManager.enabled = false;
        foreach (var blade in blades) blade.enabled = false;
        gameManager.enabled = false;
    }

    public void Replay() => SceneManager.LoadScene("PlayScene");
    public void ReturnToMenu() => SceneManager.LoadScene("MainMenu");
}
