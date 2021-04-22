using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

namespace DesignPattern
{
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

        private int width;
        private int height;

        private Vector2 cellSize;

        // Start is called before the first frame update
        void Start()
        {
            cellSize = GetComponent<Grid>().cellSize;
            width = Mathf.FloorToInt(Mathf.Abs(endPoint.position.x - startPoint.position.x));
            height = Mathf.FloorToInt(Mathf.Abs(endPoint.position.y - startPoint.position.y));

            CreateBoundary();

            Grid = new GridPathNode(width, height, cellSize, transform.position);

            Grid.ScanTilemap(groundTilemap, new PathNode(value: 1, isWalkable: true));
            Grid.ScanTilemap(blockTilemap, new PathNode(value: 10, isWalkable: false));

            if (GameController.Instance.Debug)
            {
                Grid.DebugGrid(sizeText: 20);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void CreateBoundary()
        {
            Transform bounds = new GameObject("Bounds").transform;
            bounds.parent = transform;

            BoxCollider2D downBound = new GameObject("Down Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            downBound.transform.parent = bounds;
            downBound.size = new Vector2(width * cellSize.x, cellSize.y);
            downBound.offset = new Vector2(downBound.size.x / 2 + transform.position.x, -downBound.size.y / 2 + transform.position.y);

            BoxCollider2D topBound = new GameObject("Top Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            topBound.transform.parent = bounds;
            topBound.size = new Vector2(width * cellSize.x, cellSize.y);
            topBound.offset = new Vector2(topBound.size.x / 2 + transform.position.x, height + topBound.size.y / 2 + transform.position.y);

            BoxCollider2D leftBound = new GameObject("Left Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            leftBound.transform.parent = bounds;
            leftBound.size = new Vector2(cellSize.x, height * cellSize.y);
            leftBound.offset = new Vector2(-leftBound.size.x / 2 + transform.position.x, leftBound.size.y / 2 + transform.position.y);

            BoxCollider2D rightBound = new GameObject("Right Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            rightBound.transform.parent = bounds;
            rightBound.size = new Vector2(cellSize.x, height * cellSize.y);
            rightBound.offset = new Vector2(width + rightBound.size.x / 2 + transform.position.x, rightBound.size.y / 2 + transform.position.y);


        }
    }
}
