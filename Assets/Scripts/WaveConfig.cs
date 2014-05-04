using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Game {
	public class WaveConfig {
		
		public GameLogic Game {
			get;
			private set;
		}

		public GameObject EnemyPrefab {
			get;
			private set;
		}

		public Maze Maze {
			get;
			private set;
		}
		
		public Transform EnemyParent {
			get;
			private set;
		}

		public WaveConfig (GameLogic game, GameObject enemyPrefab, Maze maze, Transform enemyParent) {
			Game = game;
			EnemyPrefab = enemyPrefab;
			Maze = maze;
			EnemyParent = enemyParent;
		}
	}

}

