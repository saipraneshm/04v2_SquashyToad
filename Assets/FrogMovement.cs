﻿using UnityEngine;
using System.Collections;

public class FrogMovement : MonoBehaviour {

    public float jumpElevationInDegrees = 45;
    public float[] jumpSpeedInCMPS = { 200, 400, 700 };
    public float jumpGroundClearance = 2;
    public float jumpSpeedTolerance = 5;

    public int collisionCount = 0;
    public int hopCount = 0;

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter()
    {
        collisionCount++;
    }

    void OnCollisionExit()
    {
        collisionCount--;
    }
	
	// Update is called once per frame
	void Update () {
        bool isOnGround = collisionCount > 0;

        if (isOnGround)
        {
            hopCount = 0;
        }

        if (GvrViewer.Instance.Triggered && hopCount < jumpSpeedInCMPS.Length)
		{
            var camera = GetComponentInChildren<Camera>();
            var projectedLookDirection = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
            var radiansToRotate = Mathf.Deg2Rad * jumpElevationInDegrees;
            var unnormalizedJumpDirection = Vector3.RotateTowards(projectedLookDirection, Vector3.up, radiansToRotate, 0);
            var jumpVector = unnormalizedJumpDirection.normalized * jumpSpeedInCMPS[hopCount];
            GetComponent<Rigidbody>().AddForce(jumpVector, ForceMode.VelocityChange);

            hopCount++;
		}
	}
}
