using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class GUIManager : MonoBehaviour
{
    #region Variables

        [SerializeField] private bool isPaused;

        [SerializeField] private PauseGUIController pauseScreen;

        [SerializeField] private FullMapController fullMapController;

        [SerializeField] private MiniMapController miniMapController;

    #endregion

    private void Awake()
    {       
        if(pauseScreen == null)
            pauseScreen = GetComponentInChildren<PauseGUIController>();

        if(fullMapController == null)
            fullMapController = GetComponentInChildren<FullMapController>();

        if(miniMapController == null)
            miniMapController = GetComponentInChildren<MiniMapController>();
    }

    private void OnEnable()
    {
        InputManager.Inputs.Player.Menu.started += OnMenuButton;
        InputManager.Inputs.UI.Menu.started += OnMenuButton;
    }

    private void OnDisable()
    {
        InputManager.Inputs.Player.Menu.started -= OnMenuButton;
        InputManager.Inputs.UI.Menu.started -= OnMenuButton;
    }


    private void OnMenuButton(InputAction.CallbackContext ctx)
    {
        if(fullMapController.IsDisplaying) return;

        if(isPaused) // Unpause the game
        {
            Time.timeScale = 1;
            pauseScreen.Unpause();
            InputManager.SwitchToPlayerInputs();
        }
        else // Pause the game
        {
            Time.timeScale = 0;
            pauseScreen.Pause();
            InputManager.SwitchToUIInputs();
        }
        
        isPaused = !isPaused;
    }

    public void FullMapToggle(bool value)
    {
        fullMapController.FullMapToggle(value);
    }

    public void MiniMapToggle(bool value)
    {
        miniMapController.MiniMapToggle(value);
    }

    #region Button Functions

        public void OnPause()
        {
            OnMenuButton(new InputAction.CallbackContext());
        }

        

    #endregion
}
