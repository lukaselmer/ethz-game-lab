using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace Game {
	public class WaveManager {
		private int interval = 0;
		private int pauseBetweenWaves = 5;
		private Wave currentWave;

		LightCheckpoint start;

		GameObject enemyPrefab;
		Transform enemyParent;
		
		private float pauseBetweenNextWave = 0;
		private int currentWaveNumber = 0;
		private IList<Wave> waves;

		GameLogic game;

		public WaveManager (GameLogic game, GameObject enemyPrefab, Maze maze, Transform enemyParent) {
			this.game = game;
			this.enemyPrefab = enemyPrefab;
			this.start = maze.StartCheckpoint;
			this.enemyParent = enemyParent;

			waves = new List<Wave>();
			addWaves();
		}

		void addWaves () {
			waves.Add(new Wave(game, 5, enemyPrefab, start, enemyParent));
			waves.Add(new Wave(game, 8, enemyPrefab, start, enemyParent));
			waves.Add(new Wave(game, 10, enemyPrefab, start, enemyParent));
			waves.Add(new Wave(game, 15, enemyPrefab, start, enemyParent));
			waves.Add(new Wave(game, 20, enemyPrefab, start, enemyParent));
		}

		public void StartNextWave () {
			currentWave = waves[currentWaveNumber++];
			currentWave.Start();
			pauseBetweenNextWave = pauseBetweenWaves;
		}

		public void Update () {
			if(interval++ % 10 == 0){
				CheckWave();
			}
		}

		bool HasNewWave () {
			return currentWaveNumber < waves.Count;
		}

		void CheckWave () {
			if(currentWave == null) return;
			if(!currentWave.Finished) return;
			
			pauseBetweenNextWave -= Time.deltaTime;
			if(pauseBetweenNextWave < 0) return;

			if(HasNewWave()) StartNextWave();
		}
	}
}

