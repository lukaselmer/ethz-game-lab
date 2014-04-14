using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public LayerMask towerBaseLayer;
	public Tower SelectedTower { get; private set; }
	
	public static InputHandler I {
		get {
			return FindObjectOfType<InputHandler>();
		}
	}

	// Update is called once per frame
	void Update () {

		// Handle left click
		if (Input.GetMouseButton (0)) {	

			// Tower placement
			if (TowerPlacement.Instance.PlacementMode) {
				if (TowerPlacement.Instance.placeTower(Input.mousePosition, SelectedTower)) {
					TowerPlacement.Instance.PlacementMode = false;
				}

			// Tower selection
			} else {
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 1000, towerBaseLayer)) {

					var selectedGameObject = hit.collider.gameObject.transform.parent;
					var newSelection = selectedGameObject.GetComponent<Tower>();

					if (SelectedTower && newSelection != SelectedTower) {
						SelectedTower.SetSelection(false);
					}

					SelectedTower = newSelection;
					SelectedTower.SetSelection(true);
				}
			}
		}

		if (Input.GetMouseButton (1) && SelectedTower) {
			SelectedTower = null;
		}

		if (Input.GetKeyDown(KeyCode.Delete)) {

			// remove Tower
			if (SelectedTower) {
				Destroy(SelectedTower.transform.gameObject);
			}
		}	
	}
}
