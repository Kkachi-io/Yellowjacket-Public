using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    #region Variables

        [SerializeField] private int mainMenuSceneIndex = 0;
        [SerializeField] private GameObject cameraToRemove;

    #endregion

    public void SwitchScenes()
    {
        GameObject.Destroy(cameraToRemove);
        FindObjectOfType<GUICamera>().Set();
        SceneManager.UnloadSceneAsync(mainMenuSceneIndex);
    }

}
