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
        public BooleanAction showRightPointer;
        public BooleanAction showLeftPointer;
        public float speed = 10;

        public Transform headTracker;
        public float rotateDegree = 45;

        private Rigidbody _rigidbody;

        public float scaleStartDistance = 2f;
        public float scaleJump = 0.05f;
        public float scaleStop = 0.5f;

        public FloatGrabHandler leftGrabHandler;
        public FloatGrabHandler rightGrabHandler;

        public Animator leftHandAnimator;
        public Animator rightHandAnimator;
        public BooleanAction leftClosedAction;
        public BooleanAction rightClosedAction;
        
        private static readonly int Closed = Animator.StringToHash("closed");

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir)
        {
            if (teleporting) return;
            if (dir.magnitude < 0.001f) return;
            dir.y = 0;
            dir = dir.normalized;
            _rigidbody.velocity = dir * speed;
        }

        public void ShowCurvedPointer(bool val)
        {
            if (showCurvedAction == null) return;
            showCurvedAction.Receive(teleporting && val);
        }

        public void ShowRightPointer(bool val)
        {
            if (rightGrabHandler.FloatGrabbing || leftGrabHandler.FloatGrabbing ||
                rightGrabHandler.StandardGrabbing) return;
            showRightPointer.Receive(val);
        }

        public void ShowLeftPointer(bool val)
        {
            if (rightGrabHandler.FloatGrabbing || leftGrabHandler.FloatGrabbing ||
                leftGrabHandler.StandardGrabbing) return;
            showLeftPointer.Receive(val);
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
                        showRightPointer.Receive(false);
                        showLeftPointer.Receive(false);
                        return;
                    }
                }
            }
        }

        public void ReleaseRight()
        {
            if (rightGrabHandler.FloatGrabbing)
                rightGrabHandler.ReleaseObject();
        }

        public void ReleaseLeft()
        {
            if (!leftGrabHandler.FloatGrabbing) return;

            GrabbableObject grabbed = leftGrabHandler.GrabbedObject;
            leftGrabHandler.ReleaseObject();

            var headPos = headTracker.position;
            var oldPos = grabbed.transform.position;
            var dir = (oldPos - headPos).normalized;
            var ray = new Ray(oldPos, dir);

            if (Physics.Raycast(ray, out var hitInfo))
            {
                grabbed.transform.position = Vector3.Lerp(oldPos, hitInfo.point, 0.9f);
                grabbed.transform.localScale *= Vector3.Distance(headPos, grabbed.transform.position)
                                                / Vector3.Distance(headPos, oldPos);
            }
        }

        private void UpdateAnimations()
        {
            leftHandAnimator.SetBool(Closed, leftClosedAction.Value);
            rightHandAnimator.SetBool(Closed, rightClosedAction.Value);
        }
        
        private void Update()
        {
            UpdateAnimations();
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
                        showRightPointer.Receive(false);
                        showLeftPointer.Receive(false);
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
                    grabbed.transform.position = Vector3.Lerp(oldPos, hitInfo.point, 0.9f);
                    grabbed.transform.localScale *= Vector3.Distance(headPos, grabbed.transform.position)
                                                    / Vector3.Distance(headPos, oldPos);
                }
            }
        }

        public void ReleaseGrabbed()
        {
            ReleaseLeft();
            ReleaseRight();
            leftGrabHandler.interactor.Ungrab();
            rightGrabHandler.interactor.Ungrab();
        }

        public void SuspendLeft()
        {
            if (!leftGrabHandler.FloatGrabbing) return;
            leftGrabHandler.Suspend();
        }
        
        public void SuspendRight()
        {
            if (!rightGrabHandler.FloatGrabbing) return;
            rightGrabHandler.Suspend();
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