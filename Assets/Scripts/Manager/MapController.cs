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

    public GridPathNode Grid { get; private set; }

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;

    [SerializeField] Tilemap groundTilemap;
    [SerializeField] Tilemap blockTilemap;

    [SerializeField] bool debug = false;

    private int width;
    private int height;

    // Start is called before the first frame update
    void Start()
    {
        width = Mathf.FloorToInt(Mathf.Abs(endPoint.position.x - startPoint.position.x));
        height = Mathf.FloorToInt(Mathf.Abs(endPoint.position.y - startPoint.position.y));

        Grid = new GridPathNode(width, height, GetComponent<Grid>().cellSize, transform.position);

        Grid.ScanTilemap(groundTilemap, new PathNode(value: 1, isWalkable: true));
        Grid.ScanTilemap(blockTilemap, new PathNode(value: 10, isWalkable: false));

        if (debug)
        {
            Grid.DebugGrid(sizeText: 20);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
