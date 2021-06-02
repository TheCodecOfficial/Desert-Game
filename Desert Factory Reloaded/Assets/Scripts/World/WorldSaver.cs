
using UnityEngine;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;
using System;
public class WorldSaver
{
    static string path = Application.dataPath + "/Saves/";

    public static void ClearSave(){

        string saveName = "WORLDSAVE" + DateTime.UtcNow.ToString("d") + ".bingus";
        File.Delete(path + saveName);
    }
    public static void SaveWorld()
    {
        // Get list of coords of all chunks that have been generated
        List<Tuple<int, int>> chunkCoords = new List<Tuple<int, int>>(WorldData.chunks.Keys);

        Debug.Log("Total chunks: " + chunkCoords.Count);

        // Generate ChunkData objects for each chunk
        ChunkData[] worldChunkData = new ChunkData[chunkCoords.Count];
        for (int i = 0; i < chunkCoords.Count; i++)
        {
            int x = chunkCoords[i].Item1;
            int y = chunkCoords[i].Item2;
            worldChunkData[i] = new ChunkData(x, y, WorldData.chunks[x, y]);
        }

        // Generate wrapper for JSON conversion
        GenericArrayWrapper<ChunkData> worldChunkDataWrapper = new GenericArrayWrapper<ChunkData>(worldChunkData);

        // Generat compressed string from JSON
        byte[] save = CompressString(JsonUtility.ToJson(worldChunkDataWrapper));
        string saveName = "WORLDSAVE" + DateTime.UtcNow.ToString("d") + ".bingus";

        // Save file
        File.WriteAllBytes(path + saveName, save);
    }

    public static ChunkData[] LoadWorld(string path)
    {
        if (!File.Exists(path)) throw new Exception("File not found at path " + path);

        byte[] file = File.ReadAllBytes(path);
        using (var inputStream = new MemoryStream(file))
        using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
        using (var outputStream = new MemoryStream())
        {
            gZipStream.CopyTo(outputStream);
            var outputBytes = outputStream.ToArray();

            string decompressed = System.Text.Encoding.UTF8.GetString(outputBytes);
            GenericArrayWrapper<ChunkData> worldChunkDataWrapper = JsonUtility.FromJson<GenericArrayWrapper<ChunkData>>(decompressed);
            
            return worldChunkDataWrapper.data;
        }
    }

    /*public static void SaveChunk(int x, int y)
    {
        Chunk chunk = WorldData.chunks[x, y];
        ChunkData chunkData = new ChunkData(x, y, chunk);

        string json = JsonUtility.ToJson(chunkData);
        Debug.Log(json);
        byte[] compressed = CompressString(json);

        string fileName = "chunk" + x + "_" + y + ".chuck-e-cheese-horror-story";
        File.WriteAllBytes(path + fileName, compressed);
    }

    public static ChunkData LoadChunk(string path)
    {
        byte[] file = File.ReadAllBytes(path);
        using (var inputStream = new MemoryStream(file))
        using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
        using (var outputStream = new MemoryStream())
        {
            gZipStream.CopyTo(outputStream);
            var outputBytes = outputStream.ToArray();

            string decompressed = System.Text.Encoding.UTF8.GetString(outputBytes);
            Debug.Log(decompressed);
            ChunkData chunkData = JsonUtility.FromJson<ChunkData>(decompressed);
            return chunkData;
        }
    }*/

    public static byte[] CompressString(string input)
    {
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(input);
        using (var outputStream = new MemoryStream())
        {
            using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                gZipStream.Write(jsonBytes, 0, jsonBytes.Length);
            return outputStream.ToArray();
        }
    }
}

[System.Serializable]
public struct GenericArrayWrapper<T>
{
    public T[] data;
    public GenericArrayWrapper(T[] _data) { data = _data; }
}

[System.Serializable]
public class ChunkData
{

    public int x, y;
    public Tile[] data;

    public ChunkData(int _x, int _y, Chunk chunk)
    {

        x = _x;
        y = _y;

        int cs = WorldData.CHUNK_SIZE;
        data = new Tile[cs * cs];
        int index = 0;
        foreach (Tile tile in chunk.tiles)
        {
            data[index] = tile;
            index++;
        }
    }
}