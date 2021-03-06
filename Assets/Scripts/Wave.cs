using UnityEngine;
using Game;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class Wave {
	int amount;
	float speed;
	float health;
	IList<Enemy> enemies;
	WaveConfig waveConfig;

	public void DamageAllEnemies (float damage) {
		foreach (var el in enemies)
			el.Damage (damage);
	}
	
	public bool Finished {
		get{ return enemies.All ((el) => el.Finished);}
	}

	public Wave (WaveConfig waveConfig, int amount, float speed, float health) {
		this.waveConfig = waveConfig;
		this.amount = amount;
		this.speed = speed;
		this.health = health;
	}

	public void Start () {
		enemies = new List<Enemy> ();

		for (var i = 0; i < amount; ++i) {
			var enemy = ObjectFactory.CreateSmallEnemy (waveConfig, speed, health, i * 0.8f);
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

