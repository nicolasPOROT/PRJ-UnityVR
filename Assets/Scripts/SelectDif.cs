using System.Collections;
using UnityEngine;

public class SelectDif : SliceableObject
{
    [SerializeField] private int dificultySelected;

    public override void IsSliced()
    {
        AudioSource.PlayClipAtPoint(sliceAudioClip, transform.position);

        DifficultyTransfert.Instance.difficultyLevel = dificultySelected;

        FindFirstObjectByType<MainSceneManager>().Play();
    }
}
