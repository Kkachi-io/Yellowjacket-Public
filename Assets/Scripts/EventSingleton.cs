using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class EventSingleton : MonoBehaviour
{
    #region Variables

        private static EventSystem instance;

    #endregion

    private void Awake()
    {
        if(instance)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            var eventSystem = this.gameObject.GetComponent<EventSystem>();
            if(eventSystem)
            {
                instance = eventSystem;
                GameObject.DontDestroyOnLoad(instance);
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }
    }


}
