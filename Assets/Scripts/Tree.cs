using UnityEngine;
using System.Collections;


public static class TreeConfig {
	public static float z_angle = 30;
	public static int numBranches = 4;
	public static int maxDepth = 5;

	public static GameObject branchPrefab;
	public static LayerMask branchLayer;

}

public class Tree : MonoBehaviour {
	public LayerMask branchLayer;	
	public GameObject branchPrefab;

	private RootBranch root;

	// Use this for initialization
	void Start () {
		TreeConfig.branchLayer = gameObject.layer;
		TreeConfig.branchPrefab = branchPrefab;

		root = new RootBranch (this.transform);
	}
	
	// Update is called once per frame
	void Update () {
		root.Grow (Time.deltaTime * 0.1f);

		//HandleBranchSelection ();
	}


	void HandleBranchSelection() {
		// Handle left click
		if (Input.GetMouseButton (0)) {	
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000, TreeConfig.branchLayer)) {
				var selectedGameObject = hit.collider.gameObject;
				//Branch branch = selectedGameObject.GetComponent<Branch>();
				//print (branch.depth);
			}
		}
	}
}
