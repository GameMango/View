using System;
using UnityEngine;
using UnityEngine.Events;

namespace View.Perspective
{
    public class PerspectiveCheck : MonoBehaviour
    {
        [Serializable]
        public struct ToCheckTransform
        {
            public Transform checkAgainst;
            public Transform toCheck;
        }

        public ToCheckTransform[] checks;
        public float positionCheckTolerance = 1;
        public float directionCheckTolerance = 1;
        public UnityEvent onCheckSucceeded;

        private bool _checkSucceeded;

        private void Update()
        {
            if (_checkSucceeded) return;
            foreach (var checkTransforms in checks)
            {
                if (Vector3.Distance(checkTransforms.checkAgainst.position, checkTransforms.toCheck.position) >
                    positionCheckTolerance) return;
                if (Vector3.Distance(checkTransforms.checkAgainst.forward, checkTransforms.toCheck.forward) >
                    directionCheckTolerance) return;
            }

            _checkSucceeded = true;
            onCheckSucceeded.Invoke();
        }
    }
}