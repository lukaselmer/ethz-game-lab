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

	private Checkpoint nextCheckpoint;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Finished)
			return;
		if (SleepFor >= 0) {
			SleepFor -= Time.deltaTime;
			if (SleepFor >= 0)
				return;

			state = EnemyState.Running;
		}
		if (nextCheckpoint == null)
			nextCheckpoint = game.NextCheckpoint (nextCheckpoint);
		if (nextCheckpoint == null)
			return;
		if (gameObject == null)
			return;

		var checkpointPosition = nextCheckpoint.Position;
		var myPosition = transform.position;

		var directionTowardsEnemy = checkpointPosition - myPosition;
		var movementTowardsEnemy = directionTowardsEnemy; //new Vector3(directionTowardsEnemy.x, 0, directionTowardsEnemy.z);

		var delta = 0.25;
		if (movementTowardsEnemy.magnitude < delta) {
			if (game.EndCheckpoint == nextCheckpoint) {
				state = EnemyState.Survived;
				game.Finished (this);
				Destroy (gameObject);
				return;
			}
			nextCheckpoint = game.NextCheckpoint (nextCheckpoint);
			return;
		}

		var movement = speed * Time.deltaTime * movementTowardsEnemy.normalized;
		gameObject.transform.Translate (movement);
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Projectile") {

			Projectile projectile = other.gameObject.GetComponent<Projectile> ();

			health -= projectile.myDamage;

			Destroy (other.gameObject);

			if (health <= 0) {
				state = EnemyState.Dead;
				game.Finished (this);
				Destroy (gameObject);
			}
		}
	}
}
