//using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Game {
	public class Maze {
		private GameObject gameObject;
		private List<LightCheckpoint> checkpoints;
		private List<LightCheckpoint> interpolatedCheckpoints;
		const float MAX_CHECKPOINT_DISTANCE = 0.1f;

		public LightCheckpoint EndCheckpoint {
			get{ return interpolatedCheckpoints [interpolatedCheckpoints.Count - 1];}
		}

		public LightCheckpoint StartCheckpoint {
			get{ return interpolatedCheckpoints [0];}
		}

		public Maze (GameObject gameObject, Checkpoint[] unityCheckpoints) {
			this.gameObject = gameObject;
			this.checkpoints = unityCheckpoints.Select (el => new LightCheckpoint (el.Position)).ToList ();
			this.interpolatedCheckpoints = CalculateInterpolateCheckpoints (checkpoints, 4);
			new PathPainter (interpolatedCheckpoints, Terrain.activeTerrain).PaintPath ();
		}

		public int IndexOfCheckpoint (LightCheckpoint checkpoint) {
			if (interpolatedCheckpoints == null)
				return 0;
			
			for (int i = 0; i < interpolatedCheckpoints.Count; ++i) {
				if (interpolatedCheckpoints [i] == checkpoint) {
					return i;
				}
			}
			return -1;
		}

		public LightCheckpoint NextCheckpoint (LightCheckpoint currentCheckpoint) {
			int index = IndexOfCheckpoint (currentCheckpoint);
			
			if (index == -1)
				return interpolatedCheckpoints [0];
			else if (index < interpolatedCheckpoints.Count - 1)
				return interpolatedCheckpoints [index + 1];
			
			// This should never happen!
			return EndCheckpoint;
		}

		public List<LightCheckpoint> CalculateInterpolateCheckpoints (List<LightCheckpoint> checkpoints, int limit) {
			if (limit == 0)
				return checkpoints;

			var newCheckpoints = new List<LightCheckpoint> ();
			foreach (var checkpoint in checkpoints) {
				if (newCheckpoints.Count == 0) {
					newCheckpoints.Add (checkpoint);
					continue;
				}
				var p1 = newCheckpoints.Last ().Position;
				var steps = CalculateSteps (p1, checkpoint.Position);
				AddCheckpoints (newCheckpoints, checkpoint, p1, steps);
			}

			return newCheckpoints;
		}

		static void AddCheckpoints (List<LightCheckpoint> newCheckpoints, LightCheckpoint checkpoint, Vector3 p1, int steps) {
			for (var step = 1; step < steps; step++) {
				var currentLerp = 1f / steps * step;
				var pos = Vector3.Lerp (p1, checkpoint.Position, currentLerp);
				pos += new Vector3(Random.Range(-.2f, .2f), Random.Range(-.05f, .05f), Random.Range(-.2f, .2f));
				newCheckpoints.Add (new LightCheckpoint (pos));
			}
			newCheckpoints.Add (checkpoint);
		}

		static int CalculateSteps (Vector3 c1, Vector3 c2) {
			var lerpSize = .5f;
			var count = 0;
			while (count == 0 || Vector3.Distance (c1, Vector3.Lerp (c1, c2, lerpSize)) > MAX_CHECKPOINT_DISTANCE) {
				lerpSize /= 2f;
				count += 1;
			}
			// calculate 2^count
			return 1 << count;
		}
	}
}

