using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NOISEMAP,
        COLOURMAP,
        MESH,
        PLANEANDMAP
    }
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainTypes[] regions;

    public void GenerateMap()
    {   
        // random generation for variables if wanted
        /*mapWidth = (int)Random.Range(1, 20);
        mapHeight = (int)Random.Range(1, 20);
        noiseScale = Random.Range(0f, 1f);*/


        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);

        // determining which region each part of the map is
        Color[] colourMap = new Color[mapWidth * mapHeight];
        for(int y = 0; y< mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];

                for(int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;

                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if (drawMode == DrawMode.NOISEMAP)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap)); 
        }
        else if (drawMode == DrawMode.COLOURMAP)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.MESH)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.PLANEANDMAP)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }

    }

    private void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(noiseScale < 0)
        {
            noiseScale = 0;
        }
        if(octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainTypes
{
    public string name;
    public float height;
    public Color colour;
}