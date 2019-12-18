using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace View.Perspective
{
    public class PerspectiveCheck : MonoBehaviour
    {
        [Serializable]
        public class ColliderRay
        {
            public Transform target;
            public Collider toHit;
        }

        public Transform cameraTransform;
        public Transform targetCameraTransform;
        public ColliderRay[] toCheck = new ColliderRay[0];

        public float positionCheckTolerance = 1;
        public float directionCheckTolerance = 1;
        public UnityEvent checkSucceeded;

        private bool _checkSucceeded;
        private int _perspectiveLayerMask;

        private void Start()
        {
            _perspectiveLayerMask = LayerMask.GetMask("Perspective");
        }

        private void Update()
        {
            if (_checkSucceeded) return;

            Vector3 camPos = cameraTransform.transform.position;
            foreach (var colliderRay in toCheck)
            {
                Debug.DrawLine(camPos, colliderRay.target.position, Color.red, 0.1f);
            }

//            if (Vector3.Distance(cameraTransform.position, targetCameraTransform.position) >
//                positionCheckTolerance) return;
//            if (Quaternion.Angle(cameraTransform.rotation, targetCameraTransform.rotation) >
//                directionCheckTolerance) return;


            foreach (var colliderRay in toCheck)
            {
                var ray = new Ray(camPos, colliderRay.target.position - camPos);
                if (!Physics.Raycast(ray, out var hit, _perspectiveLayerMask)) return;
                if (hit.collider.GetInstanceID() != colliderRay.toHit.GetInstanceID()) return;
            }

            _checkSucceeded = true;
            checkSucceeded.Invoke();
        }
    }
}