using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerTargetController : MonoBehaviour {
	
	private HashSet<GameObject> targets = new HashSet<GameObject> ();
	public Transform CurrentTarget { get; private set; }


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			targets.Add (other.gameObject);
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.transform == CurrentTarget) {
			targets.Remove (other.gameObject);
		}
	}
		
	public void SetNextTarget ()
	{
		// remove destroyed targets, because they don't trigger OnTriggerExit
		targets.RemoveWhere (i => i == null);
		
		GameObject nextTarget = null;
		double minHealth = double.MaxValue;
		foreach (var enemyObject in targets) {
			Enemy enemy = enemyObject.GetComponent<Enemy> ();
			if (enemy.health < minHealth) {
				nextTarget = enemyObject;
				minHealth = enemy.health;
			}
		}
		
		//		if (nextTarget != null && nextTarget.transform != currentTarget) 
		//			nextFireTime = Time.time + reloadTime * 0.5;

		CurrentTarget = nextTarget ? nextTarget.transform : null;
	}
}
