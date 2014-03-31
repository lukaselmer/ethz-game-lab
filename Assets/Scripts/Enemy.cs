using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public double health = 50;
	public GameLogic game;
	public float speed = 2.5f;
	
	private Checkpoint nextCheckpoint;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(nextCheckpoint == null) nextCheckpoint = game.NextCheckpoint(nextCheckpoint);
		if(nextCheckpoint == null) return;
		if(gameObject == null) return;

		var checkpointPosition = nextCheckpoint.Position;
		var myPosition = transform.position;

		var directionTowardsEnemy = checkpointPosition - myPosition;
		var movementTowardsEnemy = new Vector3(directionTowardsEnemy.x, 0, directionTowardsEnemy.z);

		var delta = 0.25;
		if(movementTowardsEnemy.magnitude < delta){
			if(game.endCheckpoint == nextCheckpoint){
				game.Survived(this);
				Destroy(gameObject);
				return;
			}
			nextCheckpoint = game.NextCheckpoint(nextCheckpoint);
			return;
		}

		var movement = speed * Time.deltaTime * movementTowardsEnemy.normalized;
		gameObject.transform.Translate(movement);
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Projectile") {

			Projectile projectile = other.gameObject.GetComponent<Projectile>();

			health -= projectile.myDamage;

			Destroy(other.gameObject);

			if (health <= 0) {
				game.Killed(this);
				Destroy(gameObject);
			}
		}
	}
}
