using UnityEngine;

public class FruitBehaviour : SliceableObject
{
    protected GameManager gm;
    protected virtual void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }
    
    protected override void OnMissed()
    {
        gm.FruitFall();
    }
    
    public override void IsSliced()
    {
        AudioSource.PlayClipAtPoint(sliceAudioClip, transform.position); 
        gm.GainScore();
    }
}
