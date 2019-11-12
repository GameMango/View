using UnityEngine;

namespace View.Character.Tool
{
    public class FollowSmooth : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float forceMult = 0.1f;
        private bool _isTargetNull;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _isTargetNull = target == null;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_isTargetNull) return;
            _rigidbody.AddForce((target.position + offset - transform.position) * forceMult);
        }
    }
}
