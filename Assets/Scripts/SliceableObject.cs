using UnityEngine;

public abstract class SliceableObject : MonoBehaviour
{
    protected GameManager gm;
    public GameObject sliceEffect;
    public AudioClip sliceAudioClip;

    protected virtual void Start()
    {
        gm = FindFirstObjectByType<GameManager>();
    }

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
