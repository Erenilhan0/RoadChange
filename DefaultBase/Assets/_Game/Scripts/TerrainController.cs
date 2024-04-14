using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TerrainController : MonoBehaviour
{
    [SerializeField] private Terrain Terrain;

    [SerializeField] private Transform TargetTransform;

    public int TerrainWidth;

    public int TerrainHeight;
    private TerrainData terrainData; //all the fun stuff the Unity terrain provides


    public int PosXInTerrain; // position of the game object in terrain width (x axis)
    public int PosYInTerrain; // position of the game object in terrain height (z axis)

    public int size = 1; // the diameter of terrain portion that will raise under the game object
    public float desiredHeight = 1; // the height we want that portion of terrain to be

    public LayerMask groundLM;


    public List<float[,]> undoHeights = new List<float[,]>();
    public int offset;

    public float heightIncreaseMultiplier;
    void Start()
    {
        terrainData = Terrain.terrainData;

        TerrainWidth = terrainData.heightmapResolution;
        TerrainHeight = terrainData.heightmapResolution;
    }

    private void Update()
    {
        RaycastHit hit;
        var direction = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(direction, out hit, 50, groundLM))
        {
            TargetTransform.position = hit.point;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            RaiseTerrain();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            UndoModifyTerrainHeightmap();
        }
    }


    private void RaiseTerrain()
    {
        var currentHeights = terrainData.GetHeights(0, 0, TerrainWidth, TerrainHeight);

        undoHeights.Add(currentHeights);
        


        var tempCoord = (TargetTransform.position - Terrain.gameObject.transform.position);
        Vector3 coord;
        
        coord.x = tempCoord.x / terrainData.size.x;
        coord.z = tempCoord.z / terrainData.size.z;
        
        // get the position of the terrain heightmap where this game object is
        PosXInTerrain = (int)(coord.x * TerrainWidth);
        PosYInTerrain = (int)(coord.z * TerrainHeight);
        
        // we set an offset so that all the raising terrain is under this game object
        
        // get the heights of the terrain under this game object
        var heights = terrainData.GetHeights(PosYInTerrain - offset, PosXInTerrain - offset, size, size);

          
        var currentHeight = Terrain.SampleHeight(TargetTransform.position);
        Debug.Log(currentHeight);
        
        // if (currentHeight > desiredHeight)
        // {
        //     desiredHeight += heightIncreaseMultiplier;
        // }
        // else
        // {
        //     desiredHeight = 0.5f;
        // }
        
        
        // we set each sample of the terrain in the size to the desired height
        var heightScale = 1.0f / terrainData.size.y;
        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                heights[i, j] = desiredHeight * heightScale;
            }
        }
        
        
        // set the new height
        terrainData.SetHeights(PosXInTerrain - offset, PosYInTerrain - offset, heights);
        
      

    }

    private void UndoModifyTerrainHeightmap()
    {
        if (undoHeights.Count == 0) return;

        // get last heights
        float[,] newHeights = undoHeights[^1];

        // apply to terrain
        Terrain.terrainData.SetHeights(0, 0, newHeights);

        // remove from list
        undoHeights.RemoveAt(undoHeights.Count - 1);

    }
}