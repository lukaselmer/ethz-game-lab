using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game {
	public class PathPainter {
		private List<LightCheckpoint> Checkpoints { get; set; }

		TerrainData terrainData;
		Terrain terrain;
		float widthHeight;
		int pathSize = 20;
		
		public PathPainter (List<LightCheckpoint> checkpoints, Terrain terrain) {
			this.Checkpoints = checkpoints;

			terrainData = terrain.terrainData;
			widthHeight = terrainData.size.x;
			var leftTop = terrain.transform.position.x;
			pathSize = (int)(widthHeight / 10);

			if (widthHeight != terrainData.size.z || leftTop != terrain.transform.position.z || widthHeight + 2 * leftTop != 0.0)
				throw new Exception ("Invalid terrain configuration!");
		}

		private float[,,] CreateTerrainMap () {
			var map = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
			//var map = terrainData.GetAlphamaps (0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
			for (var x = 0; x < terrainData.alphamapWidth; ++x) {
				for (var y = 0; y < terrainData.alphamapHeight; ++y) {
					map [x, y, 0] = 1f;
					map [x, y, 1] = 0f;
					map [x, y, 2] = 0f;
				}
			}
			return map;
		}

		private void PaintCheckpoint (float[,,] map, Vector2 pos) {
			PaintPoint (map, pos, 1, pathSize / 2);
		}
		
		private void PaintPoint (float[,,] map, Vector2 pos, int index, int size) {
			var cx = (pos.x + widthHeight / 2) / widthHeight * map.GetLength (0);
			var cy = (pos.y + widthHeight / 2) / widthHeight * map.GetLength (1);
			
			DrawPoint (map, (int)cx, (int)cy, index, size);
		}

		private void DrawPoint (float[,,] map, int cx, int cy, int index, int size) {
			var h = size;
			float maxDist = (float) h * (float)Math.Sqrt (2);

			for (int x = cx - h; x < cx + h; ++x) {
				for (int y = cy - h; y < cy + h; ++y) {
					float dx = Math.Abs (x - cx);
					float dy = Math.Abs (y - cy);
					float distToCenter = (float)Math.Sqrt (dx * dx + dy * dy);
					float intensity = 1.0f - distToCenter / maxDist;

					if (index == 2) {
						map [y, x, 0] = 1f-intensity;
					} else {
						map [y, x, 0] = 1f;
					}
					map [y, x, index] = Math.Max (intensity, map [y, x, 1]);
				}
			}
		}
		
		public void PaintPath () {
			var map = CreateTerrainMap ();

			foreach (var checkpoint in Checkpoints) {
				PaintCheckpoint (map, new Vector2 (checkpoint.Position.x, checkpoint.Position.z));
			}

			var c1 = Checkpoints [0].Position;
			PaintPoint (map, new Vector2 (c1.x, c1.z), 2, pathSize);
			var c2 = Checkpoints [Checkpoints.Count - 1].Position;
			PaintPoint (map, new Vector2 (c2.x, c2.z), 2, pathSize);
			
			terrainData.SetAlphamaps (0, 0, map);
		}
	}
}

