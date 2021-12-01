using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsScreen : MonoBehaviour, ISettingsTabScreen
{
	#region Variables



	#endregion
	public void Activate()
	{
		Debug.Log("Audio Tab activated");
	}

	public void Deactivate()
	{
		Debug.Log("Audio Tab deactivated");
	}
}
