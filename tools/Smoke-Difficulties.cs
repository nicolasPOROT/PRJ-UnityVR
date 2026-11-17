// Parcours des cinq sélecteurs existants via leurs méthodes, sans geste physique.
if (!EditorApplication.isPlaying) throw new InvalidOperationException("Play depuis MainMenu requis.");
var results = new List<object>();
for (int level = 0; level <= 4; level++)
{
    var selector = Object.FindObjectsByType<SelectDif>(FindObjectsSortMode.None).Single(s => new SerializedObject(s).FindProperty("dificultySelected").intValue == level);
    selector.IsSliced();
    await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "PlayScene");
    var gm = Object.FindFirstObjectByType<GameManager>();
    await Wait.Until(() => gm.lifes == 3);
    int observedLevel = gm.difficulty;
    await Wait.Until(() => Object.FindObjectsByType<FruitBehaviour>(FindObjectsSortMode.None).Length + Object.FindObjectsByType<BombBehaviour>(FindObjectsSortMode.None).Length > 0);
    int before = gm.lifes;
    gm.FruitFall();
    int missedFruitCost = before - gm.lifes;
    results.Add(new { selected = level, observed = observedLevel, missedFruitCost, spawned = true });
    if (observedLevel != level || missedFruitCost != (level >= 2 ? 1 : 0)) throw new InvalidOperationException("Comportement inattendu au niveau " + level);
    while (gm.lifes > 0) gm.LoseLife();
    var presentation = Object.FindFirstObjectByType<GameOverPresentation>();
    await Wait.Until(() => !gm.enabled && presentation.IsVisible);
    presentation.GetComponentsInChildren<UnityEngine.UI.Button>(true).Single(b => b.name == "MenuButton").onClick.Invoke();
    await Wait.Until(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu");
    int frame = Time.frameCount;
    await Wait.Until(() => Time.frameCount > frame + 2);
}
return new { levels = results, limitation = "Sélection et fruit manqué invoqués par méthodes ; interaction physique non testée." };
