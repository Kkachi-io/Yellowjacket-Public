using System.Collections;
using Cinemachine;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Variables

        [SerializeField] private CinemachineVirtualCamera roomCamera;

    #endregion

    private void Awake()
    {
        if(roomCamera == null)
            roomCamera = GetComponentInChildren<CinemachineVirtualCamera>(true);

        if(roomCamera.Follow == null)
        {
            var hero = FindObjectOfType<Hero>();
            if(hero != null)
            {
                roomCamera.Follow = hero.transform;
            }
            else
            {
                Debug.LogError($"Couldn't find hero.");
            }
        }

        roomCamera.gameObject.SetActive(false);

        var doors = GetComponentsInChildren<Doorway>();
        foreach(var door in doors)
        {
            door.SetRoom(this);
        }
    }

    public void ActivateRoomCamera()
    {
        CameraManager.SetNewCamera(roomCamera.gameObject);
    }


}
