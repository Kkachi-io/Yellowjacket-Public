using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FullMap
{
    #region Variables

        private bool fullMapIsDisplayed = false;
        private GUIManager guiManager;

    #endregion

    public void SetGuiManager(GUIManager guiManager)
    {
        this.guiManager = guiManager;
    }

    public void FullMapOn()
    {
        if(fullMapIsDisplayed) return;

        InputManager.SwitchToUIInputs();
        fullMapIsDisplayed = true;
        guiManager.FullMapToggle(fullMapIsDisplayed);
    }

    public void FullMapOff()
    {
        if(!fullMapIsDisplayed) return;

        InputManager.SwitchToPlayerInputs();
        fullMapIsDisplayed = false;
        guiManager.FullMapToggle(fullMapIsDisplayed);
        
    }

}
