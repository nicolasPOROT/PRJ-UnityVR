using UnityEngine;

public abstract class SliceableObject : MonoBehaviour
{
    public GameObject sliceEffect;
    public AudioClip sliceAudioClip;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("fruitKillZone"))
        {
            OnMissed();
            Destroy(gameObject);
        }
    }

    // Méthodes virtuelles à override
    protected virtual void OnMissed() { }
    public virtual void IsSliced() { }
}
