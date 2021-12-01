using CMF;
using UnityEngine;

[System.Serializable]
public class DoubleJump
{
	#region Variables

        private bool hasDoubleJumped = true;


	#endregion

	public bool MayDoubleJump()
    {
        return !hasDoubleJumped;
    }

    public void CheckAndSetDoubleJump(ControllerState currentControllerState)
    {
        if(currentControllerState == ControllerState.Grounded)
            return;
        
        if(hasDoubleJumped)
            Debug.LogError("Performed Double Jump more than once?");

        hasDoubleJumped = true;
    }

    public void ResetDoubleJump(Vector3 v3)
    {
        hasDoubleJumped = false;
    }

}
