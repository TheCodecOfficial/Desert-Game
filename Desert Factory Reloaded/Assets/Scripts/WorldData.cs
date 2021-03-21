using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData
{
	public const int CHUNK_SIZE = 16;

	static WorldLoader worldLoader;

	public static Dictionary<int, int, Chunk> chunks;
	
	public static void InitializeWorld(WorldLoader wl) {
		chunks = new Dictionary<int, int, Chunk>();
		worldLoader = wl;
	}

	public static Chunk GenerateChunk(int x, int y) {
		return GenerateChunk(x, y, new Tile[CHUNK_SIZE, CHUNK_SIZE]);
	}

	public static Chunk GenerateChunk(int x, int y, Tile[,] chunkData) {
		Chunk chunk = new Chunk() { tiles = chunkData };
		chunks[x, y] = chunk;
		return chunk;
	}

	public static bool ContainsChunk(int x, int y) {
		return chunks.ContainsKey(x, y);
	}

	public static bool ContainsChunk(Chunk chunk) {
		return chunks.ContainsValue(chunk);
	}

	public static void UpdateTile(int x, int y, int type) {
		Tile tile = GetTile(x, y);
		tile.type = type;
		worldLoader.UpdateTile(x, y);
	}

	public static Tile GetTile(int x, int y) {
		Vector2Int chunkCoords = WorldToChunkCoords(x, y);
		int cx = chunkCoords.x;
		int cy = chunkCoords.y;
		x %= CHUNK_SIZE;
		y %= CHUNK_SIZE;

		if (x < 0) x += CHUNK_SIZE;
		if (y < 0) y += CHUNK_SIZE;

		return chunks[cx, cy].tiles[x, y];
	}

	public static Chunk GetChunk(int x, int y) {
		Vector2Int chunkCoords = WorldToChunkCoords(x, y);
		int cx = chunkCoords.x;
		int cy = chunkCoords.y;
		return chunks[cx, cy];
	}

	static Vector2Int WorldToChunkCoords(int x, int y) {
		int cx = Mathf.FloorToInt((float)x / CHUNK_SIZE);
		int cy = Mathf.FloorToInt((float)y / CHUNK_SIZE);
		return new Vector2Int(cx, cy);
	}
}

public struct Chunk {
	public Tile[,] tiles;
	public Transform reference;
}

public class Tile {
	public Transform objectReference;

	// 0: Sand / Nothing, 1: Cactus, 2: Machine
	public int type;

	public Tile(int type) {
		this.type = type;
	}
}


public class Dictionary<TKey1, TKey2, TValue> : Dictionary<Tuple<TKey1, TKey2>, TValue>, IDictionary<Tuple<TKey1, TKey2>, TValue> {

	public TValue this[TKey1 key1, TKey2 key2] {
		get { return base[Tuple.Create(key1, key2)]; }
		set { base[Tuple.Create(key1, key2)] = value; }
	}

	public void Add(TKey1 key1, TKey2 key2, TValue value) {
		base.Add(Tuple.Create(key1, key2), value);
	}

	public bool ContainsKey(TKey1 key1, TKey2 key2) {
		return base.ContainsKey(Tuple.Create(key1, key2));
	}
}