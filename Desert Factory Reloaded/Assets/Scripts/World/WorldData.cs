using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldData
{
    public const int CHUNK_SIZE = 16;

    static WorldLoader worldLoader;

    public static Dictionary<int, int, Chunk> chunks;

    public static void InitializeWorld(WorldLoader _worldLoader)
    {
        chunks = new Dictionary<int, int, Chunk>();
        worldLoader = _worldLoader;

        // Load saved chunks
        if (!System.IO.File.Exists(Application.dataPath + "/Saves/WORLDSAVE02.06.2021.bingus")) return;
        ChunkData[] worldChunkData = WorldSaver.LoadWorld(Application.dataPath + "/Saves/WORLDSAVE02.06.2021.bingus");
        foreach (ChunkData chunkData in worldChunkData){
            int x = chunkData.x;
            int y = chunkData.y;
            chunks[x, y] = GenerateChunkFromData(chunkData);
            worldLoader.InstantiateChunk(x, y);
        }
    }

    #region Generate Chunks
    public static Chunk GenerateChunk(int x, int y)
    {
        return GenerateChunk(x, y, new Tile[CHUNK_SIZE, CHUNK_SIZE]);
    }

    public static Chunk GenerateChunkFromData(ChunkData chunkData){
        Tile[,] tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];
        int index = 0;
        foreach (Tile tile in chunkData.data)
        {
            tiles[index / CHUNK_SIZE, index % CHUNK_SIZE] = tile;
            index++;
        }
        return GenerateChunk(chunkData.x, chunkData.y, tiles);
    }

    public static Chunk GenerateChunk(int x, int y, Tile[,] chunkData)
    {
        Chunk chunk = new Chunk() { tiles = chunkData };
        chunks.Add(x, y, chunk);
        return chunk;
    }
    #endregion
    
    #region Contains
    public static bool ContainsChunk(int x, int y)
    {
        return chunks.ContainsKey(x, y);
    }

    public static bool ContainsChunk(Chunk chunk)
    {
        return chunks.ContainsValue(chunk);
    }

    public static bool ContainsTile(int x, int y)
    {
        Vector2Int cc = WorldToChunkCoords(x, y);
        if (!ContainsChunk(cc.x, cc.y)) return false;
        else return (GetTile(x, y) != null);
    }
    #endregion

    public static void UpdateTile(int x, int y, int type)
    {
        Tile tile = GetTile(x, y);
        tile.type = type;
        worldLoader.UpdateTile(x, y);
    }

    public static void UpdateTile(int x, int y, Machine machine){
        Tile tile = GetTile(x, y);
        tile.type = 2; // Machine
        worldLoader.UpdateTile(x, y);
    }

    public static Tile GetTile(int x, int y)
    {
        Vector2Int chunkCoords = WorldToChunkCoords(x, y);
        int cx = chunkCoords.x;
        int cy = chunkCoords.y;
        x %= CHUNK_SIZE;
        y %= CHUNK_SIZE;

        if (x < 0) x += CHUNK_SIZE;
        if (y < 0) y += CHUNK_SIZE;

        return chunks[cx, cy].tiles[x, y];
    }

    public static Chunk GetChunk(int x, int y)
    {
        Vector2Int chunkCoords = WorldToChunkCoords(x, y);
        int cx = chunkCoords.x;
        int cy = chunkCoords.y;
        return chunks[cx, cy];
    }

    static Vector2Int WorldToChunkCoords(int x, int y)
    {
        int cx = Mathf.FloorToInt((float)x / CHUNK_SIZE);
        int cy = Mathf.FloorToInt((float)y / CHUNK_SIZE);
        return new Vector2Int(cx, cy);
    }
}

# region Objects/Structs
public struct Chunk
{
    public Tile[,] tiles;
    public Transform reference;

    public void Print(){
        string s = "";
        for (int x = 0; x < tiles.GetLength(0); x++){
            for (int y = 0; y < tiles.GetLength(1); y++){
                s += tiles[x, y].type;
            }
            s += "\n";
        }
        Debug.Log(s);
    }
}

[System.Serializable]
public class Tile
{
    public Transform objectReference;

    // 0: Sand / Nothing, 1: Cactus, 2: Machine
    public int type;
}

class MachineTile : Tile {
    public Machine machine;

    public MachineTile(){
        base.type = 2;
    }
}


public class Dictionary<TKey1, TKey2, TValue> : Dictionary<Tuple<TKey1, TKey2>, TValue>, IDictionary<Tuple<TKey1, TKey2>, TValue>
{

    public TValue this[TKey1 key1, TKey2 key2]
    {
        get { return base[Tuple.Create(key1, key2)]; }
        set { base[Tuple.Create(key1, key2)] = value; }
    }

    public void Add(TKey1 key1, TKey2 key2, TValue value)
    {
        base.Add(Tuple.Create(key1, key2), value);
    }

    public bool ContainsKey(TKey1 key1, TKey2 key2)
    {
        return base.ContainsKey(Tuple.Create(key1, key2));
    }
}
#endregion