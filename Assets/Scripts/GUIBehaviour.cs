using UnityEngine;
using System.Collections;

public class GUIBehaviour : MonoBehaviour {
	
	public GameLogic gameLogic;
	public LayerMask terrainLayer;
	public GameObject tower;
	public GUIText guiPlayTime;
	public Transform towerParent;

	private bool placementMode = false;

	void Update () {
		guiPlayTime.text = "Play Time: " + gameLogic.PlayTime;

		if (placementMode) {
			if (Input.GetMouseButton(0)) {

				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, 1000, terrainLayer)) {
					var towerObject = (GameObject)Instantiate (tower, hit.point, Quaternion.identity);	
					towerObject.transform.parent = towerParent;
					placementMode = false;
				}
			}
		}
	}

	
	void OnGUI () {
		if (GUI.Button (new Rect (10, Screen.height-110, 100, 100), "Tower")) {
			placementMode = true;
		}
	}
}
