using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {
	/*void Start() {
		WorldData.InitializeWorld();

		// Create n x n chunks
		int n = 16;
		int chunkSize = WorldData.CHUNK_SIZE;
		for (int x = 0; x < n; x++) {
			for (int y = 0; y < n; y++) {
				Chunk chunk = WorldData.GenerateChunk(x, y);
				for (int cx = 0; cx < chunkSize; cx++) {
					for (int cy = 0; cy < chunkSize; cy++) {
						int px = x * chunkSize + cx;
						int py = y * chunkSize + cy;
						float perlin = FractalNoise(px * 0.1f, py * 0.1f, 4, 2, 0.5f);
						chunk.tiles[cx, cy] = (perlin > 0.6f);
					}
				}
			}
		}
	}*/

	public static bool[,] GenerateChunk(int x, int y) {
		int chunkSize = WorldData.CHUNK_SIZE;
		bool[,] tiles = new bool[chunkSize, chunkSize];
		for (int cx = 0; cx < chunkSize; cx++) {
			for (int cy = 0; cy < chunkSize; cy++) {
				int px = x * chunkSize + cx;
				int py = y * chunkSize + cy;
				float perlin = FractalNoise(px * 0.1f, py * 0.1f, 8, 6.5f, 0.5f);
				tiles[cx, cy] = (perlin < 0.3f);
			}
		}
		return tiles;
	}

	public static float FractalNoise(float x, float y, int octaves, float lacunarity, float persistence) {
		float value = 0;
		float average = 0;
		for (int i = 0; i < octaves; i++) {
			float frequency = Mathf.Pow(lacunarity, i);
			float amplitude = Mathf.Pow(persistence, i);
			value += Mathf.PerlinNoise(x * frequency + (i + 1) * 1000, y * frequency + (i + 1) * 1000) * amplitude;
			average += amplitude;
		}
		return value / average;
	}
}