using UnityEngine;

public class GUICamera : MonoBehaviour
{
    [SerializeField] private bool autoRunSet;

    private void Start()
    {
        if(autoRunSet)
            Set();
    }

    public void Set()
    {
        this.transform.SetParent(Camera.main.transform);
        this.transform.position = Vector3.zero;
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        GameObject.DontDestroyOnLoad(this.gameObject);
        this.GetComponent<Camera>().enabled = true;
    }


}
