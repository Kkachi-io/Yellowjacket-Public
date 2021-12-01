[System.Serializable]
public class MiniMap 
{
	#region Variables

        private bool miniMapIsDisplayed;
        private GUIManager guiManager;

	#endregion

	public void SetGuiManager(GUIManager guiManager)
    {
        this.guiManager = guiManager;
    }

    public void ToggleMiniMap(bool value)
    {
        miniMapIsDisplayed = value;
        guiManager.MiniMapToggle(miniMapIsDisplayed);
    }
}
