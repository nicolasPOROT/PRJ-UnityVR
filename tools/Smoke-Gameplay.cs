// Test logique ponctuel via script eval en Play Mode ; aucun objet structurel créé.
// Ne valide ni tracking, ni geste de coupe EzySlice, ni perception sur casque.
if (!EditorApplication.isPlaying) throw new InvalidOperationException("Play Mode requis.");
var menu = Object.FindFirstObjectByType<MainSceneManager>();
if (!menu) throw new InvalidOperationException("Démarrer depuis MainMenu.");
DifficultyTransfert.Instance.difficultyLevel = 0;
menu.Play();
await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PlayScene");
await Wait.Until(() => Object.FindObjectsByType<FruitBehaviour>(FindObjectsSortMode.None).Length > 0);
var gm = Object.FindFirstObjectByType<GameManager>();
if (!gm) throw new InvalidOperationException("GameManager absent.");
var fruit = Object.FindFirstObjectByType<FruitBehaviour>();
int beforeScore = gm.score;
fruit.IsSliced();
Object.Destroy(fruit.gameObject);
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
await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu");
return new { simpleScore = afterSimple - beforeScore, comboScore = afterCombo - afterSimple,
    initialLives, easyMissPreservesLives, bombCostsLife, spawnedFruits, returnedToMenu = true,
    limitation = "Appels de méthodes de gameplay ; geste EzySlice, tracking et casque non testés." };
