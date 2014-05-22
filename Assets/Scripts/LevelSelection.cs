using UnityEngine;
using System.Collections.Generic;
using Game;
using System;
using System.Linq;

class LevelSelection {
	public static int UnlockedLevelID {
		get {
			return unlockedLevelID;
		}
	}

	private static int unlockedLevelID = Debug.isDebugBuild ? 4 : 1;
	private static Dictionary<string, int> levelsNamesByID = new Dictionary<string, int> () {
		{"Spring", 1}, {"Summer", 2}, {"Fall", 3}, {"Winter", 4}, {"Levels", 5} // TODO: replace Levels by Victory Scene
	};
	private static Dictionary<int, string> levelsIDsByName = levelsNamesByID.ToDictionary (k => k.Value, k => k.Key);

	public static int LevelID (string levelName) {
		return levelsNamesByID [levelName];
	}
	
	public static string LevelName (int levelID) {
		return levelsIDsByName [levelID];
	}

	public static void UnlockNextLevel () {
		unlockedLevelID = Math.Max (1, LevelID (Application.loadedLevelName) + 1);
	}

	public static bool CanPlay (string level) {
		return LevelID(level) <= unlockedLevelID;
	}

	public static void LoadLevel (string levelName) {
		if (CanPlay (levelName))
			Application.LoadLevel (levelName);
	}

	public void LoadLevels () {
		Application.LoadLevel ("Levels");
	}

	public void LoadNextLevel () {
		TimeManager.Speed1 ();

		var nextLevel = LevelName(LevelID(Application.loadedLevelName) + 1);
		Application.LoadLevel (nextLevel);
	}

	public void ReplayLevel () {
		Application.LoadLevel (Application.loadedLevelName);
	}
}
