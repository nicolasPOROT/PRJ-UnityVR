using TMPro;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
var path = "Assets/Prefabs/GameplayZone 1.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var gm = root.GetComponentInChildren<GameManager>(true);
    foreach (var text in root.GetComponentsInChildren<TextMeshProUGUI>(true))
    {
        text.color = Color.white;
        text.fontStyle = FontStyles.Bold;
        text.enableAutoSizing = false;
        text.fontSize = text == gm.scoreUi ? 1.05f : 0.9f;
        text.rectTransform.sizeDelta = new Vector2(text == gm.scoreUi ? 2 : 2.8f, 1.2f);
        text.overflowMode = TextOverflowModes.Overflow;
        if (text == gm.scoreUi) text.text = "0";
    }
    foreach (var life in gm.lifeUI)
    {
        life.transform.localScale *= 1.15f;
        var image = life.GetComponent<UnityEngine.UI.Image>();
        if (image) image.color = Color.white;
    }
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/PlayScene.unity");
UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
return new { saved = !scene.isDirty };
