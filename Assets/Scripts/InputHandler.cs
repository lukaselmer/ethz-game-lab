using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public LayerMask towerBaseLayer;
	public Tower SelectedTower { get; private set; }
	
	public static InputHandler Instance {
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
				if (TowerPlacement.Instance.placeTower(Input.mousePosition)) {
					TowerPlacement.Instance.PlacementMode = false;
				}

			// Tower selection
			} else {
					
				// reset color of selected element
				if (SelectedTower) {
					SelectedTower.SetSelection(false);
				}
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 1000, towerBaseLayer)) {
					var selectedGameObject = hit.collider.gameObject.transform.parent;
					SelectedTower = selectedGameObject.GetComponent<Tower>();
					SelectedTower.SetSelection(true);
				} else {
					SelectedTower = null;
				}
			}

		}

		if (Input.GetKeyDown(KeyCode.Delete)) {

			// remove Tower
			if (SelectedTower) {
				Destroy(SelectedTower.transform.gameObject);
			}
		}	
	}
}
