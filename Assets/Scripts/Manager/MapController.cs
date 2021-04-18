using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    #region SINGELTON
    private MapController() { }
    public static MapController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] Tilemap blockTile;

    private int width;
    private int height;
    private GridMap<bool> gridMap;

    // Start is called before the first frame update
    void Start()
    {
        width = Mathf.FloorToInt(Mathf.Abs(endPoint.position.x - startPoint.position.x));
        height = Mathf.FloorToInt(Mathf.Abs(endPoint.position.y - startPoint.position.y));

        gridMap = new GridMap<bool>(width, height, GetComponent<Grid>().cellSize, transform.position);

        ScanGridMap();
    }

    // Update is called once per frame
    void Update()
    {
        gridMap.DebugGrid();
    }

    public void ScanGridMap()
    {
        BoundsInt bounds = blockTile.cellBounds;
        TileBase[] tileArray = blockTile.GetTilesBlock(bounds);
        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = tileArray[x + y * bounds.size.x];
                if (tile != null)
                {
                    //Debug.Log("x: " + x + ", y: " + y + " = " + tile.name);
                    gridMap.SetValue(x, y, true);
                }
            }
        }
    }
}
