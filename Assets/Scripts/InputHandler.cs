using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

	public HUD hud;
	public TowerPlacement towerPlacement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {	
			if (hud.PlacementMode) {
				if (towerPlacement.placeTower(Input.mousePosition)) {
					hud.PlacementMode = false;
				}
			}
		}
	
	}
}
