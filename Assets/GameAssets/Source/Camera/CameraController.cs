using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : ControllerBaseModel
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform followTarget;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private bool follow;
    [SerializeField] private bool lockX,lockY,lockZ;
    
    public override void Initialize()
    {
        base.Initialize();
    }

    private void LateUpdate()
    {
        if (follow)
        {
            mainCamera.transform.position = GetTargetPosition() + followOffset;
        }
        
    }

    private Vector3 GetTargetPosition()
    {
        var targetPosition = followTarget.transform.position;
        if (lockX)
        {
            targetPosition = new Vector3(0f, targetPosition.y, targetPosition.z);
        }
        if (lockY)
        {
            targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
        }
        if (lockZ)
        {
            targetPosition = new Vector3(targetPosition.x, targetPosition.y,0f);
        }

        return targetPosition;
    }
}
