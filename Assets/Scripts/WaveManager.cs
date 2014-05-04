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

		private WaveConfig _waveConfig;

		public WaveManager (GameLogic game, GameObject enemyPrefab, Maze maze, Transform enemyParent) {
			_waveConfig = new WaveConfig(game, enemyPrefab, maze, enemyParent);

			waves = new List<Wave>();
			addWaves();
		}

		void addWaves () {
			waves.Add(new Wave(_waveConfig, 5, 4f, 20f));
			waves.Add(new Wave(_waveConfig, 8, 4f, 20f));
			waves.Add(new Wave(_waveConfig, 5, 8f, 10f));
			waves.Add(new Wave(_waveConfig, 10, 6f, 20f));
			waves.Add(new Wave(_waveConfig, 15, 4f, 20f));
			waves.Add(new Wave(_waveConfig, 20, 4f, 30f));
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

