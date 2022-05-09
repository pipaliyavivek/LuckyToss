using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DebugScript : MonoBehaviour
    {
        public RectTransform m_Target;
        public Camera m_Camera;
        public float MoveSpeed;

        void ABC()
        {
            // Vector2 l_posi = m_Camera.WorldToScreenPoint(m_Target.anchoredPosition);
            // l_posi.z = transform.position.z;
            //transform.position = l_posi;
        }
        void Update()
        {
            /*if (MoveSpeed == 0) return;
            Vector2 StartPosition = transform.position;
            Vector2 DestinationPosition = Camera.main.ScreenToWorldPoint(m_Target.anchoredPosition3D);
            Debug.Log(StartPosition + " StartPos " + DestinationPosition + " Destination Pos ");
            transform.position = Vector2.MoveTowards(StartPosition, DestinationPosition, MoveSpeed);*/
        }
    }
