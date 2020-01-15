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
        public ColliderRay[] toCheck = new ColliderRay[0];

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