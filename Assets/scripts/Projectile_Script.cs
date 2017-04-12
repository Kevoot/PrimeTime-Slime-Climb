using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Script : MonoBehaviour {
	public float speed;
    public Titty_Gunner_AI tga;
    private bool BulletDirection;

	// Use this for initialization
	void Start() {
        BulletDirection = tga.direction;
        this.gameObject.transform.position = tga.transform.position;
	}
	
	// Update is called once per frame
	void Update() {
        if(BulletDirection)
        {
            transform.position = new Vector3(transform.position.x + 1, transform.position.y) * speed * Time.deltaTime;
        }
		else
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y) * speed * Time.deltaTime;
        }
	}

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			// reduce player size (damage the player)
		}
		Destroy(gameObject);
	}
}
