using UnityEngine;

public class FruitBehaviour : SliceableObject
{
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
