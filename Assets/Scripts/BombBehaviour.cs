using UnityEngine;

public class BombBehaviour : SliceableObject
{
    public override void IsSliced()
    {
        gm.LoseLife();
    }
}
