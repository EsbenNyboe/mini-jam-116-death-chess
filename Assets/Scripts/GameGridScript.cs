using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameGridScript : MonoBehaviour
{
    public static GameGridScript Instance;

    public int height = 10;
    public int width = 10;
    public float gridSpaceSize = 1f;

    public float randRangeMin = 0f;
    public float randRangeMax = 0f;

    [SerializeField] private GameObject gridCellWhite;
    [SerializeField] private GameObject gridCellBlack;
    private GameObject[,] gameGrid;
    private bool isBlack = false;

    public Dictionary<Vector2Int, GridCellScript> gridCellScripts;

    // fruits.Add("apple","macintosh");

    private void Awake()
    {
        if (Instance)
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    private int selectedHeight;
    private int selectedWidth;

    private void Update()
    {
        // if (Input.GetKey(KeyCode.Return))
        // {
        //     selectedWidth++;
        //     if (selectedWidth > width)
        //     {
        //         selectedHeight++;
        //         selectedWidth = 0;
        //     }
        //     Selection.activeGameObject = gameGrid[selectedHeight, selectedWidth];
        // }
    }

    // Creates the gridd when the game starts
    private void CreateGrid()
    {
        gameGrid = new GameObject[height, width];
        gridCellScripts = new Dictionary<Vector2Int, GridCellScript>();

        // Make the grid
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                isBlack = !isBlack;
                GameObject gridCellPrefab;
                if (isBlack)
                {
                    gridCellPrefab = gridCellBlack;
                }
                else
                {
                    gridCellPrefab = gridCellWhite;
                }

                //Create new GridSpace Object for each cell

                gridSpaceSize = Random.Range(randRangeMin, randRangeMax);

                float yPosition = -gridSpaceSize * 0.5f;
                gameGrid[x, z] = Instantiate(gridCellPrefab, 
                    new Vector3(x * gridSpaceSize, yPosition, z * gridSpaceSize), Quaternion.identity);
                GridCellScript gridCellScript = gameGrid[x, z].GetComponent<GridCellScript>();
                gridCellScript.SetPosition(x, z);
                gameGrid[x, z].transform.parent = transform;
                gameGrid[x, z].gameObject.name = "Grid Space ( X: " + x.ToString() + " , Y: " + z.ToString() + ")";
                gridCellScripts.Add(new Vector2Int(x,z), gridCellScript);
            }
        }
    }

    // Gets the grids position form the world position
    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition)

    {
        int x = Mathf.FloorToInt(worldPosition.x / gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.z / gridSpaceSize);

        x = Mathf.Clamp(x, 0, width);
        y = Mathf.Clamp(x, 0, height);

        return new Vector2Int(x, y);
    }

    // Gets the world posistion of a grid position
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * gridSpaceSize;
        float y = gridPos.y * gridSpaceSize;

        return new Vector3(x, 0, y);
    }

    public GridCellScript GetGridCellScriptFromGridPos(Vector2Int gridPos)
    {
        return gridCellScripts[gridPos];
    }
}