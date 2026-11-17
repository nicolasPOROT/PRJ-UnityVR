// Test logique ponctuel via script eval en Play Mode ; aucun objet structurel créé.
// Vitesse synthétique pour la coupe : ne valide ni tracking ni geste physique.
if (!EditorApplication.isPlaying) throw new InvalidOperationException("Play Mode requis.");
var menu = Object.FindFirstObjectByType<MainSceneManager>();
if (!menu) throw new InvalidOperationException("Démarrer depuis MainMenu.");
bool fpsOverlayWorks = false;
var fpsOverlay = Object.FindFirstObjectByType<FpsOverlay>();
if (fpsOverlay)
{
    var fpsSettings = new SerializedObject(fpsOverlay);
    fpsSettings.FindProperty("showOverlay").boolValue = true;
    fpsSettings.ApplyModifiedPropertiesWithoutUndo();
    var fpsLabel = fpsOverlay.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    await Wait.Until(() => fpsLabel.text != "FPS : —");
    fpsOverlayWorks = fpsLabel.enabled && fpsLabel.text.StartsWith("FPS :");
    fpsSettings.FindProperty("showOverlay").boolValue = false;
    fpsSettings.ApplyModifiedPropertiesWithoutUndo();
}
var volumeMenu = Object.FindFirstObjectByType<VolumeMenu>();
float originalVolume = AudioListener.volume;
bool volumeApplied = false;
if (volumeMenu)
{
    volumeMenu.GetComponentInChildren<UnityEngine.UI.Slider>().value = 0.37f;
    volumeApplied = Mathf.Approximately(AudioListener.volume, 0.37f);
}
DifficultyTransfert.Instance.difficultyLevel = 0;
menu.Play();
await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PlayScene");
await Wait.Until(() => Object.FindObjectsByType<FruitBehaviour>(FindObjectsSortMode.None).Length > 0);
var gm = Object.FindFirstObjectByType<GameManager>();
if (!gm) throw new InvalidOperationException("GameManager absent.");
var fruit = Object.FindFirstObjectByType<FruitBehaviour>();
int beforeScore = gm.score;
var blade = Object.FindFirstObjectByType<SliceObject>();
var oldStart = blade.startSlicePoint.position;
var oldEnd = blade.endSlicePoint.position;
var oldEnabled = blade.enabled;
int successfulCuts = 0;
System.Action onSlice = () => successfulCuts++;
blade.SuccessfulSlice += onSlice;
try
{
    blade.enabled = false;
    blade.velocityEstimator.FinishEstimatingVelocity();
    var flags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
    typeof(VelocityEstimator).GetField("velocitySamples", flags).SetValue(blade.velocityEstimator, Enumerable.Repeat(Vector3.right, 5).ToArray());
    typeof(VelocityEstimator).GetField("sampleCount", flags).SetValue(blade.velocityEstimator, 5);
    blade.startSlicePoint.position = fruit.transform.position - Vector3.up;
    blade.endSlicePoint.position = fruit.transform.position;
    blade.Slice(fruit.gameObject);
    if (successfulCuts != 1) throw new InvalidOperationException("Échec de coupe EzySlice synthétique.");
}
finally
{
    blade.SuccessfulSlice -= onSlice;
    blade.startSlicePoint.position = oldStart;
    blade.endSlicePoint.position = oldEnd;
    blade.enabled = oldEnabled;
    blade.velocityEstimator.BeginEstimatingVelocity();
}
await Wait.Until(() => gm.score > beforeScore);
int afterSimple = gm.score;
gm.GainScore();
gm.GainScore();
await Wait.Until(() => gm.score > afterSimple);
int afterCombo = gm.score;
int initialLives = gm.lifes;
gm.FruitFall();
bool easyMissPreservesLives = gm.lifes == initialLives;
await Wait.Until(() => Object.FindObjectsByType<BombBehaviour>(FindObjectsSortMode.None).Length > 0);
var bomb = Object.FindFirstObjectByType<BombBehaviour>();
bomb.IsSliced();
Object.Destroy(bomb.gameObject);
bool bombCostsLife = gm.lifes == initialLives - 1;
int spawnedFruits = Object.FindObjectsByType<FruitBehaviour>(FindObjectsSortMode.None).Length;
while (gm.lifes > 0) gm.LoseLife();
var gameOver = Object.FindFirstObjectByType<GameOverPresentation>();
bool endScreen = false;
bool replayReset = false;
if (gameOver)
{
    await Wait.Until(() => !gm.enabled && gameOver.IsVisible);
    endScreen = true;
    gameOver.GetComponentsInChildren<UnityEngine.UI.Button>(true).Single(b => b.name == "ReplayButton").onClick.Invoke();
    await Wait.Until(() => gm == null && Object.FindFirstObjectByType<GameManager>() != null);
    gm = Object.FindFirstObjectByType<GameManager>();
    await Wait.Until(() => gm.lifes > 0);
    replayReset = gm.score == 0 && gm.lifes == initialLives && gm.difficulty == 0;
    while (gm.lifes > 0) gm.LoseLife();
    gameOver = Object.FindFirstObjectByType<GameOverPresentation>();
    await Wait.Until(() => !gm.enabled && gameOver.IsVisible);
    gameOver.GetComponentsInChildren<UnityEngine.UI.Button>(true).Single(b => b.name == "MenuButton").onClick.Invoke();
}
await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu");
bool volumePersisted = false;
if (volumeApplied)
{
    int frame = Time.frameCount;
    await Wait.Until(() => Time.frameCount > frame + 2);
    volumePersisted = Mathf.Approximately(AudioListener.volume, 0.37f);
    Object.FindFirstObjectByType<VolumeMenu>().GetComponentInChildren<UnityEngine.UI.Slider>().value = originalVolume;
}
return new { syntheticEzySlice = successfulCuts == 1, simpleScore = afterSimple - beforeScore, comboScore = afterCombo - afterSimple,
    initialLives, easyMissPreservesLives, bombCostsLife, spawnedFruits, endScreen, replayReset, volumeApplied, volumePersisted, fpsOverlayWorks, returnedToMenu = true,
    limitation = "Coupe EzySlice avec vitesse injectée ; geste physique, tracking et casque non testés." };
