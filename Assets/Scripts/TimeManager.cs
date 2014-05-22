using UnityEngine;
using System.Collections;
using Game;

public class TimeManager {

	private static TimeManager _timeManager = new TimeManager ();
	private float speed = 1.0f;

	public static float GetDeltaTime () {
		return _timeManager.DeltaTime;
	}

	public static void Pause () {
		_timeManager.speed = 0;
	}
	
	public static void Speed1 () {
		_timeManager.speed = 1;
	}
	
	public static void Speed2 () {
		_timeManager.speed = 2;
	}
	
	public static void Speed3 () {
		_timeManager.speed = 4;
	}

	public float DeltaTime {
		get {
			return Time.deltaTime * speed;
		}
	}

	private TimeManager () {
	}
}

