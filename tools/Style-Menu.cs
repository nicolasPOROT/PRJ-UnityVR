using TMPro;
using UnityEditor.SceneManagement;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
var scene = EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity", OpenSceneMode.Single);
var labels = new Dictionary<string, string> { {"Easy Mode", "Facile"}, {"Medium", "Normal"}, {"Hard", "Difficile"}, {"Insane", "Intense"}, {"Extreme", "Extrême"} };
var changed = new List<string>();
foreach (var text in scene.GetRootGameObjects().SelectMany(r => r.GetComponentsInChildren<TextMeshProUGUI>(true)))
{
    if (labels.TryGetValue(text.text, out var label))
    {
        text.text = label;
        text.fontSize = 6;
        text.fontStyle = FontStyles.Bold;
        text.color = Color.white;
        text.enableAutoSizing = false;
        changed.Add(label);
    }
    else if (text.name == "Info")
    {
        text.text = "Tranche un fruit\npour choisir ce niveau";
        text.fontSize = 3.5f;
        text.rectTransform.sizeDelta = new Vector2(45, 14);
        text.alignment = TextAlignmentOptions.Center;
    }
    EditorUtility.SetDirty(text);
}
EditorSceneManager.MarkSceneDirty(scene);
EditorSceneManager.SaveScene(scene);
return new { labels = changed.ToArray(), saved = !scene.isDirty };
