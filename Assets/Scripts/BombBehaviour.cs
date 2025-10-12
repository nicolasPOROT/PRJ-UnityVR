using UnityEngine;

public class BombBehaviour : SliceableObject
{
    public override void IsSliced()
    {
        AudioSource.PlayClipAtPoint(sliceAudioClip, transform.position); 
        gm.LoseLife();
    }
}
