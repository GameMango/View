using System;
using UnityEngine;

namespace View.Perspective
{
    public class ConstantSize : MonoBehaviour
    {
        public Transform checkAgainst;

        private Vector3 _startScale;
        private Vector3 _checkAgainstStartScale;
        private Vector3 _temp = new Vector3();

        private void Awake()
        {
            _checkAgainstStartScale = checkAgainst.localScale;
            _startScale = transform.localScale;
        }

        private void Update()
        {
            Vector3 currScale = checkAgainst.localScale;
            _temp.Set(
                _startScale.x / (currScale.x / _checkAgainstStartScale.x),
                _startScale.y / (currScale.y / _checkAgainstStartScale.y),
                _startScale.z / (currScale.z / _checkAgainstStartScale.z));
            transform.localScale = _temp;
        }
    }
}