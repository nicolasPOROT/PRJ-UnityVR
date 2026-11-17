using UnityEngine.UI;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
string path = "Assets/Prefabs/UI/VolumeMenu.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var slider = root.GetComponentInChildren<Slider>(true);
    slider.fillRect.sizeDelta = new Vector2(0, -32);
    slider.handleRect.sizeDelta = new Vector2(38, 16);
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
var scene = UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(scene);
UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
return true;
