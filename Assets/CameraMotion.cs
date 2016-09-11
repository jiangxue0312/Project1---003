using UnityEngine;
using System.Collections;

public class CameraMotion : MonoBehaviour {
    // The speed of moving 
    public float speed;
    
    public GameObject landscape;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // Control the relative pitch and yaw of the camera
        float yaw =  Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y");
        transform.eulerAngles += new Vector3(-pitch, yaw, 0.0f);

        // Move forwards and backwards respectively
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.rotation * Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.rotation * Vector3.back * speed * Time.deltaTime;
        }
        // Move left and right respectively
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.rotation * Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.rotation * Vector3.right * speed * Time.deltaTime;
        }
        // Control the roll of the camera
        if (Input.GetKey(KeyCode.Q))
        {
            transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.back);
        }
        // Prohibit the user from moving underground or outside the bounds of the landscape
        // The current position of the camera
        float posX = transform.position.x;
        float posY = transform.position.y;
        float posZ = transform.position.z;

        
        // Prohibit the camera move out of the landscape

        // The range of the landscape

        LandscapeScript land = landscape.GetComponent<LandscapeScript>();
        
        float range = ((int)Mathf.Pow(2, land.level) + 1) - 10; 
        if (posX < 0)
        {
            posX = 0;
        }
        if (posZ < 0)
        {
            posZ = 0;
        }
        if (posX > range)
        {
            posX = range;
        }
        if (posZ > range)
        {
            posZ = range;
        }
        // Get the height of the landscape
        // float height = terrain.terrainData.GetHeight((int)posX, (int)posY);
        // float height = terrain.SampleHeight(transform.position);
        float height = land.landscape[(int)posX, (int)posZ];
        if (posY < height + 10)
        {
            posY = height + 10;
        }
        // Update camera position
        transform.position = new Vector3(posX, posY, posZ);
       

    }

   
}
