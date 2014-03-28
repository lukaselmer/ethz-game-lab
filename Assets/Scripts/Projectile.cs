using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float mySpeed = 10;
	public float myRange = 10;
	public float myDamage = 5;

	float myDist;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * Time.deltaTime * mySpeed);
		myDist += Time.deltaTime * mySpeed;
		if (myDist > myRange) {
			Destroy(gameObject);
		}
	}
}
