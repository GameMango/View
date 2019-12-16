using System;
using UnityEngine;

namespace View.Character
{
    public class FollowLocalPosition: MonoBehaviour
    {
        public Transform follow;
        private Vector3 _cached = new Vector3();
        private void Update()
        {
            var followLocal = follow.localPosition;
            var trans = transform;
            _cached.Set(followLocal.x, trans.localPosition.y, followLocal.z);
            trans.localPosition = _cached;
        }
    }
}