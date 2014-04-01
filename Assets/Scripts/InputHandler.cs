using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public HUD hud;
	public TowerPlacement towerPlacement;

	public LayerMask towerBaseLayer;
	public Material highlightMaterial;
	public Material defaultMaterial;

	private GameObject selected;

	// Update is called once per frame
	void Update () {

		// Handle left click
		if (Input.GetMouseButton (0)) {	

			// Tower placement
			if (hud.PlacementMode) {
				if (towerPlacement.placeTower(Input.mousePosition)) {
					hud.PlacementMode = false;
				}

			// Tower selection
			} else {
					
				// reset color of selected element
				if (selected) {
					MeshRenderer rnd = selected.GetComponent<MeshRenderer>();
					rnd.material = defaultMaterial;
				}
				
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 1000, towerBaseLayer)) {
					selected = hit.collider.gameObject;
					MeshRenderer rnd = selected.GetComponent<MeshRenderer>();
					rnd.material = highlightMaterial;
				} else {
					selected = null;
				}
			}

		}

		if (Input.GetKeyDown(KeyCode.Delete)) {

			// remove Tower
			if (selected) {
				Destroy(selected.transform.parent.gameObject);
			}
		}
	
	}
}
