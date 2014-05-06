using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

class LevelSelection {
	public void LoadLevels () {
		Application.LoadLevel("Levels");
	}

	public void LoadNextLevel () {
		// TODO: refactor this
		if(Application.loadedLevelName == "Spring")
			Application.LoadLevel("Summer");
		if(Application.loadedLevelName == "Summer")
			Application.LoadLevel("Fall");
		if(Application.loadedLevelName == "Fall")
			Application.LoadLevel("Winter");
		if(Application.loadedLevelName == "Winter")
			Application.LoadLevel("Levels");
	}

	public void ReplayLevel () {
		Application.LoadLevel(Application.loadedLevelName);
	}
}
