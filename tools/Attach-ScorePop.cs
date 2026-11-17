if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
string path = "Assets/Prefabs/GameplayZone 1.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var gm = root.GetComponentInChildren<GameManager>(true);
    var pop = gm.scoreUi.gameObject.AddComponent<ScorePop>();
    var serialized = new SerializedObject(pop);
    serialized.FindProperty("scoreLabel").objectReferenceValue = gm.scoreUi;
    serialized.ApplyModifiedPropertiesWithoutUndo();
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/PlayScene.unity");
UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
return true;
