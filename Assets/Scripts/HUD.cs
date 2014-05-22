using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{	
	public GUIText guiPlayTime;
	public GUIText guiTower;

	void Update ()
	{
		guiPlayTime.text = string.Format ("Lives: {0}, Remaining Waves: {1}, Enemies killed: {2}, Play Time: {3:F0}", 
		                                  GameLogic.I.Lives, GameLogic.I.RemainingWaves, GameLogic.I.EnemiesKilled, GameLogic.I.PlayTime);

		
		if (InputHandler.I.Selected is Treee) {
			guiTower.text = string.Format ("Size: {0:F1}", (InputHandler.I.Selected as Treee).Size);
		} else {
			guiTower.text = "";
		}
	}
	
	void OnGUI ()
	{
		//if (InputHandler.I.SelectedTower != null) {
		//	if (GUI.Button (new Rect (Screen.width-110, 30, 100, 30), TreePlacement.Instance.PlacementMode ? "Placing..." : "Branch Tower")) {
		//		TreePlacement.Instance.PlacementMode = true;
		//	}
		//}
	}
}
