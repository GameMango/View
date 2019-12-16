using System;
using UnityEngine;
using View.Objects;
using VRTK.Prefabs.Interactions.Interactables;
using VRTK.Prefabs.Interactions.Interactors;
using VRTK.Prefabs.Locomotion.Teleporters;
using Zinnia.Action;
using Zinnia.Data.Attribute;
using Zinnia.Data.Type;
using Zinnia.Pointer;

namespace View.Character
{
    public class VRCharacterController : MonoBehaviour
    {

        public bool teleporting = true;
        public TeleporterFacade teleporter;
        public BooleanAction showCurvedAction;
        public float speed = 10;

        public Transform headTracker;
        public float rotateDegree = 45;
        
        private Rigidbody _rigidbody;

        public FloatGrabHandler leftGrabHandler;
        public FloatGrabHandler rightGrabHandler;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir)
        {
            if (!teleporting)
                _rigidbody.AddForce(dir * speed, ForceMode.VelocityChange);
        }

        public void ShowCurvedPointer(bool val)
        {
            if (showCurvedAction == null) return;
            showCurvedAction.Receive(teleporting && val);
        }

        public void Teleport(TransformData dest)
        {
            if (teleporting)
                teleporter.Teleport(dest);
        }

        public void RotateLeft()
        {
            transform.RotateAround(headTracker.position, Vector3.up, -rotateDegree);
        }

        public void RotateRight()
        {
            transform.RotateAround(headTracker.position, Vector3.up, rotateDegree);
        }
        

        public void ForceGrabSelected(ObjectPointer.EventData data)
        {
            if(rightGrabHandler.StandardGrabbing) return;
            
            if (data?.CurrentPointsCastData.HitData != null)
            {
                RaycastHit hit = data.CurrentPointsCastData.HitData.Value;
                if (hit.rigidbody != null)
                {
                    GrabbableObject grabbableObject = hit.rigidbody.GetComponent<GrabbableObject>();
                    if (grabbableObject != null && grabbableObject != rightGrabHandler.GrabbedObject)
                    {
                        rightGrabHandler.GrabObject(grabbableObject);
                        return;
                    }
                }
            }

            if (rightGrabHandler.FloatGrabbing)
                rightGrabHandler.ReleaseObject();
        }

        public void ScaleSelected(ObjectPointer.EventData data)
        {
//            if (Grabbing) return;
//            if (data?.CurrentPointsCastData.HitData != null)
//            {
//                RaycastHit hit = data.CurrentPointsCastData.HitData.Value;
//                if (hit.rigidbody != null)
//                {
//                    GrabbableObject grabbableObject = hit.rigidbody.GetComponent<GrabbableObject>();
//                    if (grabbableObject != null)
//                    {
//                        Vector3 position = headTracker.transform.position + (grabbableObject.transform.position - headTracker.transform.position).normalized * scaleDistance;
//                        grabbableObject.transform.position = position;
//                        
//                        Plane plane = new Plane(headTracker.forward, headTracker.transform.position); 
//                        float dist = plane.GetDistanceToPoint(position); 
//                        Debug.Log(dist);
//                        grabbableObject.transform.localScale *= dist; 
////                        GrabObject(grabbableObject);
//                    }
//                }
//            }
        }

     
    }
}