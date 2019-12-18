using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Reset : MonoBehaviour
    {
        public Transform reset;

        private void OnCollisionEnter(Collision other)
        {
            other.gameObject.transform.position = reset.position;
            
        }
    }
}