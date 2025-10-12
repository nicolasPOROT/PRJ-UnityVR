using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public void Play()
    {
        StartCoroutine(LoadDelay());
    }

    private IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("PlayScene");
    }
}
