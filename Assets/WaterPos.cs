using UnityEngine;
using System.Collections;

public class WaterPos : MonoBehaviour {

    public GameObject landscape;
	// Use this for initialization
	void Start () {
        LandscapeScript land = landscape.GetComponent<LandscapeScript>();
        transform.position = new Vector3(0, 0.1f * land.seed * land.roughness, 0);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
