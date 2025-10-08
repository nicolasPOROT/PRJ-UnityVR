using UnityEngine;

public class FruitBehaviour : SliceableObject
{
    protected override void OnMissed()
    {
        gm.LoseLife();
    }
    
    public override void IsSliced()
    {
        gm.score++; // ajout score;
    }
}
