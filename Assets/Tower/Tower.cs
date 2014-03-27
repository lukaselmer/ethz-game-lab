using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public GameObject myProjectile;
	public float reloadTime = 1.0f;
	public float turnSpeed = 5.0f;
	public float firePauseTime = 0.25f;
	public float errorAmount = 0.001f;
	public Transform myTarget;
	public Transform[] muzzlePositions;
	public Transform turretBall;

	double nextFireTime;
	double nextMoveTime;
	Quaternion desiredRotation;
	float aimError;

	// Update is called once per frame
	void Update () {
	
		if (myTarget) {

			if (Time.time >= nextMoveTime) {
				CalculateAimPosition(myTarget.position);
				turretBall.rotation = Quaternion.Lerp (turretBall.rotation, desiredRotation, Time.deltaTime * turnSpeed);
			}

			if (Time.time >= nextFireTime) {
				FireProjectile();
			}
		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Enemy") {
			nextFireTime = Time.time + (reloadTime * 0.5);
			myTarget = other.gameObject.transform;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.transform == myTarget) {
			myTarget = null;
		}
	}

	void CalculateAimPosition (Vector3 targetPos) {
		Vector3 aimPoint = new Vector3 (targetPos.x + aimError, targetPos.y + aimError, targetPos.z + aimError);
		aimPoint -= turretBall.position;
		desiredRotation = Quaternion.LookRotation (aimPoint);
	}

	void CalculateAimError() {
		aimError = Random.Range (-errorAmount, errorAmount);
	}

	void FireProjectile() {
		nextFireTime = Time.time + reloadTime;
		nextMoveTime = Time.time + firePauseTime;

		CalculateAimError ();

		foreach (var theMouzzlePos in muzzlePositions) {
			Instantiate (myProjectile, theMouzzlePos.position, theMouzzlePos.rotation);
		}

	}
}
