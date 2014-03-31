using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{

	public GameObject projectilePrefab;
	public float reloadTime = 1.0f;
	public float turnSpeed = 5.0f;
	public float firePauseTime = 0.25f;
	public Transform[] muzzlePositions;
	public Transform turretBall;
	private HashSet<GameObject> targets = new HashSet<GameObject> ();
	private Transform currentTarget;
	private double nextFireTime;
	private Quaternion desiredRotation;

	void Start ()
	{
		nextFireTime = Time.time + reloadTime * 0.5;
	}

	// Update is called once per frame
	void Update ()
	{
		SetNextTarget ();

		if (currentTarget) {
			CalculateAimPosition (currentTarget.position);
			turretBall.rotation = Quaternion.Lerp (turretBall.rotation, desiredRotation, Time.deltaTime * turnSpeed);
		}
		
		if (Time.time >= nextFireTime) {
			if (currentTarget) {
				FireProjectile ();
				SetNextTarget ();
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			targets.Add (other.gameObject);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.transform == currentTarget) {
			targets.Remove (other.gameObject);
		}
	}

	void SetNextTarget ()
	{
		// remove destroyed targets, because they don't trigger OnTriggerExit
		targets.RemoveWhere (i => i == null);

		GameObject nextTarget = null;
		double minHealth = double.MaxValue;
		foreach (var enemyObject in targets) {
			Enemy enemy = enemyObject.GetComponent<Enemy> ();
			if (enemy.health < minHealth) {
				nextTarget = enemyObject;
				minHealth = enemy.health;
			}
		}

		if (nextTarget != null && nextTarget.transform != currentTarget) 
			nextFireTime = Time.time + reloadTime * 0.5;
		
		currentTarget = nextTarget ? nextTarget.transform : null;
	}

	void CalculateAimPosition (Vector3 targetPos)
	{
		Vector3 aimPoint = new Vector3 (targetPos.x, targetPos.y, targetPos.z);
		aimPoint -= turretBall.position;
		desiredRotation = Quaternion.LookRotation (aimPoint);
	}

	void FireProjectile ()
	{
		nextFireTime = Time.time + reloadTime;

		foreach (var mouzzlePosition in muzzlePositions) {
			var projectileObj = (GameObject)Instantiate (projectilePrefab, mouzzlePosition.position, mouzzlePosition.rotation);
			projectileObj.transform.parent = gameObject.transform;	
			var projectile = projectileObj.GetComponent<Projectile> ();
			projectile.target = currentTarget;
		}
	}
}
