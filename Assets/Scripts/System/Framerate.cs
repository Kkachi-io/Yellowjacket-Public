using UnityEngine;

public class Framerate : MonoBehaviour
{
    #region Variables

        

    #endregion

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }

}
