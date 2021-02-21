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
		return GenerateChunk(x, y, new bool[CHUNK_SIZE, CHUNK_SIZE]);
	}

	public static Chunk GenerateChunk(int x, int y, bool[,] chunkData) {
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

	public static void PrintChunk(Chunk chunk) {
		for (int x = 0; x < CHUNK_SIZE; x++) {
			string s = "";
			for (int y = 0; y < CHUNK_SIZE; y++) {
				s += (chunk.tiles[x, y]) ? 1 : 0;
			}
			Debug.Log(s);
		}
	}
}

public struct Chunk {
	public bool[,] tiles;
	public Transform reference;
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