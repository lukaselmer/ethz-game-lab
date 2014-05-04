using System;
using UnityEngine;


namespace Game {
	public class WaveManager {
		private int interval = 0;
		private Wave currentWave;

		LightCheckpoint start;

		GameObject enemyPrefab;
		Transform enemyParent;

		//private float wait = 0;

		GameLogic game;

		public WaveManager (GameLogic game, GameObject enemyPrefab, Maze maze, Transform enemyParent) {
			this.game = game;
			this.enemyPrefab = enemyPrefab;
			this.start = maze.StartCheckpoint;
			this.enemyParent = enemyParent;
		}

		public void StartWaves () {
			currentWave = new Wave(game, 20, enemyPrefab, start, enemyParent);
			currentWave.Start();
		}

		public void Update () {
			if(interval++ % 1000 == 0){
				CheckWave();
			}
		}

		void CheckWave () {
			if(currentWave == null) return;
			if(!currentWave.Finished) return;

			//currentWave = new Wave(this, 20, enemyPrefab, start);
			//currentWave.Start();
		}
	}
}

