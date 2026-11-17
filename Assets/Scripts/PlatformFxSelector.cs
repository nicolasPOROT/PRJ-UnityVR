using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SliceableObject))]
public sealed class PlatformFxSelector : MonoBehaviour
{
    [SerializeField] private SliceableObject sliceable;
    [SerializeField] private GameObject pcvrEffect;
    [SerializeField] private GameObject questEffect;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        sliceable.sliceEffect = questEffect;
#else
        sliceable.sliceEffect = pcvrEffect;
#endif
    }

    private void OnValidate()
    {
        if (!sliceable) sliceable = GetComponent<SliceableObject>();
    }
}
