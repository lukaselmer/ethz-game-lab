using UnityEngine;
using Game;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;

namespace Game {
	public class ObjectFactory: MonoBehaviour {
		protected static ObjectFactory instance;

		// TODO: implement this: public Transform enemyParent;

		// TODO: implement this: public GameObject smallEnemyPrefab;
		// TODO: implement this: public GameObject bigEnemyPrefab;

		void Start () {
			instance = this;
		}

		public static Enemy CreateSmallEnemy(WaveConfig waveConfig, float speed, float health, float sleep)
		{	
			var enemyObject = (GameObject)GameObject.Instantiate (waveConfig.EnemyPrefab /*TODO: use instance.smallEnemyPrefab*/, waveConfig.Maze.StartCheckpoint.Position, Quaternion.identity);	
			var enemy = enemyObject.GetComponent<Enemy> ();
			enemy.transform.parent = waveConfig.EnemyParent;
			enemy.game = waveConfig.Game;
			enemy.speed = speed;
			enemy.health = health;
			enemy.SleepFor = sleep;
			return enemy;
		}
	}
}

