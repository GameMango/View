using System;
using UnityEngine;
using Zinnia.Cast;
using Zinnia.Pointer;

namespace View.Objects
{
    public class GrabbableObject: MonoBehaviour

    {
        public Rigidbody Rigidbody
        {
            get; private set;
        }

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
    }
}