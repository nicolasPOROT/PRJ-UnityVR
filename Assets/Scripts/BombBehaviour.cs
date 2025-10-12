using UnityEngine;

public class BombBehaviour : SliceableObject
{
    protected GameManager gm;
    protected virtual void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }
    
    public override void IsSliced()
    {
        AudioSource.PlayClipAtPoint(sliceAudioClip, transform.position); 
        gm.LoseLife();
    }
}
