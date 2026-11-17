using UnityEngine.UI;
using UnityEditor.SceneManagement;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
string path = "Assets/Prefabs/GameplayZone 1.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    var presentation = root.GetComponentInChildren<GameOverPresentation>(true);
    var data = new SerializedObject(presentation);
    data.FindProperty("gameplayHud").objectReferenceValue = root.GetComponentInChildren<GameManager>(true).scoreUi.canvas.gameObject;
    data.ApplyModifiedPropertiesWithoutUndo();
    var panel = presentation.transform.Find("GameOverPanel").GetComponent<Image>();
    panel.color = new Color(0.025f, 0.05f, 0.08f, 1f);
    presentation.GetComponent<Canvas>().sortingOrder = 100;
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
var scene = EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
var volume = Object.FindFirstObjectByType<VolumeMenu>();
volume.transform.localPosition = new Vector3(0, -0.25f, 0.8f);
volume.transform.localScale = Vector3.one * 0.001f;
volume.GetComponent<Canvas>().sortingOrder = 100;
PrefabUtility.RecordPrefabInstancePropertyModifications(volume.transform);
PrefabUtility.RecordPrefabInstancePropertyModifications(volume.GetComponent<Canvas>());
EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene); AssetDatabase.SaveAssets();
// Restaurer le volume initial connu après l'ancien test interrompu par son timeout.
AudioListener.volume = 1;
PlayerPrefs.SetFloat("FruitNinjaVR.AudioVolume", 1);
PlayerPrefs.Save();
return true;
