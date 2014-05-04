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

	private WaveManager waveManager;

	public Maze Maze { get; private set; }
	
	public static GameLogic I {
		get {
			return FindObjectOfType<GameLogic> ();
		}
	}

	void Start () {
		Maze = new Maze (gameObject, checkpoints);
		
		waveManager = new WaveManager (this, enemyPrefab, Maze, enemyParent);
		waveManager.StartNextWave ();
	}

	void Update () {
		PlayTime += Time.deltaTime;
		waveManager.Update ();
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
