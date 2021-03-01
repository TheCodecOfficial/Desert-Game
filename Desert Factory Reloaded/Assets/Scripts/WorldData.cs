using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData
{
	public const int CHUNK_SIZE = 16;

	public static Dictionary<int, int, Chunk> chunks;
	//public static List<Chunk> chunkList;
	
	public static void InitializeWorld() {
		chunks = new Dictionary<int, int, Chunk>();
		//chunkList = new List<Chunk>();
	}

	public static Chunk GenerateChunk(int x, int y) {
		return GenerateChunk(x, y, new Tile[CHUNK_SIZE, CHUNK_SIZE]);
	}

	public static Chunk GenerateChunk(int x, int y, Tile[,] chunkData) {
		Chunk chunk = new Chunk() { tiles = chunkData };
		chunks[x, y] = chunk;
		//chunkList.Add(chunk);
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
		if (type == 0) {
			// TEMPORARY
			tile.objectReference.gameObject.SetActive(false);
		}
	}

	public static Tile GetTile(int x, int y) {
		int cx = x / CHUNK_SIZE;
		int cy = y / CHUNK_SIZE;
		x %= CHUNK_SIZE;
		y %= CHUNK_SIZE;

		if (x < 0) { x += CHUNK_SIZE; cx--; }
		if (y < 0) { y += CHUNK_SIZE; cy--; }

		return chunks[cx, cy].tiles[x, y];
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