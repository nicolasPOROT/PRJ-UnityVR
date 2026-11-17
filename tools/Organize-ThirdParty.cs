using UnityEditor.SceneManagement;
if (EditorApplication.isPlaying) throw new InvalidOperationException("Hors Play uniquement.");
if (!AssetDatabase.IsValidFolder("Assets/ThirdParty")) AssetDatabase.CreateFolder("Assets", "ThirdParty");
var moves = new[] {
    new { source = "Assets/Imported_assets", destination = "Assets/ThirdParty/Imported_assets" },
    new { source = "Assets/ezy-slice-master", destination = "Assets/ThirdParty/EzySlice" }
};
foreach (var move in moves)
{
    if (!AssetDatabase.IsValidFolder(move.source)) throw new InvalidOperationException("Source absente : " + move.source);
    var error = AssetDatabase.ValidateMoveAsset(move.source, move.destination);
    if (!string.IsNullOrEmpty(error)) throw new InvalidOperationException(error);
    error = AssetDatabase.MoveAsset(move.source, move.destination);
    if (!string.IsNullOrEmpty(error)) throw new InvalidOperationException(error);
}
EditorSceneManager.SaveOpenScenes();
AssetDatabase.SaveAssets();
return moves;
