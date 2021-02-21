using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {
	void Start() {
		WorldData.InitializeWorld();

		// Create n x n chunks
		int n = 16;
		int chunkSize = WorldData.CHUNK_SIZE;
		for (int x = 0; x < n; x++) {
			for (int y = 0; y < n; y++) {
				Chunk chunk = WorldData.GenerateChunk(x, y);
				for (int cx = 0; cx < chunkSize; cx++) {
					for (int cy = 0; cy < chunkSize; cy++) {
						float perlin = Mathf.PerlinNoise((x * chunkSize + cx) * 0.1f, (y * chunkSize + cy) * 0.1f);
						chunk.tiles[cx, cy] = (perlin > 0.5);
					}
				}
			}
		}

		/*for (int x = 0; x < n; x++) {
			for (int y = 0; y < n; y++) {
				WorldData.chunks[x, y].tiles[1, 1] = true;
			}
		}*/
	}
}