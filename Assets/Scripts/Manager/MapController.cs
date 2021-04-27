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

        [Header("Points")]
        [SerializeField] Transform startPoint;
        [SerializeField] Transform endPoint;

        [Header("Player Bound")]
        [SerializeField] int distanceToMapBound = 1;
        [SerializeField] Transform playerBounds;
        [SerializeField] PolygonCollider2D cameraBound;

        [Header("Tile maps")]
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
            CreatePlayerBoundary();

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

        private void CreatePlayerBoundary()
        {
            float width = this.width - distanceToMapBound * 2;
            float height = this.height - distanceToMapBound * 2;
            Vector2 offset = Vector2.one * distanceToMapBound;

            BoxCollider2D downBound = new GameObject("Down Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            downBound.transform.parent = playerBounds;
            downBound.gameObject.layer = playerBounds.gameObject.layer;
            downBound.size = new Vector2(width * cellSize.x, cellSize.y);
            downBound.offset = new Vector2(downBound.size.x / 2 + transform.position.x, -downBound.size.y / 2 + transform.position.y) + offset;

            BoxCollider2D topBound = new GameObject("Top Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            topBound.transform.parent = playerBounds;
            topBound.gameObject.layer = playerBounds.gameObject.layer;
            topBound.size = new Vector2(width * cellSize.x, cellSize.y);
            topBound.offset = new Vector2(topBound.size.x / 2 + transform.position.x, height + topBound.size.y / 2 + transform.position.y) + offset;

            BoxCollider2D leftBound = new GameObject("Left Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            leftBound.transform.parent = playerBounds;
            leftBound.gameObject.layer = playerBounds.gameObject.layer;
            leftBound.size = new Vector2(cellSize.x, height * cellSize.y);
            leftBound.offset = new Vector2(-leftBound.size.x / 2 + transform.position.x, leftBound.size.y / 2 + transform.position.y) + offset;

            BoxCollider2D rightBound = new GameObject("Right Bound", typeof(BoxCollider2D)).GetComponent<BoxCollider2D>();
            rightBound.transform.parent = playerBounds;
            rightBound.gameObject.layer = playerBounds.gameObject.layer;
            rightBound.size = new Vector2(cellSize.x, height * cellSize.y);
            rightBound.offset = new Vector2(width + rightBound.size.x / 2 + transform.position.x, rightBound.size.y / 2 + transform.position.y) + offset;

            // Cap nhat camera bounds
            Vector2[] points = new Vector2[4];
            // Top Right
            points[0] = (Vector2)endPoint.position - offset - (Vector2)transform.position;
            // Top Left
            points[1] = new Vector2(startPoint.position.x + offset.x - transform.position.x, endPoint.position.y - offset.y - transform.position.y);
            // Down left
            points[2] = (Vector2)startPoint.position + offset - (Vector2)transform.position;
            // Down Right
            points[3] = new Vector2(endPoint.position.x - offset.x - transform.position.x, startPoint.position.y + offset.y - transform.position.y);
            

            cameraBound.SetPath(0, points);

            CameraController.Instance.UpdateCameraBound(cameraBound);
        }
    }
}
