using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	
	void OnGUI ()
	{
		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2-20, 100, 40), "Start Game")) {
			Application.LoadLevel("SimpleScene");
		}

		if (GUI.Button (new Rect (Screen.width/2-50, Screen.height/2+50, 100, 40), "Quit")) {
			Application.Quit();
		}
	}

}
