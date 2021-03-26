using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TerrainType
{

    public string name;
    public float height;
    public Vector2 coords;

    public TerrainType(string n, float h, Vector2 c)
    {
        name = n;
        height = h;
        coords = c;
    }
}


public class CubeSpawn : MonoBehaviour
{

    private TerrainType[] terrainTypes =
    {
        new TerrainType("grass", 0.5f, new Vector2(3,16)),
        new TerrainType("stone", 0.4f, new Vector2(2, 16)),
        new TerrainType("diamond", 0.3f, new Vector2(3, 13))
            
    };


    public GameObject[,,] cubeArray;
    public Material textureAtlas;
    public void CubeMake(int length, int depth, int height, float[,] heightMap)
    {     
        cubeArray = new GameObject[length, height, depth];
        for (int xIndex = 0; xIndex < length; xIndex++)
        {
            for (int zIndex = 0; zIndex < depth; zIndex++)
            {
                for (int yIndex = 0; yIndex < height; yIndex++)
                {
                    //Debug.Log(Math.Round(heightMap[xIndex, zIndex], 1));
                    // height map is 0-1, k/height is in decimal form whihc will be 0-1. 
                    if (heightMap[xIndex, zIndex]/1.0 > yIndex/(double)height )
                    {
                        
                        GameObject go = new GameObject();
                        cubeArray[xIndex, yIndex, zIndex] = go;
                        //cubeArray[i, j].transform.position = (new Vector3(j, i, 0));
                        CubeGenerator.CreateCube(go, new Vector3(xIndex, yIndex, zIndex), textureAtlas, ChooseTerrainType(heightMap[xIndex, zIndex]));
                        //Debug.Log(ChooseTerrainType(heightMap[xIndex, zIndex]));
                        //cubeArray[i, j].gameObject.transform.SetParent(gameObject.transform);
                    }
                }
            }
        }
    }

    public Vector2 ChooseTerrainType(float height)
    {
        Debug.Log(terrainTypes.Length);
        foreach (TerrainType terrainType in terrainTypes)
        {
            Debug.Log($"{height} < {terrainType.height}");
            if (height > terrainType.height)
            {
                return terrainType.coords;
            }
        }
        return new Vector2(3, 16);
    }
}
