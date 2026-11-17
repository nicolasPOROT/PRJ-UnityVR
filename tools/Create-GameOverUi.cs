// Assemblage éditorial par l'API Unity, hors Play. Aucun générateur dans le jeu.
using TMPro;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactors.Visuals;
using UnityEngine.XR.Interaction.Toolkit.UI;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
if (!AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/UiRay.mat"))
{
    var material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    material.SetColor("_BaseColor", new Color(0.3f, 0.9f, 0.7f));
    AssetDatabase.CreateAsset(material, "Assets/Materials/UiRay.mat");
}
GameObject Child(string name, Transform parent, Vector2 size, Vector2 position)
{
    var go = new GameObject(name, typeof(RectTransform));
    go.transform.SetParent(parent, false);
    var rect = (RectTransform)go.transform;
    rect.sizeDelta = size; rect.anchoredPosition = position;
    return go;
}
TextMeshProUGUI Label(string name, string text, Transform parent, Vector2 size, Vector2 position, float fontSize)
{
    var go = Child(name, parent, size, position);
    var label = go.AddComponent<TextMeshProUGUI>();
    label.text = text; label.fontSize = fontSize; label.color = Color.white;
    label.alignment = TextAlignmentOptions.Center; label.raycastTarget = false;
    return label;
}
string playerPath = "Assets/Prefabs/Player.prefab";
var player = PrefabUtility.LoadPrefabContents(playerPath);
try
{
    var actionPath = AssetDatabase.GUIDToAssetPath("4144a405cb82629438d58ef374a56d12");
    var actionAsset = AssetDatabase.LoadAssetAtPath<InputActionAsset>(actionPath);
    var references = AssetDatabase.LoadAllAssetsAtPath(actionPath).OfType<InputActionReference>().ToArray();
    var manager = player.GetComponent<InputActionManager>();
    var managerData = new SerializedObject(manager);
    var assets = managerData.FindProperty("m_ActionAssets");
    assets.arraySize = 2;
    assets.GetArrayElementAtIndex(1).objectReferenceValue = actionAsset;
    managerData.ApplyModifiedPropertiesWithoutUndo();
    foreach (var side in new[] { "Left", "Right" })
    {
        var hand = player.GetComponentsInChildren<Transform>(true).Single(t => t.name == side + "Hand");
        var go = new GameObject(side + "UIRay"); go.transform.SetParent(hand, false);
        var ray = go.AddComponent<XRRayInteractor>();
        ray.rayOriginTransform = go.transform;
        ray.attachTransform = go.transform;
        ray.enableUIInteraction = true; ray.maxRaycastDistance = 10;
        ray.interactionLayers = 0;
        ray.uiPressInput.inputActionReferencePerformed = references.First(r => r.action.actionMap.name == "XRI " + side + " Interaction" && r.action.name == "UI Press");
        ray.uiPressInput.inputActionReferenceValue = references.First(r => r.action.actionMap.name == "XRI " + side + " Interaction" && r.action.name == "UI Press Value");
        var line = go.AddComponent<LineRenderer>();
        line.widthMultiplier = 0.003f;
        line.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/UiRay.mat");
        var visual = go.AddComponent<XRInteractorLineVisual>(); visual.lineWidth = 0.003f;
    }
    PrefabUtility.SaveAsPrefabAsset(player, playerPath);
}
finally { PrefabUtility.UnloadPrefabContents(player); }
string gamePath = "Assets/Prefabs/GameplayZone 1.prefab";
var root = PrefabUtility.LoadPrefabContents(gamePath);
try
{
    var camera = root.GetComponentInChildren<Camera>(true);
    var gm = root.GetComponentInChildren<GameManager>(true);
    var go = Child("GameOverCanvas", camera.transform, new Vector2(760, 440), Vector2.zero);
    go.transform.localPosition = new Vector3(0, 0, 1.6f);
    go.transform.localScale = Vector3.one * 0.002f;
    var canvas = go.AddComponent<Canvas>(); canvas.renderMode = RenderMode.WorldSpace; canvas.worldCamera = camera; canvas.sortingOrder = 100;
    go.AddComponent<TrackedDeviceGraphicRaycaster>();
    var panel = Child("GameOverPanel", go.transform, new Vector2(760, 440), Vector2.zero);
    panel.AddComponent<Image>().color = new Color(0.025f, 0.05f, 0.08f, 1f);
    Label("Title", "Partie terminée", panel.transform, new Vector2(700, 80), new Vector2(0, 140), 48);
    var score = Label("FinalScore", "Score final : 0", panel.transform, new Vector2(700, 80), new Vector2(0, 40), 42);
    var presentation = go.AddComponent<GameOverPresentation>();
    var data = new SerializedObject(presentation);
    data.FindProperty("panel").objectReferenceValue = panel;
    data.FindProperty("finalScoreLabel").objectReferenceValue = score;
    data.FindProperty("gameManager").objectReferenceValue = gm;
    data.FindProperty("canonManager").objectReferenceValue = root.GetComponentInChildren<CononsManager>(true);
    data.FindProperty("gameplayHud").objectReferenceValue = gm.scoreUi.canvas.gameObject;
    var blades = root.GetComponentsInChildren<SliceObject>(true);
    data.FindProperty("blades").arraySize = blades.Length;
    for (int i = 0; i < blades.Length; i++) data.FindProperty("blades").GetArrayElementAtIndex(i).objectReferenceValue = blades[i];
    data.ApplyModifiedPropertiesWithoutUndo();
    foreach (var replay in new[] { true, false })
    {
        var buttonGo = Child(replay ? "ReplayButton" : "MenuButton", panel.transform, new Vector2(290, 80), new Vector2(replay ? -165 : 165, -115));
        var image = buttonGo.AddComponent<Image>(); image.color = replay ? new Color(0.12f, 0.42f, 0.3f) : new Color(0.17f, 0.24f, 0.32f);
        var button = buttonGo.AddComponent<Button>(); button.targetGraphic = image;
        Label("Label", replay ? "Rejouer" : "Menu", buttonGo.transform, new Vector2(270, 70), Vector2.zero, 34);
        if (replay) UnityEventTools.AddPersistentListener(button.onClick, presentation.Replay);
        else UnityEventTools.AddPersistentListener(button.onClick, presentation.ReturnToMenu);
    }
    var gmData = new SerializedObject(gm); gmData.FindProperty("gameOverPresentation").objectReferenceValue = presentation; gmData.ApplyModifiedPropertiesWithoutUndo();
    PrefabUtility.SaveAsPrefabAsset(root, gamePath);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
foreach (var path in new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/PlayScene.unity" })
{
    var scene = EditorSceneManager.OpenScene(path);
    var system = Object.FindFirstObjectByType<UnityEngine.EventSystems.EventSystem>();
    var old = system.GetComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
    if (old) old.enabled = false;
    if (!system.GetComponent<XRUIInputModule>()) system.gameObject.AddComponent<XRUIInputModule>();
    EditorSceneManager.MarkSceneDirty(scene); EditorSceneManager.SaveScene(scene);
}
AssetDatabase.SaveAssets();
return true;
