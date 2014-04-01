using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{	
	public GameLogic gameLogic;
	public GUIText guiPlayTime;

	public bool PlacementMode { get; set; }

	void Update ()
	{
		guiPlayTime.text = string.Format ("Enemies killed: {0}, Enemies survived: {1}, Play Time: {2:F0}", 
		               gameLogic.EnemiesKilled, gameLogic.EnemiesSurvived, gameLogic.PlayTime);
	}
	
	void OnGUI ()
	{
		if (GUI.Button (new Rect (10, Screen.height - 110, 100, 100), PlacementMode ? "Placing..." : "Tower")) {
			PlacementMode = true;
		}
	}
}
