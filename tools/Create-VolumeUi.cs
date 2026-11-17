using TMPro;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
if (!AssetDatabase.IsValidFolder("Assets/Prefabs/UI")) AssetDatabase.CreateFolder("Assets/Prefabs", "UI");
GameObject Rect(string name, Transform parent, Vector2 size, Vector2 position)
{
    var go = new GameObject(name, typeof(RectTransform));
    if (parent) go.transform.SetParent(parent, false);
    var rect = (RectTransform)go.transform; rect.sizeDelta = size; rect.anchoredPosition = position;
    return go;
}
var root = Rect("VolumeMenu", null, new Vector2(600, 170), Vector2.zero);
try
{
    var canvas = root.AddComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace;
    root.AddComponent<TrackedDeviceGraphicRaycaster>();
    root.AddComponent<Image>().color = new Color(0.025f, 0.05f, 0.08f, 0.95f);
    var labelGo = Rect("VolumeLabel", root.transform, new Vector2(550, 60), new Vector2(0, 45));
    var label = labelGo.AddComponent<TextMeshProUGUI>(); label.text = "Volume : 100 %"; label.fontSize = 32; label.alignment = TextAlignmentOptions.Center; label.raycastTarget = false;
    var sliderGo = Rect("VolumeSlider", root.transform, new Vector2(480, 50), new Vector2(0, -35));
    var track = sliderGo.AddComponent<Image>(); track.color = new Color(0.16f, 0.23f, 0.3f);
    var slider = sliderGo.AddComponent<Slider>();
    var fillGo = Rect("Fill", sliderGo.transform, new Vector2(480, 16), Vector2.zero); fillGo.AddComponent<Image>().color = new Color(0.25f, 0.85f, 0.6f);
    var handleGo = Rect("Handle", sliderGo.transform, new Vector2(38, 66), Vector2.zero); var handleImage = handleGo.AddComponent<Image>(); handleImage.color = Color.white;
    slider.fillRect = (RectTransform)fillGo.transform; slider.handleRect = (RectTransform)handleGo.transform; slider.targetGraphic = handleImage; slider.minValue = 0; slider.maxValue = 1; slider.value = 1;
    var controller = root.AddComponent<VolumeMenu>();
    var data = new SerializedObject(controller); data.FindProperty("volumeSlider").objectReferenceValue = slider; data.FindProperty("volumeLabel").objectReferenceValue = label; data.ApplyModifiedPropertiesWithoutUndo();
    UnityEventTools.AddPersistentListener(slider.onValueChanged, controller.SetVolume);
    PrefabUtility.SaveAsPrefabAsset(root, "Assets/Prefabs/UI/VolumeMenu.prefab");
}
finally { Object.DestroyImmediate(root); }
var scene = EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
var camera = Object.FindFirstObjectByType<Camera>();
var instance = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/VolumeMenu.prefab"), scene);
instance.transform.SetParent(camera.transform, false);
instance.transform.localPosition = new Vector3(0, -0.55f, 1.6f); instance.transform.localScale = Vector3.one * 0.0015f;
instance.GetComponent<Canvas>().worldCamera = camera;
PrefabUtility.RecordPrefabInstancePropertyModifications(instance.transform); PrefabUtility.RecordPrefabInstancePropertyModifications(instance.GetComponent<Canvas>());
EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene); AssetDatabase.SaveAssets();
return true;
