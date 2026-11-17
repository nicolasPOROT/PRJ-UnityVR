using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class VolumeMenu : MonoBehaviour
{
    private const string VolumeKey = "FruitNinjaVR.AudioVolume";
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeLabel;

    private void Start()
    {
        float volume = Mathf.Clamp01(PlayerPrefs.GetFloat(VolumeKey, 1f));
        volumeSlider.SetValueWithoutNotify(volume);
        SetVolume(volume);
    }

    public void SetVolume(float value)
    {
        float volume = Mathf.Clamp01(value);
        AudioListener.volume = volume;
        volumeLabel.SetText("Volume : {0} %", Mathf.RoundToInt(volume * 100));
        PlayerPrefs.SetFloat(VolumeKey, volume);
    }

    private void OnDisable() => PlayerPrefs.Save();
}
