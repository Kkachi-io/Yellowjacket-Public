using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    #region Variables

        [SerializeField] private static GameObject currentCamera;

    #endregion

    public static void SetNewCamera(GameObject newCamera)
    {
        if(newCamera == currentCamera) return;

        newCamera.SetActive(true);

        if(currentCamera != null) currentCamera.SetActive(false);

        currentCamera = newCamera;
    }


}
