using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour {

	public Transform world;

	public Transform player;

	public GameObject sandPrefab;
	public GameObject cactusPrefab;

	public int RENDER_DISTANCE = 4;

	void Start() {
		int n = 16;
		for (int x = 0; x < n; x++) {
			for (int y = 0; y < n; y++) {
				if (WorldData.ContainsChunk(x, y)) InstantiateChunk(x, y);
			}
		}
	}

	float time = 0;
	private void Update() {
		time += Time.deltaTime;
		if (time > 0.2f) {
			LoadCycle();
			time = 0;
		}
	}

	public void LoadCycle() {
		HideAllChunks();

		int chunkSize = WorldData.CHUNK_SIZE;

		Vector3 playerPosition = player.position;
		int px = Mathf.FloorToInt(playerPosition.x / chunkSize);
		int py = Mathf.FloorToInt(playerPosition.z / chunkSize);

		for (int x = -RENDER_DISTANCE; x < RENDER_DISTANCE + 1; x++) {
			for (int y = -RENDER_DISTANCE; y < RENDER_DISTANCE + 1; y++) {
				int chunkX = px + x;
				int chunkY = py + y;
				ShowChunk(chunkX, chunkY);
			}
		}
	}

	public void ShowChunk(int x, int y) {
		if (WorldData.ContainsChunk(x, y)) ShowChunk(WorldData.chunks[x, y]);
	}

	public void ShowChunk(Chunk chunk) {
		if (WorldData.ContainsChunk(chunk) && chunk.reference != null) chunk.reference.gameObject.SetActive(true);
	}

	public void HideAllChunks() {
		foreach (Chunk chunk in WorldData.chunks.Values) {
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

		for (int cx = 0; cx < chunkSize; cx++) {
			for (int cy = 0; cy < chunkSize; cy++) {
				Vector3 position = new Vector3(chunkX + cx, 0, chunkY + cy);
				if (chunk.tiles[cx, cy]) Instantiate(cactusPrefab, position, Quaternion.identity, chunkParent);
				Instantiate(sandPrefab, position, Quaternion.identity, chunkParent);
			}
		}
	}
}