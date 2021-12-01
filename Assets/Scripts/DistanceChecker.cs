using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    #region Variables

        [SerializeField] private Transform hero;
        
        [SerializeField] private Color textBackground = new Color(0,0,0,0);

        [SerializeField] private List<ObjToTrack> objs;

    #endregion

    #if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(objs.Count > 0 && hero != null)
        {
            foreach(var obj in objs)
			{
                if(obj != null)
                {
                    Gizmos.color = obj.color;
                    Gizmos.DrawLine(hero.position, obj.transform.position);

                    DrawDistanceText(obj);
                    continue;
                }

                objs.Remove(obj);

			}
		}


    }

	private void DrawDistanceText(ObjToTrack obj)
	{
		var halfway = Vector3.Lerp(hero.position, obj.transform.position, 0.5f);
		var distance = Vector3.Distance(hero.position, obj.transform.position);
		var text = $"{obj.text}\n{distance}";

		UnityEditor.Handles.color = obj.color;
		UnityEditor.Handles.Label(halfway, text);
	}

    #endif

}


[System.Serializable]
public class ObjToTrack
{
    public Transform transform;
    public string text;
    public Color color = Color.magenta;

	public ObjToTrack(Transform transform, string text, Color color)
	{
		this.transform = transform;
		this.text = text;
		this.color = color;
	}
}