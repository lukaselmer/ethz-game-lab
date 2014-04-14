using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	public GameObject towerPrefab;
	public Transform towerParent;

	public LayerMask terrainLayer;
	public LayerMask towerLayer;
	
	public bool PlacementMode { get; set; }
	
	public static TowerPlacement Instance {
		get {
			return FindObjectOfType<TowerPlacement>();
		}
	}

	public bool placeTower (Vector3 mousePosition, Tower originTower) {
		if (originTower.Size < 1.5) {
			return true;
		}

		Ray ray = Camera.main.ScreenPointToRay (mousePosition);
		
		// Get Mouse hit point on terrain
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000, terrainLayer)) {

			if ((hit.point - originTower.transform.position).magnitude > originTower.Size * 5) {
				return false;
			}
			
			// check if allready towers are near mouse hitpoint
			var towersNearPoint = Physics.OverlapSphere (hit.point, 1, towerLayer);
			if (towersNearPoint.Length == 0) {
				GameObject towerObject = (GameObject)Instantiate (towerPrefab, hit.point, Quaternion.identity);	
				towerObject.transform.parent = towerParent;
				towerObject.name = "Tower";

				towerObject.GetComponent<Tower>().Size = originTower.Size/2;
				originTower.Size = originTower.Size/2;
				return true;
			}
		}

		return false;
	}
}
