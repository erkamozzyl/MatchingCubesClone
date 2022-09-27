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
            targetPosition.x = 0;
        }
        if (lockY)
        {
            targetPosition.y = 0;
        }
        if (lockZ)
        {
            targetPosition.z = 0;
        }

        return targetPosition;
    }
}
