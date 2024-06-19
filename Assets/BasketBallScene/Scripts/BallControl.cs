using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{

    public static Vector3 startingPos;
    public float spawnTime = 5f;
    public float throwForce = 100f;


    public float throwDirectionX = 0.1f;
    public float throwDirectionY = 0.5f;

    public Vector3 ballOffset = new Vector3(0, -0.268f, 0.66f);

    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    private bool directionChoosen = false;
    private bool throwStarted = false;

    [SerializeField]
    private GameObject ARCam;

    [SerializeField]
    private ARSessionOrigin sessionOrigin;

    Rigidbody rb;


    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        sessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = sessionOrigin.transform.Find("AR Camera").gameObject;
        transform.parent = ARCam.transform;

        ResetBall();

    }
    private void Update()
{
    if (Input.GetMouseButtonDown(0))
    {
        startPosition = Input.mousePosition;
        startingPos = startPosition;
        startTime = Time.time;
        throwStarted = true;
        directionChoosen = false;
    }
    else if (Input.GetMouseButtonUp(0))
    {
        endTime = Time.time;
        duration = endTime - startTime;
        direction = Input.mousePosition - startPosition;
        directionChoosen = true;
    }

    if (directionChoosen)
    {
        rb.mass = 1;
        rb.useGravity = true;

        // Calculate throw force based on duration and adjust for realistic projectile motion
        float throwSpeed = throwForce / duration;
        Vector3 throwVector = ARCam.transform.forward * throwSpeed +
                              ARCam.transform.up * direction.y * throwDirectionY +
                              ARCam.transform.right * direction.x * throwDirectionX;

        // Apply calculated force to the basketball
        rb.AddForce(throwVector, ForceMode.Impulse);

        throwStarted = false;
        directionChoosen = false;
        startTime = 0;
        duration = 0;
        startPosition = Vector3.zero;
        direction = Vector3.zero;
    }

    if (Time.time - endTime >= spawnTime && Time.time - endTime <= spawnTime + 1)
    {
        ResetBall();
    }
}


    private void ResetBall()
    {
        rb.mass = 0;
        rb.useGravity= false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        endTime= 0;

        Vector3 ballPos=ARCam.transform.position+ARCam.transform.forward*ballOffset.z+ARCam.transform.up*ballOffset.y;
        transform.position = ballPos;

    }
}
