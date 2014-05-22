using UnityEngine;
using System.Collections;
using Game;

public class TimeManager {

	private static TimeManager _timeManager = new TimeManager ();
	private float speed = 1.0f;

	public static float GetDeltaTime () {
		return _timeManager.DeltaTime;
	}
	
	public static void Faster () {
		_timeManager.speed = Mathf.Min(3.0f, _timeManager.speed + 0.5f);
	}
	
	public static void Slower () {
		_timeManager.speed = Mathf.Max(0.0f, _timeManager.speed - 0.5f);
	}
	
	public float DeltaTime {
		get {
			return Time.deltaTime * speed;
		}
	}

	private TimeManager () {
	}
}

