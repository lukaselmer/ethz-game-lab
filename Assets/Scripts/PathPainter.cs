using System;
using UnityEngine;

namespace Game
{
	public class PathPainter
	{
		private Checkpoint[] Checkpoints { get; set; }

		TerrainData terrainData;
		Terrain terrain;
		float widthHeight;
		const int pathSize = 40;
		
		public PathPainter (Checkpoint[] checkpoints, Terrain terrain)
		{
			this.Checkpoints = checkpoints;

			terrainData = terrain.terrainData;
			widthHeight = terrainData.size.x;
			var leftTop = terrain.transform.position.x;

			if (widthHeight != terrainData.size.z || leftTop != terrain.transform.position.z || widthHeight + 2 * leftTop != 0.0)
				throw new Exception ("Invalid terrain configuration!");
		}

		private float[,,] CreateTerrainMap ()
		{
			var map = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, 2];
			for (var x = 0; x < terrainData.alphamapWidth; ++x) {
				for (var y = 0; y < terrainData.alphamapHeight; ++y) {
					map [x, y, 0] = 1f;
					map [x, y, 1] = 0f;
				}
			}
			return map;
		}

		private void PaintCheckpoint (float[,,] map, Vector2 pos)
		{
			var cx = (pos.x + widthHeight / 2) / widthHeight * map.GetLength (0);
			var cy = (pos.y + widthHeight / 2) / widthHeight * map.GetLength (1);

			DrawPoint (map, (int)cx, (int)cy);
		}

		private void DrawPoint (float[,,] map, int cx, int cy)
		{
			var h = pathSize / 2;
			float maxDist = (float) Math.Sqrt(h*h*2);

			for (int x = cx - h; x < cx + h; ++x) {
				for (int y = cy - h; y < cy + h; ++y) {
					float dx = Math.Abs (x - cx);
					float dy = Math.Abs (y - cy);
					float distToCenter = (float) Math.Sqrt(dx * dx + dy * dy);
					float intensity = 1.0f - distToCenter / maxDist;

					map [y, x, 0] = 1f;
					map [y, x, 1] = Math.Max (intensity, map [y, x, 1]);
				}
			}
		}
		
		public void PaintPath ()
		{
			var map = CreateTerrainMap ();
			var xmin = terrainData.alphamapWidth / 2;
			var ymin = terrainData.alphamapHeight / 2;

			foreach (var checkpoint in Checkpoints) {
				PaintCheckpoint (map, new Vector2 (checkpoint.Position.x, checkpoint.Position.z));
			}
			
			terrainData.SetAlphamaps (0, 0, map);
		}
	}
}

