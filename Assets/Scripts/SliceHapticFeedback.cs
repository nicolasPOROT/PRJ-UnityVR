using UnityEngine;
using UnityEngine.XR;

[DisallowMultipleComponent]
public sealed class SliceHapticFeedback : MonoBehaviour
{
    [SerializeField] private SliceObject blade;
    [SerializeField] private XRNode controllerNode = XRNode.RightHand;
    [SerializeField, Range(0f, 1f)] private float amplitude = 0.15f;
    [SerializeField, Range(0.01f, 0.2f)] private float duration = 0.04f;

    private void OnEnable() => blade.SuccessfulSlice += Pulse;
    private void OnDisable() => blade.SuccessfulSlice -= Pulse;

    private void Pulse()
    {
        var device = InputDevices.GetDeviceAtXRNode(controllerNode);
        if (device.isValid && device.TryGetHapticCapabilities(out var capabilities) && capabilities.supportsImpulse)
            device.SendHapticImpulse(0, amplitude, duration);
    }
}
