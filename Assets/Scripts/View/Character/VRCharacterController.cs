using System;
using UnityEngine;
using UnityEngine.Rendering;
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

        public float scaleStartDistance = 2f;
        public float scaleJumpLerp = 0.8f;

        public FloatGrabHandler leftGrabHandler;
        public FloatGrabHandler rightGrabHandler;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir)
        {
            if (leftGrabHandler.FloatGrabbing) return;
            if (!teleporting)
                _rigidbody.AddForce(dir * speed, ForceMode.VelocityChange);
        }

        public void ShowCurvedPointer(bool val)
        {
            if (leftGrabHandler.FloatGrabbing) return;
            if (showCurvedAction == null) return;
            showCurvedAction.Receive(teleporting && val);
        }

        public void Teleport(TransformData dest)
        {
            if (leftGrabHandler.FloatGrabbing) return;
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
            if (rightGrabHandler.StandardGrabbing || leftGrabHandler.FloatGrabbing) return;

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
            if (leftGrabHandler.StandardGrabbing || rightGrabHandler.FloatGrabbing) return;

            if (data?.CurrentPointsCastData.HitData != null)
            {
                RaycastHit hit = data.CurrentPointsCastData.HitData.Value;
                if (hit.rigidbody != null)
                {
                    GrabbableObject grabbableObject = hit.rigidbody.GetComponent<GrabbableObject>();
                    if (grabbableObject != null && grabbableObject != leftGrabHandler.GrabbedObject)
                    {
                        var grabTransform = grabbableObject.transform;

                        var headPos = headTracker.position;
                        var oldPos = grabTransform.position;
                        var dir = (oldPos - headPos).normalized;

                        if (Vector3.Distance(oldPos, headPos) > scaleStartDistance)
                        {
                            grabTransform.position = headPos + dir * scaleStartDistance;
                            grabTransform.localScale *= Vector3.Distance(headPos, grabTransform.position) /
                                                        Vector3.Distance(headPos, oldPos);
                        }

                        leftGrabHandler.GrabObject(grabbableObject);
                        return;
                    }
                }
            }

            if (leftGrabHandler.FloatGrabbing)
            {
                GrabbableObject grabbed = leftGrabHandler.GrabbedObject;
                leftGrabHandler.ReleaseObject();

                var headPos = headTracker.position;
                var oldPos = grabbed.transform.position;
                var dir = (oldPos - headPos).normalized;
                var ray = new Ray(oldPos, dir);

                if (Physics.Raycast(ray, out var hitInfo))
                {
                    grabbed.transform.position = Vector3.Lerp(oldPos, hitInfo.point, scaleJumpLerp);
                    grabbed.transform.localScale *= Vector3.Distance(headPos, grabbed.transform.position)
                                                    / Vector3.Distance(headPos, oldPos);
                }
            }
        }

        public void DisableShadows(GrabbableObject grabbableObject)
        {
            foreach (var rend in grabbableObject.GetComponentsInChildren<Renderer>())
            {
                rend.shadowCastingMode = ShadowCastingMode.Off;
                rend.receiveShadows = false;
            }
        }

        public void EnableShadows(GrabbableObject grabbableObject)
        {
            foreach (var rend in grabbableObject.GetComponentsInChildren<Renderer>())
            {
                rend.shadowCastingMode = ShadowCastingMode.On;
                rend.receiveShadows = true;
            }
        }
    }
}