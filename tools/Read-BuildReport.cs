using UnityEditor.Build.Reporting;
using Newtonsoft.Json;
var report = BuildReport.GetLatestReport();
if (!report) throw new InvalidOperationException("Aucun Build Report disponible.");
string Category(string path)
{
    string extension = System.IO.Path.GetExtension(path).ToLowerInvariant();
    if (new[] { ".png", ".jpg", ".jpeg", ".tga", ".psd", ".exr", ".hdr", ".tif" }.Contains(extension)) return "Textures";
    if (new[] { ".fbx", ".obj", ".blend" }.Contains(extension)) return "Modeles";
    if (new[] { ".wav", ".mp3", ".ogg", ".aiff" }.Contains(extension)) return "Audio";
    if (new[] { ".shader", ".shadergraph", ".compute", ".shadervariants" }.Contains(extension)) return "Shaders";
    if (extension == ".mat") return "Materiaux";
    if (extension == ".unity") return "Scenes";
    return "Autres";
}
var packed = report.packedAssets.SelectMany(p => p.contents).ToArray();
var categories = packed.GroupBy(p => Category(p.sourceAssetPath)).Select(g => new { category = g.Key, packedBytes = g.Sum(p => (long)p.packedSize) }).OrderByDescending(g => g.packedBytes).ToArray();
var largest = packed.GroupBy(p => p.sourceAssetPath).Select(g => new { path = g.Key, packedBytes = g.Sum(p => (long)p.packedSize) }).OrderByDescending(g => g.packedBytes).Take(20).ToArray();
var summary = report.summary;
var result = new {
    platform = summary.platform.ToString(), result = summary.result.ToString(), output = summary.outputPath,
    bytes = summary.totalSize, errors = summary.totalErrors, warnings = summary.totalWarnings,
    started = summary.buildStartedAt.ToString("o"), timestampKind = summary.buildStartedAt.Kind.ToString(), duration = summary.totalTime.ToString(),
    unity = Application.unityVersion, categories, largest,
    largestOutputFiles = report.GetFiles().OrderByDescending(f => f.size).Take(10).Select(f => new { f.path, f.size, f.role }).ToArray(),
    messages = report.steps.SelectMany(s => s.messages).Where(m => m.type == LogType.Error || m.type == LogType.Exception || m.type == LogType.Warning).Select(m => new { type = m.type.ToString(), m.content }).ToArray(),
    note = "Categories calculees par extension source a partir des PackedAssetInfo ; poids packe Unity, pas taille compressee dans APK. summary.totalSize peut inclure des fichiers intermediaires C++/natifs et symboles hors APK : comparer les livrables reels avec Compare-BuildSizes.ps1."
};
var output = "docs/build_report_" + (summary.platform == BuildTarget.Android ? "quest" : "windows") + ".json";
System.IO.File.WriteAllText(output, JsonConvert.SerializeObject(result, Formatting.Indented));
return new { file = output, result.result, result.bytes, result.errors, result.warnings, categories, largest };
