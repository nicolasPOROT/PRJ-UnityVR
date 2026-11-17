using TMPro;
using UnityEngine;

public sealed class FpsOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private bool showOverlay;
    [SerializeField, Range(0.2f, 2f)] private float sampleSeconds = 0.5f;
    private float elapsed;
    private int frames;

    private void Update()
    {
        label.enabled = showOverlay;
        if (!showOverlay) { elapsed = 0; frames = 0; return; }
        elapsed += Time.unscaledDeltaTime;
        frames++;
        if (elapsed < sampleSeconds) return;
        label.SetText("FPS : {0:0}", frames / elapsed);
        elapsed = 0;
        frames = 0;
    }

    private void OnDisable()
    {
        if (label) label.enabled = false;
    }
}
