using UnityEngine;
using System.Collections;


public class Treee : MonoBehaviour, Selectable {
	public LayerMask branchLayer;	
	public GameObject branchPrefab;
	public float zAngle = 30;
	public int numBranches = 4;
	public int maxDepth = 5;
	public int yOffsetMin = 5;
	public int yOffsetMax = 10;
	public float growFactor = 0.02f;
	
	public GameObject projectilePrefab;
	public float reloadTime = 1.0f;
	public float firePauseTime = 0.25f;
	public float turnSpeed = 5.0f;
	public GameObject selectionObject;

	private TreeTargetController targetController;
	private double nextFireTime;
	private Quaternion desiredRotation;


	public Branch Root {
		get; set;
	}

	public float Size {
		get {
			return Root.TotalSize;
		}
	}

	// Use this for initialization
	void Start () {
		nextFireTime = Time.time + reloadTime * 0.5;
		targetController = GetComponentInChildren<TreeTargetController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Root.Grow (Time.deltaTime * growFactor);

		targetController.transform.localScale = new Vector3 (Size+5, Size+5, Size+5);
	
		targetController.SetNextTarget ();
		
		if (targetController.CurrentTarget) {
			CalculateAimPosition (targetController.CurrentTarget.position);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
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
		aimPoint -= this.transform.position;
		desiredRotation = Quaternion.LookRotation (aimPoint);
	}
	
	void FireProjectile ()
	{
		nextFireTime = Time.time + reloadTime;

		var projectileObj = (GameObject)Instantiate (projectilePrefab, this.transform.position, this.transform.rotation);
		projectileObj.transform.parent = gameObject.transform;	
		var projectile = projectileObj.GetComponent<Projectile> ();
		projectile.target = targetController.CurrentTarget;
		projectile.origin = this;
	}

	public void Select() {
		selectionObject.renderer.enabled = true;
	}

	public void UnSelect() {
		selectionObject.renderer.enabled = false;
	}
}
