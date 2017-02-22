using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    private Vector3 camPos;
	
	// Update is called once per frame
	void Update ()
    {
        //fixed y coord, because mario does it too :P
        camPos = new Vector3(player.transform.position.x + offset.x, transform.position.y, player.transform.position.z + offset.z);
        transform.position = camPos;
	}
}
