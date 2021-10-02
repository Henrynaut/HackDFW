using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityEngine.XR.ARFoundation.Samples
{
    /// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    [RequireComponent(typeof(ARRaycastManager))]
    public class PopBalloon : MonoBehaviour
    {
       [SerializeField]
        [Tooltip("Instantiates this prefab on a plane at the touch location.")]
        GameObject m_PlacedPrefab;
	List<GameObject> spawnedObjects = new List<GameObject>();
        /// <summary>
        /// The prefab to instantiate on touch.
        /// </summary>
        public GameObject placedPrefab
        {
            get { return m_PlacedPrefab; }
            set { m_PlacedPrefab = value; }
        }

        /// <summary>
        /// The object instantiated as a result of a successful raycast intersection with a plane.
        /// </summary>
        public GameObject spawnedObject { get; private set; }

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        void Start()
        {
            // Spawn balloons every 2 seconds, after a 1 second delay
            InvokeRepeating("SpawnBalloon", 1, 2);
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        void Update()
        {
            // if (!TryGetTouchPosition(out Vector2 touchPosition))
            //     return;

            // // If we touch a balloon, pop it
            // if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            // {
            //     Destroy(s_Hits[0].transform.gameObject); // destroy the object hit
            // }
        }

        void SpawnBalloon()
        {
            var pos = new Vector3(Random.Range(-2,2), 0.5f, Random.Range(-2,2));
            GameObject temp = Instantiate(m_PlacedPrefab, pos, Quaternion.Euler(0, 0, 0));
            spawnedObjects.Add(temp);            
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
