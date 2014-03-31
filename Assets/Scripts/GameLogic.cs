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

	void Start ()
	{
		var t = Terrain.activeTerrain;
		var d = t.terrainData;
		var map = new float[d.alphamapWidth, d.alphamapHeight, 2];

		var xmin = d.alphamapWidth / 2;
		var ymin = d.alphamapHeight / 2;
		var delta = 50;

		for (var x = 0; x < d.alphamapWidth; ++x) {
			for (var y = 0; y < d.alphamapHeight; ++y) {
				if (x > xmin - delta && y > ymin - delta && x < xmin + delta && y < ymin + delta) {
					map [x, y, 0] = 0f;
					map [x, y, 1] = 1f;
				} else {
					map [x, y, 0] = 1f;
					map [x, y, 1] = 0f;
				}
			}
		}
		
		t.terrainData.SetAlphamaps (0, 0, map);
	}

	void Update ()
	{
		PlayTime += Time.deltaTime;
	}

	private int IndexOfCheckpoint (Checkpoint checkpoint)
	{
		for (int i = 0; i < checkpoints.Length; ++i) {
			if (checkpoints [i] == checkpoint) {
				return i;
			}
		}
		return -1;
	}

	public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint)
	{
		int index = IndexOfCheckpoint (currentCheckpoint);

		if (index == -1)
			return checkpoints [0];
		else if (index < checkpoints.Length - 1)
			return checkpoints [index + 1];

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
