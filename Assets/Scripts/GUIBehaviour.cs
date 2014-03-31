using UnityEngine;
using System.Collections;

public class GUIBehaviour : MonoBehaviour
{	
	public GameLogic gameLogic;
	public GUIText guiPlayTime;
	public TowerPlacement towerPlacement;

	private bool placementMode = false;

	void Update ()
	{
		guiPlayTime.text = string.Format ("Enemies killed: {0}, Enemies survived: {1}, Play Time: {2:F0}", 
		               gameLogic.EnemiesKilled, gameLogic.EnemiesSurvived, gameLogic.PlayTime);

		if (Input.GetMouseButton (0)) {	
			if (placementMode) {
				if (towerPlacement.placeTower(Input.mousePosition)) {
					placementMode = false;
				}
			}
		}
	}
	
	void OnGUI ()
	{
		if (GUI.Button (new Rect (10, Screen.height - 110, 100, 100), "Tower")) {
			placementMode = true;
		}
	}
}
