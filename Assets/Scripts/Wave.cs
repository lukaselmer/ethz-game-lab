using UnityEngine;
using System.Collections;
using Game;

class Wave {
	GameObject enemyPrefab;

	int amount;

	GameLogic game;

	Checkpoint start;
	
	public Wave(GameLogic game, int amount, GameObject enemyPrefab, Checkpoint start){
		this.game = game;
		this.amount = amount;
		this.enemyPrefab = enemyPrefab;
		this.start = start;
	}

	public void Start () {
		//towerObject.transform.parent = towerParent;

		for(var i = 0; i < amount; ++i){
			var enemyObject = (GameObject) GameObject.Instantiate (enemyPrefab, start.Position, Quaternion.identity);	
			var enemy = enemyObject.GetComponent<Enemy>();
			enemy.game = game;
			enemy.speed = i + 10f;
			enemy.SleepFor = i;
		}

	}
}

