using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceHoop : MonoBehaviour
{



    private GameObject aRCam;
    [SerializeField]
    [Tooltip("Instantiates this hoop prefab on a plane at the touch location.")]
    GameObject m_HoopPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedHoop
    {
        get { return m_HoopPrefab; }
        set { m_HoopPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedHoop { get; private set; }

    [SerializeField]
    [Tooltip("Instantiates this ball prefab in front of the AR Camera.")]
    GameObject m_BallPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedBall
    {
        get { return m_BallPrefab; }
        set { m_BallPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedBall { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    private bool isPlaced = false;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        aRCam = m_RaycastManager.transform.Find("AR Camera").gameObject;
    }

    void Update()
    {
        if (isPlaced)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    spawnedHoop = Instantiate(m_HoopPrefab, hitPose.position, Quaternion.AngleAxis(-90, Vector3.up));
                    spawnedHoop.transform.LookAt(aRCam.transform);
                    spawnedHoop.transform.rotation =Quaternion.Euler(0, spawnedHoop.transform.rotation.y, 0);
                    spawnedHoop.transform.parent = transform.parent;

                    isPlaced = true;

                    spawnedBall = Instantiate(m_BallPrefab);
                    spawnedBall.transform.parent = aRCam.transform;

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
       
    }
    
}