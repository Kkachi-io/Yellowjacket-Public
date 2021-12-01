using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceActivatorActivator : MonoBehaviour
{

    [SerializeField] private float distanceToActivate = 20;

    // It appears that the sphere collider radius is about 2/3 the number returned as the Vector3.distance between objects.
    private float distanceModifier = 0.666f;

    private void Awake()
    {
        // Shpere Collider management
        var sphCol = GetComponent<SphereCollider>();
        if(!sphCol)
        {
            var badCol = GetComponent<Collider>();
            if(badCol)
                Destroy(badCol);

            sphCol = this.gameObject.AddComponent<SphereCollider>();
        }
        sphCol.radius = distanceToActivate * distanceModifier;

        // Activate anything within distance
        var colliders = Physics.OverlapSphere(sphCol.transform.position, sphCol.radius);
        foreach(var collider in colliders)
        {
            var activator = collider.GetComponent<DistanceActivator>();

            if(activator)
                activator.Activate();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var activator = other.GetComponent<DistanceActivator>();

        if(activator)
            activator.Activate();
    }

    private void OnTriggerExit(Collider other)
    {
        var activator = other.GetComponent<DistanceActivator>();

        if(activator)
            activator.Deactivate();
    }

}
