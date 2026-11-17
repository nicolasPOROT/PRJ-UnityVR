// Script d'audit à exécuter uniquement dans l'éditeur via Pipeline script eval.
// Hors Assets : aucun code ni objet ajouté au jeu.
using UnityEditor.SceneManagement;
using UnityEditor.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;

if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling)
    throw new InvalidOperationException("Audit requis hors Play et après compilation.");
if (Enumerable.Range(0, UnityEngine.SceneManagement.SceneManager.sceneCount)
    .Any(i => UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).isDirty))
    throw new InvalidOperationException("Sauvegarder les scènes modifiées avant l'audit.");

var originalScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().path;
var checks = new List<object>();
var problems = new List<string>();
void InspectRoot(GameObject root, string asset)
{
    foreach (var transform in root.GetComponentsInChildren<Transform>(true))
    {
        int missing = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(transform.gameObject);
        if (missing > 0) problems.Add(asset + " / " + transform.name + ": " + missing + " Missing Script");
        foreach (var component in transform.GetComponents<Component>())
        {
            if (!component) continue;
            var serialized = new SerializedObject(component);
            var property = serialized.GetIterator();
            while (property.NextVisible(true))
                if (property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.objectReferenceValue == null && property.objectReferenceInstanceIDValue != 0)
                    problems.Add(asset + " / " + transform.name + " / " + component.GetType().Name + "." + property.propertyPath + ": Missing Reference");
        }
    }
}
try
{
    foreach (var path in new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/PlayScene.unity" })
    {
        var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        var roots = scene.GetRootGameObjects();
        foreach (var root in roots) InspectRoot(root, path);
        checks.Add(new { asset = path, roots = roots.Select(r => r.name).ToArray(), objects = roots.Sum(r => r.GetComponentsInChildren<Transform>(true).Length) });
    }
    foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs" }))
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var root = PrefabUtility.LoadPrefabContents(path);
        try { InspectRoot(root, path); checks.Add(new { asset = path, objects = root.GetComponentsInChildren<Transform>(true).Length }); }
        finally { PrefabUtility.UnloadPrefabContents(root); }
    }
}
finally
{
    if (!string.IsNullOrEmpty(originalScene)) EditorSceneManager.OpenScene(originalScene, OpenSceneMode.Single);
}
var validation = new List<object>();
foreach (var target in new[] { BuildTargetGroup.Standalone, BuildTargetGroup.Android })
{
    var issues = new List<OpenXRFeature.ValidationRule>();
    OpenXRProjectValidation.GetCurrentValidationIssues(issues, target);
    validation.Add(new { target = target.ToString(), issues = issues.Select(i => new { i.message, i.error, canFix = i.fixIt != null }).ToArray() });
}
return new { unity = Application.unityVersion, checks, problems, validation };
