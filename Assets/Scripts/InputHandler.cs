using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public LayerMask sawLayer;
	public LayerMask branchLayer;
	public Branch SelectedBranch { get; private set; }
	
	public static InputHandler I {
		get {
			return FindObjectOfType<InputHandler>();
		}
	}

	float placeTime;

	// Update is called once per frame
	void Update () {

		// Handle right click
		if (Input.GetMouseButton (1)) {	
			// Placing
			if (SelectedBranch && TreePlacement.Instance.PlacementMode) {
				if (TreePlacement.Instance.placeTree (Input.mousePosition, SelectedBranch)) {
					TreePlacement.Instance.PlacementMode = false;
				}

			}
		}
					
		// Handle left click
		if (Input.GetMouseButton (0)) {	
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// Branching
			if (SelectedBranch && SelectedBranch.depth > 1 && Physics.Raycast (ray, out hit, 1000, sawLayer)) {
				if (hit.collider.gameObject.renderer.enabled) {
					TreePlacement.Instance.PlacementMode = true;
					return;
				}
			}

			// Selecting
			if (Physics.Raycast (ray, out hit, 1000, branchLayer)) {
				var selectedGameObject = hit.collider.gameObject;
				var newSelection = selectedGameObject.GetComponent<Branch>();

				if (SelectedBranch && newSelection != SelectedBranch) {
					SelectedBranch.UnSelect();
				}

				SelectedBranch = newSelection;
				SelectedBranch.Select();
 			}
		}

		if (Input.GetMouseButton (2) && SelectedBranch) {
			SelectedBranch.UnSelect();
			SelectedBranch = null;
		}	
	}
}
