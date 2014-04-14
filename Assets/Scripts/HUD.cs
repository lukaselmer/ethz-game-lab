using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{	
	public GUIText guiPlayTime;
	public GUIText guiTower;

	void Update ()
	{
		guiPlayTime.text = string.Format ("Enemies killed: {0}, Enemies survived: {1}, Play Time: {2:F0}", 
		                                  GameLogic.I.EnemiesKilled, GameLogic.I.EnemiesSurvived, GameLogic.I.PlayTime);

		
		if (InputHandler.I.SelectedTower != null) {
			guiTower.text = string.Format ("Size: {0:F1}", InputHandler.I.SelectedTower.Size);
		} else {
			guiTower.text = "";
		}
	}
	
	void OnGUI ()
	{
		if (InputHandler.I.SelectedTower != null) {
			if (GUI.Button (new Rect (Screen.width-110, 30, 100, 30), TowerPlacement.Instance.PlacementMode ? "Placing..." : "Branch Tower")) {
				TowerPlacement.Instance.PlacementMode = true;
			}
		}
	}
}
