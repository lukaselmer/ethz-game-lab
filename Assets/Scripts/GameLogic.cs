using UnityEngine;
using System.Collections.Generic;
using Game;
using System;

public class GameLogic : MonoBehaviour {
	public float PlayTime{ get; private set; }

	public int EnemiesSurvived { get; private set; }

	public int EnemiesKilled { get; private set; }
	
	public GameObject enemyPrefab;
	public Transform enemyParent;

	// Checkpoints
	public Checkpoint[] checkpoints;
	private Checkpoint[] interpolatedCheckpoints;

	public Checkpoint EndCheckpoint { get { return interpolatedCheckpoints [interpolatedCheckpoints.Length - 1]; } }
	public Checkpoint StartCheckpoint { get { return interpolatedCheckpoints [0]; } }

	private WaveManager waveManager;

	
	public static GameLogic Instance {
		get {
			return FindObjectOfType<GameLogic>();
		}
	}

	void Start () {
		interpolatedCheckpoints = new Maze(gameObject).InterpolateCheckpoints (checkpoints, 4);
		new PathPainter (interpolatedCheckpoints, Terrain.activeTerrain).PaintPath ();
		
		waveManager = new WaveManager(this, enemyPrefab, StartCheckpoint, enemyParent);
		waveManager.StartWaves();
	}

	void Update () {
		PlayTime += Time.deltaTime;
		waveManager.Update();
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
		return EndCheckpoint;
	}

	public void Finished (Enemy enemy) {
		if(enemy.Dead) Killed (enemy);
		else if(enemy.Survived) Survived(enemy);
		else throw new ApplicationException("Invalid enemy state: " + enemy.State);
	}

	public void Survived (Enemy enemy) {
		EnemiesSurvived += 1;
	}

	public void Killed (Enemy enemy) {
		EnemiesKilled += 1;
	}
}
