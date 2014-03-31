using System;
using UnityEngine;

namespace Game {
	public class Maze {
		GameObject gameObject;

		public Maze (GameObject gameObject) {
			this.gameObject = gameObject;
		}
		
		public Checkpoint[] InterpolateCheckpoints (Checkpoint[] checkpoints, int limit) {
			if (limit == 0)
				return checkpoints;
			
			var newCheckpoints = new Checkpoint[checkpoints.Length * 2 - 1];
			for (var i = 0; i < checkpoints.Length; ++i) {
				var c = checkpoints [i];
				newCheckpoints [i * 2] = c;
				if (i == 0)
					continue;
				
				newCheckpoints [i * 2 - 1] = Interpolate (newCheckpoints [i * 2 - 2], newCheckpoints [i * 2]);
			}
			
			return InterpolateCheckpoints (newCheckpoints, limit - 1);
		}
		
		Checkpoint Interpolate (Checkpoint c1, Checkpoint c2) {
			Checkpoint c = gameObject.AddComponent<Checkpoint> ();
			c.Position = Vector3.Lerp (c1.Position, c2.Position, .5f);
			// Why does this not work!?
			// c.transform.position = Vector3.Lerp (c1.Position, c2.Position, .5f);
			return c;
		}

	}
}

