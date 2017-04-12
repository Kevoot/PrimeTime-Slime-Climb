using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {
	public ParticleSystem particleSys;
	public float radius; // used as player health, essentially
	public float absorptionRate;

	// Use this for initialization
	void Start () {
		particleSys.startLifetime = radius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.name.Contains("Titty Gunner")) {
			Destroy(other.gameObject);
			radius += absorptionRate;
			particleSys.startLifetime = radius;
		}
	}
}
