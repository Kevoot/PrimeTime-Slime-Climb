using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour {
    public GameObject PlayerCamera;
	// Use this for initialization
    private Vector2 LastCameraPosition;
    private Vector2 CurrentCameraPosition;
    private float DifferenceSinceLast;

	void Start () {
        LastCameraPosition = PlayerCamera.transform.position;
        CurrentCameraPosition = PlayerCamera.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        CurrentCameraPosition = PlayerCamera.transform.position;
        DifferenceSinceLast = CurrentCameraPosition.y - LastCameraPosition.y;
        transform.position = new Vector3(transform.position.x, (transform.position.y - (DifferenceSinceLast * .2f)), transform.position.z);
        LastCameraPosition = CurrentCameraPosition;
	}

    static void MoveFence()
    {

    } 
}
