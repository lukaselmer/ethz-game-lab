using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float mySpeed = 10;
	public float myRange = 10;
	public float myDamage = 5;
	public Transform target;
	public Treee origin;
	private float myDist;

	// Update is called once per frame
	void Update ()
	{
		if (target)
			transform.LookAt (target);

		transform.Translate (Vector3.forward * TimeManager.GetDeltaTime() * mySpeed);
		myDist += TimeManager.GetDeltaTime() * mySpeed;
		if (myDist > myRange) {
			Destroy (gameObject);
		}
	}
}
