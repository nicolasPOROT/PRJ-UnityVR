using UnityEngine;

public class DifficultyTransfert : MonoBehaviour
{
    // Singleton pour le data transfert
    public static DifficultyTransfert Instance;
    
    // Difficulté à transferer
    public int difficultyLevel = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // Faire persister l'objet
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // S'il existe déjà, détruire la nouvelle instance
            Destroy(gameObject);
        }
    }
}
