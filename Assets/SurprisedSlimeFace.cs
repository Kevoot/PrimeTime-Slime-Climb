using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisedSlimeFace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetOrderToOne()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    public void SetOrderToZero()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
