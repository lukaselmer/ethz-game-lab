using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public LayerMask sawLayer;
	public LayerMask branchLayer;
	public Branch SelectedBranch { get; private set; }
	public Selectable Selected { get; private set; }
	
	public static InputHandler I {
		get {
			return FindObjectOfType<InputHandler>();
		}
	}

	float placeTime;

	// Update is called once per frame
	void Update () {
					
		// Handle left click
		if (Input.GetMouseButtonDown (0)) {	

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// Placing
			if (SelectedBranch && TreePlacement.Instance.PlacementMode) {
				if (TreePlacement.Instance.placeTree (Input.mousePosition, SelectedBranch)) {
					TreePlacement.Instance.PlacementMode = false;
					Selected.UnSelect();
					SelectedBranch = null;
				}
				return;
			}

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
					Selected.UnSelect();
				}

				SelectedBranch = newSelection;
				Selected = SelectedBranch;

				if (SelectedBranch.depth <= 1) {
					Selected = newSelection.tree;
				}
				Selected.Select();
 			}
		}

		if (Input.GetMouseButtonDown (2) && SelectedBranch) {
			Selected.UnSelect();
			SelectedBranch = null;
		}	
	}
}
