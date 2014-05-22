using UnityEngine;
using System.Collections;
using Game;

public class Enemy : MonoBehaviour {
	
	public double health = 50;
	public GameLogic game;
	public float speed = 2.5f;

	public float SleepFor { private get; set; }
	
	private EnemyState state = EnemyState.Waiting;

	public bool Finished { get { return state == EnemyState.Dead || state == EnemyState.Survived; } }

	public bool Dead { get { return state == EnemyState.Dead; } }

	public bool Survived { get { return state == EnemyState.Survived; } }

	public EnemyState State {
		get{ return state;}
		private set{ state = value;}
	}

	private LightCheckpoint nextCheckpoint;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Finished)
			return;
		if (SleepFor >= 0) {
			SleepFor -= TimeManager.GetDeltaTime ();
			if (SleepFor >= 0)
				return;

			state = EnemyState.Running;
		}
		if (nextCheckpoint == null)
			nextCheckpoint = game.Maze.NextCheckpoint (nextCheckpoint);
		if (nextCheckpoint == null)
			return;
		if (gameObject == null)
			return;

		CheckPoisoning ();

		var checkpointPosition = nextCheckpoint.Position;
		var myPosition = transform.position;

		var directionTowardsEnemy = checkpointPosition - myPosition;
		var movementTowardsEnemy = directionTowardsEnemy; //new Vector3(directionTowardsEnemy.x, 0, directionTowardsEnemy.z);


		var delta = 2.0;
		if (movementTowardsEnemy.magnitude < delta) {
			if (game.Maze.EndCheckpoint == nextCheckpoint) {
				state = EnemyState.Survived;
				game.Finished (this);
				Destroy (gameObject);
				return;
			}
			nextCheckpoint = game.Maze.NextCheckpoint (nextCheckpoint);
			return;
		}

		//var movement = speed * TimeManager.GetDeltaTime() * movementTowardsEnemy.normalized;
		//gameObject.transform.Translate (movement);
		
		if (GameLogic.I.AreEnemiesFrozen)
			return;

		gameObject.transform.LookAt (nextCheckpoint.Position);
		gameObject.transform.Translate (Vector3.forward * TimeManager.GetDeltaTime () * speed);
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag != "Projectile")
			return;

		Projectile projectile = other.gameObject.GetComponent<Projectile> ();
		health -= projectile.myDamage;
		Destroy (other.gameObject);
		CheckHealth (projectile);
	}

	void CheckHealth (Projectile projectile = null) {
		if (health > 0)
			return;

		if (projectile != null)
			projectile.origin.Root.Grow (0.1f);

		state = EnemyState.Dead;
		game.Finished (this);
		Destroy (gameObject);
	}

	void CheckPoisoning () {
		if (!GameLogic.I.IsPoisonSpellActive)
			return;

		var damage = TimeManager.GetDeltaTime () * 0.4f;
		Damage (damage);
	}

	public void Damage (float damage) {
		if (state != EnemyState.Running)
			return;

		health -= damage;
		CheckHealth ();
	}
}
