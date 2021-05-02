using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DesignPattern;
using DesignPattern.Factory;
using UnityEngine;

namespace DesignPattern
{
    public class CameraController : MonoBehaviour
    {
        #region SINGELTON
        private CameraController() { }
        public static CameraController Instance { get; private set; }

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

        [SerializeField] GameObject playerCamera;

        private CinemachineConfiner cameraBound;
        private CinemachineVirtualCamera virtualCamera;

        private void OnEnable()
        {
            GameController.PlayerChangedEvent += UpdateCameraFollow;
        }

        private void OnDisable()
        {
            GameController.PlayerChangedEvent -= UpdateCameraFollow;
        }

        private void Start()
        {
            cameraBound = playerCamera.GetComponent<CinemachineConfiner>();
            virtualCamera = playerCamera.GetComponent<CinemachineVirtualCamera>();
        }

        public void UpdateCameraBound(PolygonCollider2D collider)
        {
            cameraBound.m_BoundingShape2D = collider;
            cameraBound.InvalidatePathCache();
        }

        private void UpdateCameraFollow(Player player)
        {
            virtualCamera.Follow = player.transform;
        }
    }
}

