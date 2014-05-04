using UnityEngine;
using Game;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

class Wave {
	GameObject enemyPrefab;
	Transform enemyParent;
	int amount;
	GameLogic game;
	LightCheckpoint start;
	IList<Enemy> enemies;
	
	public bool Finished {
		get{ return enemies.All ((el) => el.Finished);}
	}

	public Wave (GameLogic game, int amount, GameObject enemyPrefab, LightCheckpoint start, Transform enemyParent) {
		this.game = game;
		this.amount = amount;
		this.enemyPrefab = enemyPrefab;
		this.start = start;
		this.enemyParent = enemyParent;
	}

	public void Start () {
		enemies = new List<Enemy> ();

		for (var i = 0; i < amount; ++i) {
			var enemy = ObjectFactory.CreateSmallEnemy (enemyParent, enemyPrefab, game, start.Position, 4f, 20f, i / 2.0f);
			enemies.Add (enemy);

			/*var enemyObject = (GameObject)GameObject.Instantiate (enemyPrefab, start.Position, Quaternion.identity).GetComponent<Enemy> ();	
			var enemy = enemyObject.GetComponent<Enemy> ();
			enemy.transform.parent = enemyParent;
			enemy.game = game;
			enemy.speed = 4f;
			enemy.health = 20f;
			enemy.SleepFor = i/2.0f;*/
		}
		// TODO: use this instead of sleep for? yield return new WaitForSeconds(1);
	}
}

