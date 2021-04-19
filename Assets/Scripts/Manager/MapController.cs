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

    [SerializeField] Tilemap ground;
    [SerializeField] Tilemap blockTile;

    private int width;
    private int height;
    private GridMap<int> gridMap;

    // Start is called before the first frame update
    void Start()
    {
        width = Mathf.FloorToInt(Mathf.Abs(endPoint.position.x - startPoint.position.x));
        height = Mathf.FloorToInt(Mathf.Abs(endPoint.position.y - startPoint.position.y));

        gridMap = new GridMap<int>(width, height, GetComponent<Grid>().cellSize, transform.position);

        gridMap.ScanTilemap(ground, 1);
        gridMap.ScanTilemap(blockTile, 2);

        gridMap.DebugGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
