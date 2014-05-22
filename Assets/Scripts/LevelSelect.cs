using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	public LayerMask ClickableLayer;
	
	// Update is called once per frame
	void Update () {

		// Handle left click
		if (Input.GetMouseButton (0)) {	

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000, ClickableLayer)) {
				
				var selectedGameObject = hit.collider.gameObject.transform;
				var levelName = selectedGameObject.name;

				LevelSelection.LoadLevel(levelName);
			}
		}
	}
}
