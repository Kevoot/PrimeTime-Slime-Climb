using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head_Sucker_AI : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		SpriteRenderer spr = GetComponent<SpriteRenderer>();
		GameObject player = GameObject.Find("Player");
		if(player != null) {
			if(player.transform.position.x < transform.position.x) spr.flipX = true;
			else spr.flipX = false;

			float dist = Vector2.Distance(transform.position, player.transform.position);
			if((dist > 5) && (dist < 50)) {
				transform.position = Vector2.MoveTowards(transform.position, 
														 player.transform.position, 
														 (speed * Time.deltaTime));
			} else if(dist <= 5) {
				// suck that slime!
			}
		}
	}
}
