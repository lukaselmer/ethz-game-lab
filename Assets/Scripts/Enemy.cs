using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public double health = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Projectile") {

			Projectile projectile = other.gameObject.GetComponent<Projectile>();

			health -= projectile.myDamage;

			Destroy(other.gameObject);

			if (health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
