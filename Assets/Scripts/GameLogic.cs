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
	public Checkpoint[] checkpoints;

	public Checkpoint EndCheckpoint { get { return Maze.EndCheckpoint; } }

	public Checkpoint StartCheckpoint { get { return Maze.StartCheckpoint; } }

	private WaveManager waveManager;

	public Maze Maze { get; private set; }
	
	public static GameLogic I {
		get {
			return FindObjectOfType<GameLogic> ();
		}
	}

	void Start () {
		Maze = new Maze (gameObject, checkpoints);
		
		waveManager = new WaveManager (this, enemyPrefab, StartCheckpoint, enemyParent);
		waveManager.StartWaves ();
	}

	void Update () {
		PlayTime += Time.deltaTime;
		waveManager.Update ();
	}

	private int IndexOfCheckpoint (Checkpoint checkpoint) {
		return Maze.IndexOfCheckpoint (checkpoint);

	}

	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint) {
		return Maze.NextCheckpoint (currentCheckpoint);
	}

	public void Finished (Enemy enemy) {
		if (enemy.Dead)
			Killed (enemy);
		else if (enemy.Survived)
			Survived (enemy);
		else
			throw new ApplicationException ("Invalid enemy state: " + enemy.State);
	}

	public void Survived (Enemy enemy) {
		EnemiesSurvived += 1;
	}

	public void Killed (Enemy enemy) {
		EnemiesKilled += 1;
	}
}
