using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

class LevelSelection {
	public void LoadLevels () {
		Application.LoadLevel("Levels");
	}

	public void LoadNextLevel () {
		TimeManager.Speed1();

		// TODO: refactor this
		if(Application.loadedLevelName == "Spring")
			Application.LoadLevel("Summer");
		else if(Application.loadedLevelName == "Summer")
			Application.LoadLevel("Fall");
		else if(Application.loadedLevelName == "Fall")
			Application.LoadLevel("Winter");
		else if(Application.loadedLevelName == "Winter")
			Application.LoadLevel("Levels");
	}

	public void ReplayLevel () {
		Application.LoadLevel(Application.loadedLevelName);
	}
}
