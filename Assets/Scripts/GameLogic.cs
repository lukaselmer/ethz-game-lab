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

	public Checkpoint EndCheckpoint { get { return maze.EndCheckpoint; } }
	public Checkpoint StartCheckpoint { get { return maze.StartCheckpoint; } }

	private WaveManager waveManager;

	private Maze maze;
	
	public static GameLogic Instance {
		get {
			return FindObjectOfType<GameLogic>();
		}
	}

	void Start () {
		maze = new Maze(gameObject, checkpoints);
		new PathPainter (maze.InterpolateCheckpoints (), Terrain.activeTerrain).PaintPath ();
		
		waveManager = new WaveManager(this, enemyPrefab, StartCheckpoint, enemyParent);
		waveManager.StartWaves();
	}

	void Update () {
		PlayTime += Time.deltaTime;
		waveManager.Update();
	}

	private int IndexOfCheckpoint (Checkpoint checkpoint) {
		return maze.IndexOfCheckpoint(checkpoint);

	}

	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint) {
		return maze.NextCheckpoint(currentCheckpoint);
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
