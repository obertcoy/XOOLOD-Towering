using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float defaultDistance = 6f;
    [SerializeField] [Range(0f, 10f)] private float minimumDistance = 1f;
    [SerializeField] [Range(0f, 10f)] private float maximumDistance = 6f;

    [SerializeField] [Range(0f, 10f)] private float smoothing = 4f;
    [SerializeField] [Range(0f, 10f)] private float zoomSens = 2f;

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currTargetDistance;

    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        currTargetDistance = defaultDistance;
    }
    private void Update()
    {
        Zoom();

    }

    private void Zoom()
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSens;
        currTargetDistance = Mathf.Clamp(currTargetDistance + zoomValue, minimumDistance, maximumDistance);
        float currDistance = framingTransposer.m_CameraDistance;

        if (currDistance == currTargetDistance)
        {
            return;
        }
        float lerpedZoomValue = Mathf.Lerp(currDistance, currTargetDistance, smoothing * Time.deltaTime);
        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }
}
