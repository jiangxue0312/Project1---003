using UnityEngine;
using System.Collections;

public class SunScript : MonoBehaviour {

    public float speed;
    public Color color;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(new Vector3(0, 0, 0), Vector3.left, speed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }
}
