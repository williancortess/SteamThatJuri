﻿using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public class MapImporter : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;    

    public GameObject Block;
    public GameObject Danger;

    private int[,] tiles;
    public Transform _player;

    private Map0 map0;

    void Awake()
    {
       
    }

    void Start(){
        map0 = GetComponent<Map0>();
        
        tiles = Load3();
        BuildMap();
    }

    void BuildMap()
    {
        Debug.Log("Building Map...");
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {

                if (tiles[i, j] == 2)
                {
                    GameObject TilePrefab = TilePrefab = Instantiate(Block, new Vector3(j - mapWidth, mapHeight - i, 0), Quaternion.identity) as GameObject;
                    TilePrefab.transform.parent = GameObject.FindGameObjectWithTag("Room").transform;

                }
                else
                if (tiles[i, j] == 4)
                {
                    GameObject TilePrefab = TilePrefab = Instantiate(Danger, new Vector3(j - mapWidth, mapHeight - i, 1), Quaternion.identity) as GameObject;
                    TilePrefab.transform.parent = GameObject.FindGameObjectWithTag("Room").transform;
                }
            }
        }        
        Debug.Log("Building Completed!");
        //_player.transform.position = new Vector3(-46, 46, 0);
    }

    private int[,] Load3()
    {
        try
        {
            Debug.Log("Loading File...");
            string input = map0.tiled;
            string[] lines = input.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                int[,] tiles = new int[lines.Length, mapWidth];
                Debug.Log("Parsing...");
                for (int i = 0; i < lines.Length; i++)
                {
                    string st = lines[i];
                    string[] nums = st.Split(new[] { ',' });
                    if (nums.Length != mapWidth)
                    {

                    }
                    for (int j = 0; j < Mathf.Min(nums.Length, mapWidth); j++)
                    {
                        int val;
                        if (int.TryParse(nums[j], out val))
                        {
                            tiles[i, j] = val;
                        }
                        else
                        {
                            tiles[i, j] = 1;
                        }
                    }
                }
                Debug.Log("Parsing Completed!");
                return tiles;
        }
        catch (IOException e)
        {
            Debug.Log(e.Message);
        }
        return null;
    }

}
