using TMPro;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
string path = "Assets/Prefabs/Player.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var camera = root.GetComponentInChildren<Camera>(true);
    var canvasGo = new GameObject("DebugFpsCanvas", typeof(RectTransform), typeof(Canvas));
    canvasGo.transform.SetParent(camera.transform, false);
    canvasGo.transform.localPosition = new Vector3(0.28f, 0.25f, 0.8f);
    canvasGo.transform.localScale = Vector3.one * 0.001f;
    var canvas = canvasGo.GetComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace; canvas.worldCamera = camera;
    ((RectTransform)canvasGo.transform).sizeDelta = new Vector2(200, 60);
    var labelGo = new GameObject("FpsLabel", typeof(RectTransform), typeof(TextMeshProUGUI));
    labelGo.transform.SetParent(canvasGo.transform, false);
    ((RectTransform)labelGo.transform).sizeDelta = new Vector2(200, 60);
    var label = labelGo.GetComponent<TextMeshProUGUI>(); label.text = "FPS : —"; label.fontSize = 26; label.color = new Color(0.6f, 1f, 0.7f); label.raycastTarget = false;
    var overlay = canvasGo.AddComponent<FpsOverlay>();
    var data = new SerializedObject(overlay); data.FindProperty("label").objectReferenceValue = label; data.FindProperty("showOverlay").boolValue = false; data.ApplyModifiedPropertiesWithoutUndo();
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
foreach (var scenePath in new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/PlayScene.unity" })
{
    var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
    UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
    UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
}
return true;
