using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class ScorePop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField, Range(0.05f, 0.5f)] private float duration = 0.2f;
    [SerializeField, Range(1f, 1.3f)] private float peakScale = 1.12f;
    private Vector3 originalScale;
    private string previousText;
    private float remaining;

    private void OnEnable()
    {
        originalScale = scoreLabel.transform.localScale;
        previousText = scoreLabel.text;
    }

    private void LateUpdate()
    {
        if (scoreLabel.text != previousText)
        {
            previousText = scoreLabel.text;
            remaining = duration;
        }
        remaining = Mathf.Max(0, remaining - Time.unscaledDeltaTime);
        float progress = remaining / duration;
        scoreLabel.transform.localScale = originalScale * (1f + (peakScale - 1f) * Mathf.Sin(progress * Mathf.PI));
    }

    private void OnDisable()
    {
        if (scoreLabel) scoreLabel.transform.localScale = originalScale;
    }
}
