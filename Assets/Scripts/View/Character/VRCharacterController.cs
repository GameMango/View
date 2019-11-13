using System;
using UnityEngine;
using View.Character.Tool;

namespace View.Character
{
    public class VRCharacterController: MonoBehaviour
    {
        public ToolController tool;
        public OVRInput.Controller primaryController;
        public OVRInput.Controller secondaryController;
        public Camera centerEye;
        public Transform trackingSpace;
        private void FixedUpdate()
        {
           
        }
    }
}