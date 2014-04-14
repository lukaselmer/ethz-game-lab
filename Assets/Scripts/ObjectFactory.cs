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

		public static Enemy CreateSmallEnemy(Transform enemyParent, GameObject enemyPrefab, GameLogic game, Vector3 startPosition, float speed, float health, float sleep)
		{	
			var enemyObject = (GameObject)GameObject.Instantiate (enemyPrefab /*TODO: use instance.smallEnemyPrefab*/, startPosition, Quaternion.identity);	
			var enemy = enemyObject.GetComponent<Enemy> ();
			enemy.transform.parent = enemyParent;
			enemy.game = game;
			enemy.speed = speed;
			enemy.health = health;
			enemy.SleepFor = sleep;
			return enemy;
		}
	}
}

