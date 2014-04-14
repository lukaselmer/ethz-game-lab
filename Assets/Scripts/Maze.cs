using System;
using UnityEngine;

namespace Game {
	public class Maze {
		private GameObject gameObject;
		private Checkpoint[] checkpoints;
		private Checkpoint[] interpolatedCheckpoints;

		public Checkpoint EndCheckpoint {
			get{ return checkpoints [checkpoints.Length - 1];}
		}

		public Checkpoint StartCheckpoint {
			get{ return checkpoints [0];}
		}

		public Maze (GameObject gameObject, Checkpoint[] checkpoints) {
			this.gameObject = gameObject;
			this.checkpoints = checkpoints;
			this.interpolatedCheckpoints = CalculateInterpolateCheckpoints (checkpoints, 4);
		}

		public Checkpoint[] InterpolateCheckpoints () {
			return interpolatedCheckpoints;
		}

		public int IndexOfCheckpoint (Checkpoint checkpoint) {
			if (interpolatedCheckpoints == null)
				return 0;
			
			for (int i = 0; i < interpolatedCheckpoints.Length; ++i) {
				if (interpolatedCheckpoints [i] == checkpoint) {
					return i;
				}
			}
			return -1;
		}

		public Checkpoint NextCheckpoint (Checkpoint currentCheckpoint) {
			int index = IndexOfCheckpoint (currentCheckpoint);
			
			if (index == -1)
				return interpolatedCheckpoints [0];
			else if (index < interpolatedCheckpoints.Length - 1)
				return interpolatedCheckpoints [index + 1];
			
			// This should never happen!
			return EndCheckpoint;
		}

		public Checkpoint[] CalculateInterpolateCheckpoints (Checkpoint[] checkpoints, int limit) {
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
			
			return CalculateInterpolateCheckpoints (newCheckpoints, limit - 1);
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

