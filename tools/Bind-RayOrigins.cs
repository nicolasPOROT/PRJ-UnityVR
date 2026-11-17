using UnityEngine.XR.Interaction.Toolkit.Interactors;
var path = "Assets/Prefabs/Player.prefab";
var root = PrefabUtility.LoadPrefabContents(path);
try
{
    foreach (var ray in root.GetComponentsInChildren<XRRayInteractor>(true))
    {
        ray.rayOriginTransform = ray.transform;
        ray.attachTransform = ray.transform;
        EditorUtility.SetDirty(ray);
    }
    PrefabUtility.SaveAsPrefabAsset(root, path);
}
finally { PrefabUtility.UnloadPrefabContents(root); }
UnityEditor.SceneManagement.EditorSceneManager.SaveOpenScenes();
return true;
