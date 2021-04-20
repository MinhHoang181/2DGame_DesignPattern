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
    private GridPathNode grid;

    // Start is called before the first frame update
    void Start()
    {
        width = Mathf.FloorToInt(Mathf.Abs(endPoint.position.x - startPoint.position.x));
        height = Mathf.FloorToInt(Mathf.Abs(endPoint.position.y - startPoint.position.y));

        grid = new GridPathNode(width, height, GetComponent<Grid>().cellSize, transform.position);

        grid.ScanTilemap(ground, new PathNode(value: 1, isWalkable: true));
        grid.ScanTilemap(blockTile, new PathNode(value: 10, isWalkable: false));

        grid.DebugGrid(sizeText: 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
