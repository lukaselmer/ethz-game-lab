﻿using UnityEngine;
using System.Collections;
using Game;

public class GameLogic : MonoBehaviour {
	public float PlayTime{ get; private set; }

	public int EnemiesSurvived { get; private set; }

	public int EnemiesKilled { get; private set; }

	// Checkpoints
	public Checkpoint[] checkpoints;
	private Checkpoint[] interpolatedCheckpoints;

	public Checkpoint endCheckpoint { get { return interpolatedCheckpoints [interpolatedCheckpoints.Length - 1]; } }

	void Start () {
		interpolatedCheckpoints = new Maze(gameObject).InterpolateCheckpoints (checkpoints, 4);
		new PathPainter (interpolatedCheckpoints, Terrain.activeTerrain).PaintPath ();
	}

	void Update () {
		PlayTime += Time.deltaTime;
	}

	private int IndexOfCheckpoint (Checkpoint checkpoint) {
		if (interpolatedCheckpoints == null)
			return 0;

		for (int i = 0; i < interpolatedCheckpoints.Length; ++i) {
			if (interpolatedCheckpoints [i] == checkpoint) {
				return i;
			}
		}
		return -1;
	}

	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint) {
		int index = IndexOfCheckpoint (currentCheckpoint);

		if (index == -1)
			return interpolatedCheckpoints [0];
		else if (index < interpolatedCheckpoints.Length - 1)
			return interpolatedCheckpoints [index + 1];

		// This should never happen!
		return endCheckpoint;
	}

	public void Survived (Enemy enemy) {
		EnemiesSurvived += 1;
	}

	public void Killed (Enemy enemy) {
		EnemiesKilled += 1;
	}
}
