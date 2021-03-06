using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLoader : MonoBehaviour
{
    public GameObject sandPrefab;
    public GameObject cactusPrefab;
    public int RENDER_DISTANCE = 4;
    private HashSet<Chunk> loadedChunks;

    private int px, py;

    void Start()
    {
        WorldData.InitializeWorld(this);
        loadedChunks = new HashSet<Chunk>();
        LoadCycle();
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerInfo.getPosition();
        int newpx = Mathf.FloorToInt(playerPosition.x / WorldData.CHUNK_SIZE);
        int newpy = Mathf.FloorToInt(playerPosition.z / WorldData.CHUNK_SIZE);

        if (newpx != px || newpy != py) LoadCycle();
        px = newpx;
        py = newpy;
    }

    public void LoadCycle()
    {
        int chunkSize = WorldData.CHUNK_SIZE;

        HashSet<Chunk> chunksToLoad = new HashSet<Chunk>();

        Vector3 playerPosition = PlayerInfo.getPosition();
        int px = Mathf.FloorToInt(playerPosition.x / chunkSize);
        int py = Mathf.FloorToInt(playerPosition.z / chunkSize);

        for (int x = -RENDER_DISTANCE; x < RENDER_DISTANCE + 1; x++)
        {
            for (int y = -RENDER_DISTANCE; y < RENDER_DISTANCE + 1; y++)
            {
                int chunkX = px + x;
                int chunkY = py + y;
                // If the chunk does not exist yet, generate it
                if (!WorldData.ContainsChunk(chunkX, chunkY))
                {
                    Tile[,] tiles = WorldGen.GenerateChunk(chunkX, chunkY);
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

    public void ShowChunks(HashSet<Chunk> chunks)
    {
        foreach (Chunk chunk in chunks)
        {
            ShowChunk(chunk);
        }
    }

    public void ShowChunk(Chunk chunk)
    {
        if (WorldData.ContainsChunk(chunk) && chunk.reference != null)
        {
            chunk.reference.gameObject.SetActive(true);
            loadedChunks.Add(chunk);
        }
    }

    public void HideChunks(HashSet<Chunk> chunks)
    {
        foreach (Chunk chunk in chunks)
        {
            HideChunk(chunk);
        }
    }

    public void HideChunk(Chunk chunk)
    {
        //Debug.Log("Contains chunk: " + WorldData.ContainsChunk(chunk) + ", Reference: " + chunk.reference);
        if (WorldData.ContainsChunk(chunk) && chunk.reference != null)
        {
            chunk.reference.gameObject.SetActive(false);
        }
    }

    public void InstantiateChunk(int x, int y)
    {
        Chunk chunk = WorldData.chunks[x, y];

        int chunkSize = WorldData.CHUNK_SIZE;

        int chunkX = x * chunkSize;
        int chunkY = y * chunkSize;

        Transform chunkParent = new GameObject("Chunk " + x + ", " + y).transform;
        chunkParent.SetParent(transform);
        chunk.reference = chunkParent;
        WorldData.chunks[x, y] = chunk;

        Vector3 position;
        for (int cx = 0; cx < chunkSize; cx++)
        {
            for (int cy = 0; cy < chunkSize; cy++)
            {
                position = new Vector3(chunkX + cx, 0, chunkY + cy);
                Quaternion rot = Quaternion.Euler(0, Random.value * 360, 0);
                if (chunk.tiles[cx, cy].type == 1)
                {
                    Transform objectReference = Instantiate(cactusPrefab, position, rot, chunkParent).transform;
                    chunk.tiles[cx, cy].objectReference = objectReference;
                }
            }
        }
        position = new Vector3(chunkX + chunkSize / 2f - 0.5f, 0, chunkY + chunkSize / 2f - 0.5f); // Center sand plane
        Transform sandPlane = Instantiate(sandPrefab, position, Quaternion.identity, chunkParent).transform;
        sandPlane.localScale = new Vector3(chunkSize, 1, chunkSize) * 0.1f; // 0.1 Because Plane is 10 x 10 units by default
        sandPlane.GetComponent<Renderer>().materials[0].mainTextureScale = Vector2.one * chunkSize;
        HideChunk(chunk);
    }

    public void UpdateTile(int x, int y)
    {
        Tile tile = WorldData.GetTile(x, y);
        Chunk chunk = WorldData.GetChunk(x, y);
        if (tile.objectReference != null) Destroy(tile.objectReference.gameObject);
        Vector3 position = new Vector3(x, 0, y);
        if (tile.type == 0)
        {
            // Tile should be empty / sand
            // Nothing moment
        }
        else if (tile.type == 1)
        {
            // Tile should be cactus
            Transform objectReference = Instantiate(cactusPrefab, position, Quaternion.identity, chunk.reference).transform;
            tile.objectReference = objectReference;
        }
        else
        {
            // Tile should be machine
            MachineTile machineTile = (MachineTile)tile;

            GameObject machineObject = Instantiate(machineTile.machine.machinePrefab, position, Quaternion.identity, chunk.reference);
            machineTile.objectReference = machineObject.transform;

            MachineController machineController = machineObject.AddComponent<MachineController>();
            machineController.recipes = machineTile.machine.recipes;
            machineController.Setup(x, y, 0);

            machineTile.machineController = machineController;
        }
    }

    /*private void OnDrawGizmos() {

		Gizmos.color = Color.magenta;

		int chunkSize = WorldData.CHUNK_SIZE;

		Vector3 playerPosition = PlayerInfo.getPosition();
		int px = Mathf.FloorToInt(playerPosition.x / chunkSize);n
		int py = Mathf.FloorToInt(playerPosition.z / chunkSize);

		for (int x = -RENDER_DISTANCE; x < RENDER_DISTANCE + 1; x++) {
			for (int y = -RENDER_DISTANCE; y < RENDER_DISTANCE + 1; y++) {
				int chunkX = chunkSize * (px + x);
				int chunkY = chunkSize * (py + y);
				Gizmos.DrawWireCube(new Vector3(chunkX + chunkSize / 2f - 0.5f, 0.01f, chunkY + chunkSize / 2f - 0.5f), new Vector3(chunkSize, 0, chunkSize));
			}
		}
	}*/
}