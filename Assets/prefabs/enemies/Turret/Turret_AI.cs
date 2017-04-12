using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_AI : MonoBehaviour {
	public GameObject projectile;
	public GameObject spawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find("Player");
		if(player != null) {
			Vector2 pl = new Vector2(player.transform.position.x, player.transform.position.y);
			Vector2 me = new Vector2(transform.position.x, transform.position.y);
			float dist = Vector2.Distance(pl, me);
			if(dist < 20) {
				Transform gun = transform.Find("Gun");
				Transform pivot = gun.Find("Pivot");
				Quaternion rot = Quaternion.RotateTowards(gun.rotation, player.transform.rotation, (Mathf.PI * 2));
				Instantiate(projectile, spawn.transform.position, rot);
			}
		}
	}
}
