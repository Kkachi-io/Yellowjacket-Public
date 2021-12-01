using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEnding : MonoBehaviour
{
    #region Variables

        

    #endregion

    private void OnEnable()
    {
        GetComponent<Enemy>().EnemyDied += OnTriggerEnding;
    }

    private void OnDisable()
    {
        GetComponent<Enemy>().EnemyDied -= OnTriggerEnding;
    }

    private void OnTriggerEnding()
    {
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        InputManager.SwitchToUIInputs();
    }

}
