// Ajustement des bursts sérialisés dans les variantes Quest, hors Play.
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
var summaries = new List<object>();
foreach (var name in new[] { "JuiceFx", "ExplosionFx" })
{
    string questPath = "Assets/Prefabs/Fx/" + name + "_Quest.prefab";
    var root = PrefabUtility.LoadPrefabContents(questPath);
    try
    {
        foreach (var ps in root.GetComponentsInChildren<ParticleSystem>(true))
        {
            var emission = ps.emission;
            for (int b = 0; b < emission.burstCount; b++)
            {
                var burst = emission.GetBurst(b);
                burst.count = new ParticleSystem.MinMaxCurve(Mathf.Max(1, Mathf.Round(burst.count.constantMin * 0.4f)), Mathf.Max(1, Mathf.Round(burst.count.constantMax * 0.4f)));
                emission.SetBurst(b, burst);
            }
        }
        PrefabUtility.SaveAsPrefabAsset(root, questPath);
    }
    finally { PrefabUtility.UnloadPrefabContents(root); }
}
foreach (var guid in AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Prefabs/Fx" }))
{
    var path = AssetDatabase.GUIDToAssetPath(guid);
    var root = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    summaries.Add(new { path, systems = root.GetComponentsInChildren<ParticleSystem>(true).Select(ps => new {
        name = ps.name, maxParticles = ps.main.maxParticles, lifetime = ps.main.startLifetime.constantMax,
        size = ps.main.startSize.constantMax, bursts = Enumerable.Range(0, ps.emission.burstCount).Select(i => new { min = ps.emission.GetBurst(i).count.constantMin, max = ps.emission.GetBurst(i).count.constantMax }).ToArray(),
        emission = ps.emission.enabled, collision = ps.collision.enabled,
        shape = ps.shape.enabled, colorOverLifetime = ps.colorOverLifetime.enabled, sizeOverLifetime = ps.sizeOverLifetime.enabled,
        velocityOverLifetime = ps.velocityOverLifetime.enabled, noise = ps.noise.enabled, trails = ps.trails.enabled,
        shader = ps.GetComponent<ParticleSystemRenderer>().sharedMaterial.shader.name
    }).ToArray() });
}
AssetDatabase.SaveAssets();
return summaries;
