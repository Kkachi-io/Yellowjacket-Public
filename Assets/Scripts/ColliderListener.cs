using System;
using UnityEngine;

public class ColliderListener : MonoBehaviour
{
    #region Variables

        public event Action<Collider> OnTriggerEntered; 
        public event Action<Collider> OnTriggerExited; 

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEntered?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExited?.Invoke(other);
    }

}
