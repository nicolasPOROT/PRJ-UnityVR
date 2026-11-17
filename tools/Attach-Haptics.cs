if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
const string path = "Assets/Prefabs/Player.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var left = root.GetComponentsInChildren<Transform>(true).Single(t => t.name == "LeftHand");
    foreach (var blade in root.GetComponentsInChildren<SliceObject>(true))
    {
        var feedback = blade.gameObject.AddComponent<SliceHapticFeedback>();
        var serialized = new SerializedObject(feedback);
        serialized.FindProperty("blade").objectReferenceValue = blade;
        serialized.FindProperty("controllerNode").intValue = (int)(blade.transform.IsChildOf(left) ? UnityEngine.XR.XRNode.LeftHand : UnityEngine.XR.XRNode.RightHand);
        serialized.ApplyModifiedPropertiesWithoutUndo();
    }
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
