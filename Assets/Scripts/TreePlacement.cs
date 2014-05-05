using UnityEngine;
using System.Collections;

public class TreePlacement : MonoBehaviour {

	public GameObject treePrefab;
	public Transform treeParent;

	public LayerMask terrainLayer;
	public LayerMask branchLayer;
	
	public bool PlacementMode { get; set; }
	
	public static TreePlacement Instance {
		get {
			return FindObjectOfType<TreePlacement>();
		}
	}

	public bool placeTree (Vector3 mousePosition, Branch branch) {

		Treee originTree = branch.tree;

		Ray ray = Camera.main.ScreenPointToRay (mousePosition);
		
		// Get Mouse hit point on terrain
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000, terrainLayer)) {

			if ((hit.point - originTree.transform.position).magnitude > originTree.Size * 5) {
				return false;
			}
			
			// check if allready trees are near mouse hitpoint
			var treesNearPoint = Physics.OverlapSphere (hit.point, 1, branchLayer);
			if (treesNearPoint.Length == 0) {
				GameObject treeObject = (GameObject)Instantiate (treePrefab);	
				treeObject.transform.parent = treeParent;
				treeObject.name = "Tree";
				
				treeObject.transform.position = hit.point;
				treeObject.transform.rotation = Quaternion.identity;

				Treee newTree = treeObject.GetComponent<Treee>();
				branch.Attach(newTree);

				//newTree.Size = originTree.Size/2;
				//originTree.Size = originTree.Size/2;
				return true;
			}
		}

		return false;
	}
}
