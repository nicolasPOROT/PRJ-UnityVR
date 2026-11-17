using TMPro;
using UnityEditor.SceneManagement;
var result = new List<object>();
foreach (var path in new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/PlayScene.unity" })
{
    var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
    var texts = scene.GetRootGameObjects().SelectMany(r => r.GetComponentsInChildren<TextMeshProUGUI>(true));
    result.Add(new { path, texts = texts.Select(t => new { t.name, t.text, t.fontSize, position = new[] {t.transform.position.x,t.transform.position.y,t.transform.position.z}, scale = new[] {t.transform.lossyScale.x,t.transform.lossyScale.y,t.transform.lossyScale.z}, rect = new[] {t.rectTransform.sizeDelta.x,t.rectTransform.sizeDelta.y} }).ToArray(),
        player = scene.GetRootGameObjects().SelectMany(r => r.GetComponentsInChildren<Transform>(true)).Where(t => t.name.Contains("Hand") || t.name == "EventSystem").Select(t => new { t.name, components = t.GetComponents<Component>().Where(c => c).Select(c => c.GetType().FullName).ToArray() }).ToArray() });
}
return new { scenes = result };
