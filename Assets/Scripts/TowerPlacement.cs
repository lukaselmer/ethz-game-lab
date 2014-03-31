﻿using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour {

	public GameObject towerPrefab;
	public GameObject towerBasePrefab;
	public Transform towerParent;

	public LayerMask terrainLayer;
	public LayerMask towerBaseLayer;

	public bool placeTower (Vector3 mousePosition) {

		Ray ray = Camera.main.ScreenPointToRay (mousePosition);
		
		// Get Mouse hit point on terrain
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 1000, terrainLayer)) {
			
			// check if allready towers are near mouse hitpoint
			var baseWidth = towerBasePrefab.collider.bounds.size.x;
			var towersNearPoint = Physics.OverlapSphere (hit.point, baseWidth/2, towerBaseLayer);
			if (towersNearPoint.Length == 0) {
				var towerObject = (GameObject)Instantiate (towerPrefab, hit.point, Quaternion.identity);	
				towerObject.transform.parent = towerParent;
				return true;
			}
		}

		return false;
	}
}