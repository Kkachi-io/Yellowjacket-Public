using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceActivator : MonoBehaviour
{
    #region Variables

        [SerializeField] private List<Behaviour> components;

        [SerializeField] private bool isActivated = false;

    #endregion

    private void Awake()
    {
        Deactivate();
    }

	public bool CheckIsActivated() => isActivated;

	public void Activate()
    {
        isActivated = true;

        foreach(var component in components)
        {
            component.enabled = true;
        }
    }

    public void Deactivate()
    {
        isActivated = false;

        foreach(var component in components)
        {
            if(component.enabled)
                component.enabled = false;
        }
    }


}
