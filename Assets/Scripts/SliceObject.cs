using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public Material crossSectionMaterial;
    public float cutForce = 2000;
    public LayerMask sliceableLayer;

    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);


            // Slice logic
            SliceableObject sliceableObject = target.GetComponent<SliceableObject>();

            sliceableObject.IsSliced();

            // Juice particles
            GameObject sliceFx = sliceableObject.sliceEffect;
            GameObject juice = Instantiate(sliceFx, upperHull.transform.position, Quaternion.identity);
            Destroy(juice, 2f);

            // Destroy main fruit
            Destroy(target);

            // Destroy sliced parts after 2 sec
            Destroy(upperHull, 2f);
            Destroy(lowerHull, 2f);
        }
    }

    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }
}
