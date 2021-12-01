
public static class InputManager
{
    #region Variables

        public static MainInputs Inputs = new MainInputs();

    #endregion

    public static void SwitchToPlayerInputs()
    {
        Inputs.Disable();
        Inputs.Player.Enable();
    }

    public static void SwitchToUIInputs()
    {
        Inputs.Disable();
        Inputs.UI.Enable();
    }


}
