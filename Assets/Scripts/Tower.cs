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
	public GameObject selectionObject;

	private TowerTargetController targetController;

	private double nextFireTime;
	private Quaternion desiredRotation;

	private float size = 1.0f;
	public float Size {
		get {
			return Mathf.Min (size, 3);
		}
		set {
			size = value;
		}
	}

	void Start ()
	{
		nextFireTime = Time.time + reloadTime * 0.5;
		targetController = GetComponentInChildren<TowerTargetController> ();
	}

	// Update is called once per frame
	void Update ()
	{
		Size += Time.deltaTime * 0.01f;
		transform.localScale = new Vector3 (Size, Size, Size);

		targetController.SetNextTarget ();

		if (targetController.CurrentTarget) {
			CalculateAimPosition (targetController.CurrentTarget.position);
			turretBall.rotation = Quaternion.Lerp (turretBall.rotation, desiredRotation, Time.deltaTime * turnSpeed);
		}
		
		if (Time.time >= nextFireTime) {
			if (targetController.CurrentTarget) {
				FireProjectile ();
				targetController.SetNextTarget ();
			}
		}
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
			projectile.target = targetController.CurrentTarget;
			projectile.origin = this;
		}
	}

	public void SetSelection(bool selection) {
		selectionObject.renderer.enabled = selection;
	}
}
