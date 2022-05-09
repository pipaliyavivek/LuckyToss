using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
 
    public class Projectile : MonoBehaviour
    {
        public GameObject m_RingPrefab;
        public GameObject cursor;
        public Transform shootPoint;
        public LayerMask layer;
        public LineRenderer lineVisual;
        public int lineSegment = 10;
        public float flightTime = 1f;
        private bool Isshoot;
        private bool canclick;
        private Camera cam;
        [HideInInspector] public float Score;
        [SerializeField, ReadOnly] public int ReamaingTube = 5;

        public static Projectile instance;
        [HideInInspector] public GameObject m_Ring;

        [SerializeField] Text m_text;

        void Awake() => instance = this;

        void Start()
        {
            canclick = true;
            Isshoot = true;
            lineVisual.enabled = false;
            cam = Camera.main;
            lineVisual.positionCount = lineSegment + 1;
        }
        void Update()
        {
            ReamaingTube = GameManager.instance.ReamaingTube;
            m_text.text = "Score:" + Score.ToString();
            if (Isshoot)
            {
                LaunchProjectile();
            }
        }
        void LaunchProjectile()
        {
            Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(camRay, out hit, 100f, layer))
            {
                if (ReamaingTube < 1) return;
                if (Input.GetMouseButton(0))
                {
                    if (!canclick) return;
                    cursor.SetActive(true);
                    lineVisual.enabled = true;
                    cursor.transform.position = hit.point + Vector3.up * 0.05f;
                    Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);
                    Visualize(vo, cursor.transform.position);
                    //transform.rotation = Quaternion.LookRotation(vo);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    ReamaingTube--;
                    cursor.SetActive(false);
                    lineVisual.enabled = false;
                    if (!canclick) return;
                    Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);
                    m_Ring = Instantiate(m_RingPrefab, shootPoint.position, Quaternion.identity);
                    m_Ring.GetComponent<Rigidbody>().velocity = vo;
                    canclick = true;
                }
            }
        }
        public void Visualize(Vector3 i_vo, Vector3 i_finalPos)
        {
            for (int i = 0; i < lineSegment; i++)
            {
                Vector3 pos = CalculatePosInTime(i_vo, (i / (float)lineSegment) * flightTime);
                lineVisual.SetPosition(i, pos);
            }
            lineVisual.SetPosition(lineSegment, i_finalPos);
        }
        Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXz = distance;
            distanceXz.y = 0f;

            float sY = distance.y;
            float sXz = distanceXz.magnitude;

            float Vxz = sXz / time;
            float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            Vector3 result = distanceXz.normalized;
            result *= Vxz;
            result.y = Vy;
            return result;
        }
        Vector3 CalculatePosInTime(Vector3 i_vo, float i_time)
        {
            Vector3 Vxz = i_vo;
            Vxz.y = 0f;

            Vector3 result = shootPoint.position + i_vo * i_time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (i_time * i_time)) + (i_vo.y * i_time) + shootPoint.position.y;
            result.y = sY;
            return result;
        }
        public void getdata()
        {
            Debug.Log("PPPPP");
        }
    }
