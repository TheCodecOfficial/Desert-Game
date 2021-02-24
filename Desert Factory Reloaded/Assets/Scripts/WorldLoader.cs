using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour {

	public Transform world;

	public Transform player;

	public GameObject sandPrefab;
	public GameObject cactusPrefab;

	public int RENDER_DISTANCE = 4;

	private HashSet<Chunk> loadedChunks;

	void Start() {
		WorldData.InitializeWorld();
		loadedChunks = new HashSet<Chunk>();
	}

	private void Update() {
		LoadCycle();
	}

	public void LoadCycle() {
		//HideAllChunks();

		int chunkSize = WorldData.CHUNK_SIZE;

		HashSet<Chunk> chunksToLoad = new HashSet<Chunk>();

		Vector3 playerPosition = player.position;
		int px = Mathf.FloorToInt(playerPosition.x / chunkSize);
		int py = Mathf.FloorToInt(playerPosition.z / chunkSize);

		for (int x = -RENDER_DISTANCE; x < RENDER_DISTANCE + 1; x++) {
			for (int y = -RENDER_DISTANCE; y < RENDER_DISTANCE + 1; y++) {
				int chunkX = px + x;
				int chunkY = py + y;
				// If the chunk does not exist yet, generate it
				if (!WorldData.ContainsChunk(chunkX, chunkY)) {
					bool[,] tiles = WorldGen.GenerateChunk(chunkX, chunkY);
					WorldData.GenerateChunk(chunkX, chunkY, tiles);
					InstantiateChunk(chunkX, chunkY);
				}
				chunksToLoad.Add(WorldData.chunks[chunkX, chunkY]);
			}
		}

		// Only hide the chunks that were loaded but shouldn't be loaded anymore
		loadedChunks.ExceptWith(chunksToLoad);
		HideChunks(loadedChunks);

		// Show all chunks in render distance
		loadedChunks.Clear();
		ShowChunks(chunksToLoad);
	}

	public void ShowChunks(HashSet<Chunk> chunks) {
		foreach (Chunk chunk in chunks) {
			ShowChunk(chunk);
		}
	}

	/*public void ShowChunk(int x, int y) {
		if (WorldData.ContainsChunk(x, y)) ShowChunk(WorldData.chunks[x, y]);
	}*/

	public void ShowChunk(Chunk chunk) {
		if (WorldData.ContainsChunk(chunk) && chunk.reference != null) {
			chunk.reference.gameObject.SetActive(true);
			loadedChunks.Add(chunk);
		}
	}

	public void HideChunks(HashSet<Chunk> chunks) {
		foreach (Chunk chunk in chunks) {
			HideChunk(chunk);
		}
	}

	public void HideChunk(Chunk chunk) {
		//Debug.Log("Contains chunk: " + WorldData.ContainsChunk(chunk) + ", Reference: " + chunk.reference);
		if (WorldData.ContainsChunk(chunk) && chunk.reference != null) {
			chunk.reference.gameObject.SetActive(false);
		}
	}

	public void InstantiateChunk(int x, int y) {
		Chunk chunk = WorldData.chunks[x, y];

		int chunkSize = WorldData.CHUNK_SIZE;

		int chunkX = x * chunkSize;
		int chunkY = y * chunkSize;

		Transform chunkParent = new GameObject("Chunk " + x + ", " + y).transform;
		chunkParent.SetParent(world);
		chunk.reference = chunkParent;
		WorldData.chunks[x, y] = chunk;

		Vector3 position;
		for (int cx = 0; cx < chunkSize; cx++) {
			for (int cy = 0; cy < chunkSize; cy++) {
				position = new Vector3(chunkX + cx, 0, chunkY + cy);
				Quaternion rot = Quaternion.Euler(0, Random.value * 360, 0);
				if (chunk.tiles[cx, cy]) Instantiate(cactusPrefab, position, rot, chunkParent);
			}
		}
		position = new Vector3(chunkX + chunkSize / 2f - 0.5f, 0, chunkY + chunkSize / 2f - 0.5f); // Center sand plane
		Transform sandPlane = Instantiate(sandPrefab, position, Quaternion.identity, chunkParent).transform;
		sandPlane.localScale = new Vector3(chunkSize, 1, chunkSize) * 0.1f; // 0.1 Because Plane is 10 x 10 units by default
		sandPlane.GetComponent<Renderer>().materials[0].mainTextureScale = Vector2.one * chunkSize;
		HideChunk(chunk);
	}

	private void OnDrawGizmos() {

		Gizmos.color = Color.magenta;

		int chunkSize = WorldData.CHUNK_SIZE;

		Vector3 playerPosition = player.position;
		int px = Mathf.FloorToInt(playerPosition.x / chunkSize);
		int py = Mathf.FloorToInt(playerPosition.z / chunkSize);

		for (int x = -RENDER_DISTANCE; x < RENDER_DISTANCE + 1; x++) {
			for (int y = -RENDER_DISTANCE; y < RENDER_DISTANCE + 1; y++) {
				int chunkX = chunkSize * (px + x);
				int chunkY = chunkSize * (py + y);
				Gizmos.DrawWireCube(new Vector3(chunkX + chunkSize / 2f - 0.5f, 0.01f, chunkY + chunkSize / 2f - 0.5f), new Vector3(chunkSize, 0, chunkSize));
			}
		}
	}
}