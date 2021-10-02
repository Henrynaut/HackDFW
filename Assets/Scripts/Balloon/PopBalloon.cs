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

        // Reference to the AR Camera
        public Camera AR_Camera;

        // Reference to the Audio source
        public AudioSource PopSound;

        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        void Start()
        {
            // Spawn balloons every 1 second, after a 0 second delay
            InvokeRepeating("SpawnBalloon", 0, 1);
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
            // If we touch a balloon, pop it
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            Ray ray = AR_Camera.ScreenPointToRay(touchPosition);
            RaycastHit hitObject;

            if(Physics.Raycast(ray, out hitObject))
            {
                GameObject DetectedObject = hitObject.transform.gameObject;

                // If the game object exists and has the tag balloon, destroy it
                if(DetectedObject != null && DetectedObject.tag == "balloon")
                {
                    Destroy(DetectedObject);

                    // Play Pop Sound, with the clip attached at 100% volume
                    PopSound.PlayOneShot(PopSound.clip, 1.0f);
                }
            }
        }

        void SpawnBalloon() 
        {

            // Set up distance and angle to spawn balloons away from AR camera, in a 180 degree cone
            float minDistance = 1.0f; // 1 meter
            float maxDistance = 3.0f; // 3 meters
            float distance    = Random.Range( minDistance, maxDistance );
            float angle       = Random.Range( 0, Mathf.PI);
            
            // Get AR camera position and create a balloon spawn position based on distance and angle
            Vector3 spawnPosition = AR_Camera.transform.position ;
            spawnPosition += new Vector3( Mathf.Cos( angle ), Random.Range(0.0f,0.5f), Mathf.Sin( angle ) ) * distance;

            // Old way to spawn at random positions
            // var pos = new Vector3(Random.Range(-2.0f,2.0f), Random.Range(0.0f,0.5f), Random.Range(-2.0f,2.0f));
            GameObject temp = Instantiate(m_PlacedPrefab, spawnPosition, Quaternion.Euler(0, 0, 0));
            spawnedObjects.Add(temp);            
        }

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARRaycastManager m_RaycastManager;
    }
}
