using UnityEngine;

public class Keep2D : MonoBehaviour
{
    private void FixedUpdate()
	{
		EnsureZAxis();
	}

	private void EnsureZAxis()
	{
		var pos = transform.position;
		pos.z = 0;
		transform.position = pos;
	}


}
