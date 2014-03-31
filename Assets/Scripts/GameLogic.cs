using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public float PlayTime{ get; private set; }

	public int EnemiesSurvived { get; private set; }

	public int EnemiesKilled { get; private set; }

	// Checkpoints
	public Checkpoint start;
	public Checkpoint checkpoint1;
	public Checkpoint checkpoint2;
	public Checkpoint checkpoint3;
	public Checkpoint checkpoint4;
	public Checkpoint end;

	void Update ()
	{
		PlayTime += Time.deltaTime;
	}

	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint)
	{
		if (currentCheckpoint == null)
			return start;
		if (currentCheckpoint == start)
			return checkpoint1;
		if (currentCheckpoint == checkpoint1)
			return checkpoint2;
		if (currentCheckpoint == checkpoint2)
			return checkpoint3;
		if (currentCheckpoint == checkpoint3)
			return checkpoint4;
		if (currentCheckpoint == checkpoint4)
			return end;

		// This should never happen!
		return end;
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
