using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour {

	public static Tile[,] GenerateChunk(int x, int y) {
		int chunkSize = WorldData.CHUNK_SIZE;
		Tile[,] tiles = new Tile[chunkSize, chunkSize];
		for (int cx = 0; cx < chunkSize; cx++) {
			for (int cy = 0; cy < chunkSize; cy++) {
				int px = x * chunkSize + cx;
				int py = y * chunkSize + cy;
				float perlin = FractalNoise(px * 0.1f, py * 0.1f, 8, 6.5f, 0.5f);
				int type = (perlin < 0.3f) ? 1 : 0;
				tiles[cx, cy] = new Tile(type);
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