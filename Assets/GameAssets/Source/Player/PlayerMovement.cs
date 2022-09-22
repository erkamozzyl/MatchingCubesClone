using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ObjectModel
{
    private bool canMove;
    [SerializeField] private float sensitivity, speed, roadOffset;
    private Vector3 firstPos, lastPos, deltaPos, destPos;

    public void Move()
    {
        canMove = true;
    }

    public void Stop()
    {
        canMove = false;
    }

    private void Update()
    {
        if (canMove)
            TransformMovement(transform);
    }
    private void TransformMovement(Transform playerTransform)
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            deltaPos = Input.mousePosition - firstPos;
            deltaPos.y = 0;
            destPos = playerTransform.transform.position + deltaPos * (sensitivity * Time.deltaTime);
            destPos.x = Mathf.Clamp(destPos.x, -roadOffset, roadOffset);
            firstPos = Input.mousePosition;
        }
        destPos.z = playerTransform.position.z + speed * Time.deltaTime;
        playerTransform.position = new Vector3(destPos.x, playerTransform.position.y, destPos.z);
    }
}
