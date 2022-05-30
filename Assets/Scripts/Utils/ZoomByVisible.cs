using System;
using Cinemachine;
using UnityEngine;

namespace Kitsuma.Utils
{
    public class ZoomByVisible : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float zoomOnVisible = 7f;
        [SerializeField] private float zoomOnNotVisible = 5f;
        [SerializeField] private float zoomSpeed = 5f;

        private Camera _cam;
        private CinemachineVirtualCamera _cinemachine;

        private void Awake()
        {
            _cam = Camera.main;
            _cinemachine = GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if (!GetNeedToAdjustZoom(GetAppropriateZoom())) return;
            ZoomTo(GetAppropriateZoom());
        }

        private void ZoomTo(float zoomLevel)
        {
            if (Math.Abs(GetZoom() - zoomLevel) < 0.1f)
            {
                SetZoom(zoomLevel);
                return;
            }

            SetZoom(Mathf.Lerp(
                GetZoom(),
                zoomLevel,
                Time.deltaTime * zoomSpeed));
        }

        private bool GetNeedToAdjustZoom(float zoomLevel)
        {
            return Math.Abs(GetZoom() - zoomLevel) > 0.01f;
        }

        private float GetAppropriateZoom()
        {
            return IsTargetOffScreen() ? zoomOnNotVisible : zoomOnVisible;
        }

        private float GetZoom()
        {
            return _cinemachine.m_Lens.OrthographicSize;
        }

        private void SetZoom(float zoomLevel)
        {
            _cinemachine.m_Lens.OrthographicSize = zoomLevel;
        }

        private bool IsTargetOffScreen()
        {
            Vector3 pos = _cam.WorldToScreenPoint(target.position);
            return pos.x <= 0 ||
                   pos.x >= Screen.width ||
                   pos.y <= 0 ||
                   pos.y >= Screen.height;
        }
    }
}