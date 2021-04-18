using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField] Tilemap ground;

    private Vector2 startPoint;
    private Vector2 endPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.Find("StartPoint").position;
        endPoint = transform.Find("EndPoint").position;
        Vector2 size = endPoint - startPoint;
        Debug.Log(size);
        CreateGridMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGridMap()
    {
    }
}
