// Recette d'édition uniquement, exécutée via Pipeline hors Play.
using UnityEditor.SceneManagement;

if (EditorApplication.isPlayingOrWillChangePlaymode) throw new InvalidOperationException("Hors Play uniquement.");
var summaries = new List<object>();
foreach (var effectName in new[] { "JuiceFx", "ExplosionFx" })
{
    foreach (var platform in new[] { "PCVR", "Quest" })
    {
        string path = "Assets/Prefabs/Fx/" + effectName + "_" + platform + ".prefab";
        if (AssetDatabase.LoadAssetAtPath<GameObject>(path)) throw new InvalidOperationException("Existe déjà : " + path);
        var preview = EditorSceneManager.NewPreviewScene();
        try
        {
            var source = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Fx/" + effectName + ".prefab");
            var root = (GameObject)PrefabUtility.InstantiatePrefab(source, preview);
            root.name = effectName + "_" + platform;
            foreach (var ps in root.GetComponentsInChildren<ParticleSystem>(true))
            {
                if (platform == "Quest")
                {
                    var main = ps.main;
                    main.maxParticles = effectName == "JuiceFx" ? 120 : 80;
                    main.startLifetimeMultiplier *= 0.75f;
                    var collision = ps.collision;
                    collision.enabled = false;
                    var emission = ps.emission;
                    emission.rateOverTimeMultiplier *= 0.4f;
                    emission.rateOverDistanceMultiplier *= 0.4f;
                    for (int b = 0; b < emission.burstCount; b++)
                    {
                        var burst = emission.GetBurst(b);
                        burst.count = new ParticleSystem.MinMaxCurve(Mathf.Max(1, Mathf.Round(burst.count.constantMin * 0.4f)), Mathf.Max(1, Mathf.Round(burst.count.constantMax * 0.4f)));
                        emission.SetBurst(b, burst);
                    }
                    PrefabUtility.RecordPrefabInstancePropertyModifications(ps);
                }
            }
            PrefabUtility.SaveAsPrefabAsset(root, path);
        }
        finally { EditorSceneManager.ClosePreviewScene(preview); }
        var saved = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        summaries.Add(new { path, variant = PrefabUtility.GetPrefabAssetType(saved).ToString(), systems = saved.GetComponentsInChildren<ParticleSystem>(true).Select(ps => new {
            name = ps.name, maxParticles = ps.main.maxParticles, lifetimeMultiplier = ps.main.startLifetimeMultiplier,
            startSizeMultiplier = ps.main.startSizeMultiplier, rateOverTime = ps.emission.rateOverTimeMultiplier,
            bursts = Enumerable.Range(0, ps.emission.burstCount).Select(i => ps.emission.GetBurst(i).count.curveMultiplier).ToArray(),
            collision = ps.collision.enabled,
            materials = ps.GetComponent<ParticleSystemRenderer>().sharedMaterials.Select(m => m ? m.name + " / " + m.shader.name : "MISSING").ToArray()
        }).ToArray() });
    }
}
void Assign(SliceableObject sliceable)
{
    string effect = sliceable is BombBehaviour || sliceable is SelectDif && ((SelectDif)sliceable).sliceEffect.name.Contains("Explosion") ? "ExplosionFx" : "JuiceFx";
    var selector = sliceable.GetComponent<PlatformFxSelector>();
    if (!selector) selector = sliceable.gameObject.AddComponent<PlatformFxSelector>();
    var serialized = new SerializedObject(selector);
    serialized.FindProperty("sliceable").objectReferenceValue = sliceable;
    serialized.FindProperty("pcvrEffect").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Fx/" + effect + "_PCVR.prefab");
    serialized.FindProperty("questEffect").objectReferenceValue = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Fx/" + effect + "_Quest.prefab");
    serialized.ApplyModifiedPropertiesWithoutUndo();
    sliceable.sliceEffect = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Fx/" + effect + "_PCVR.prefab");
    EditorUtility.SetDirty(sliceable);
}
foreach (var folder in new[] { "Assets/Prefabs/Fruits", "Assets/Prefabs/Bombs" })
    foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { folder }))
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        var root = PrefabUtility.LoadPrefabContents(path);
        try { foreach (var sliceable in root.GetComponentsInChildren<SliceableObject>(true)) Assign(sliceable); PrefabUtility.SaveAsPrefabAsset(root, path); }
        finally { PrefabUtility.UnloadPrefabContents(root); }
    }
foreach (var path in new[] { "Assets/Scenes/MainMenu.unity", "Assets/Scenes/PlayScene.unity" })
{
    var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
    foreach (var root in scene.GetRootGameObjects())
        foreach (var sliceable in root.GetComponentsInChildren<SliceableObject>(true))
            if (sliceable is SelectDif) Assign(sliceable);
    EditorSceneManager.MarkSceneDirty(scene);
    EditorSceneManager.SaveScene(scene);
}
AssetDatabase.SaveAssets();
return summaries;
