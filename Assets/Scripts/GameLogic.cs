using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public float PlayTime{ get; private set; }

	public int EnemiesSurvived { get; private set; }

	public int EnemiesKilled { get; private set; }

	// Checkpoints
	public Checkpoint[] checkpoints;
	public Checkpoint endCheckpoint { get { return checkpoints [checkpoints.Length - 1]; } } 

	void Update ()
	{
		PlayTime += Time.deltaTime;
	}

	private int IndexOfCheckpoint (Checkpoint checkpoint) {
		for (int i = 0; i < checkpoints.Length; ++i) {
			if (checkpoints[i] == checkpoint) {
				return i;
			}
		}
		return -1;
	}
	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint)
	{
		int index = IndexOfCheckpoint (currentCheckpoint);

		if (index == -1)
			return checkpoints[0];
		else if (index < checkpoints.Length-1)
			return checkpoints[index+1];

		// This should never happen!
		return endCheckpoint;
	}

	public void Survived (Enemy enemy)
	{
		EnemiesSurvived += 1;
	}

	public void Killed (Enemy enemy)
	{
		EnemiesKilled += 1;
	}
}
