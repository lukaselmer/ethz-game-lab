using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game {
	public class WaveManager {
		private int interval = 0;
		private int pauseBetweenWaves = 5;
		private Wave _currentWave;
		LightCheckpoint start;
		GameObject enemyPrefab;
		Transform enemyParent;
		private float pauseBetweenNextWave = 0;
		private int currentWaveNumber = 0;
		private IList<Wave> waves;
		private WaveConfig _waveConfig;
		private bool _waveRunning;

		public int RemainingWaves {
			get {
				return waves.Count - currentWaveNumber;
			}
		}

		public Wave CurrentWave{ get { return _currentWave; } }

		public WaveManager (GameLogic game, GameObject enemyPrefab, Maze maze, Transform enemyParent) {
			_waveConfig = new WaveConfig (game, enemyPrefab, maze, enemyParent);

			waves = new List<Wave> ();
			AddWaves ();
		}

		private void AddWaves () {
			if (Application.loadedLevelName == "Spring") {
				waves.Add (new Wave (_waveConfig, 2, 8f, 20f));
				waves.Add (new Wave (_waveConfig, 4, 8f, 20f));
				waves.Add (new Wave (_waveConfig, 8, 8f, 20f));
				waves.Add (new Wave (_waveConfig, 16, 8f, 20f));
			}
			if (Application.loadedLevelName == "Summer") {
				waves.Add (new Wave (_waveConfig, 2, 8f, 20f));
				waves.Add (new Wave (_waveConfig, 4, 10f, 20f));
				waves.Add (new Wave (_waveConfig, 8, 12f, 20f));
				waves.Add (new Wave (_waveConfig, 12, 14f, 20f));
				waves.Add (new Wave (_waveConfig, 1, 6f, 60f));
			}
			if (Application.loadedLevelName == "Fall") {
				waves.Add (new Wave (_waveConfig, 10, 10f, 5f));
				waves.Add (new Wave (_waveConfig, 10, 10f, 10f));
				waves.Add (new Wave (_waveConfig, 10, 10f, 15f));
				waves.Add (new Wave (_waveConfig, 10, 10f, 20f));
				waves.Add (new Wave (_waveConfig, 10, 10f, 25f));
				waves.Add (new Wave (_waveConfig, 1, 3f, 200f));
			}
			if (Application.loadedLevelName == "Winter") {
				waves.Add (new Wave (_waveConfig, 10, 10f, 5f));
				waves.Add (new Wave (_waveConfig, 4, 8f, 20f));
				waves.Add (new Wave (_waveConfig, 8, 12f, 20f));
				waves.Add (new Wave (_waveConfig, 12, 14f, 20f));
				waves.Add (new Wave (_waveConfig, 2, 6f, 60f));
				waves.Add (new Wave (_waveConfig, 1, 6f, 180f));
			}
		}

		public void StartNextWave () {
			_currentWave = waves [currentWaveNumber++];
			_currentWave.Start ();
			_waveRunning = true;
		}

		public void Update () {
			if (interval++ % 10 == 0) 
				CheckWave ();
		}

		bool HasNewWave () {
			return currentWaveNumber < waves.Count;
		}

		void CheckWave () {
			if (_currentWave == null)
				return;
			if (!_currentWave.Finished)
				return;

			if (_waveRunning) {
				_waveRunning = false;
				pauseBetweenNextWave = pauseBetweenWaves;
			}

			pauseBetweenNextWave -= TimeManager.GetDeltaTime ();
			if (pauseBetweenNextWave < 0)
				return;

			if (HasNewWave ())
				StartNextWave ();
			else
				_waveConfig.Game.AllWavesFinished ();
		}
	}
}

