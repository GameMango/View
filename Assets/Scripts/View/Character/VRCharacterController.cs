using System;
using System.Collections.Generic;
using System.Linq;
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
        public float speed = 10;
        public bool sprinting;
        public float sprintMultiplier = 1.5f;

        public Transform headTracker;
        public float rotateDegree = 45;

        private Rigidbody _rigidbody;
        public InteractorFacade leftInteractor;
        public FloatGrabHandler rightGrabHandler;
        public BooleanAction rightForceCheckAction;
        public Transform rightForceCheckCenter;
        public Transform rightHand;
        public Vector3 rightForceCheckSize;

        public Animator leftHandAnimator;
        public Animator rightHandAnimator;
        public BooleanAction leftClosedAction;
        public BooleanAction rightClosedAction;

        private static readonly int Closed = Animator.StringToHash("closed");
        private int _grabbableLayer;

        private List<GrabbableObject> _objects = new List<GrabbableObject>();
        private Collider[] _results = new Collider[30];

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _grabbableLayer = LayerMask.GetMask("Grabbable");
        }

        public void Move(Vector3 dir)
        {
            _rigidbody.velocity = (sprinting ? sprintMultiplier : 1) * speed * dir;
        }

        public void ToggleSprint()
        {
            sprinting = !sprinting;
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude <= 0.01f)
            {
                sprinting = false;
            }
        }

        public void RotateLeft()
        {
            transform.RotateAround(headTracker.position, Vector3.up, -rotateDegree);
        }

        public void RotateRight()
        {
            transform.RotateAround(headTracker.position, Vector3.up, rotateDegree);
        }

        public GrabbableObject ForceCheck()
        {
            ClearHighlight();
            if (rightGrabHandler.StandardGrabbing) return null;
            var size = Physics.OverlapSphereNonAlloc(rightForceCheckCenter.transform.position, rightForceCheckSize.x,
                _results, _grabbableLayer);
            for (int i = 0; i < size; i++)
            {
                if (!_results[i].attachedRigidbody) continue;
                GrabbableObject o = _results[i].attachedRigidbody.GetComponent<GrabbableObject>();
                if (!o)
                    continue;
//                if (!Physics.Raycast(rightForceCheckCenter.transform.position,
//                     o.transform.position-rightForceCheckCenter.transform.position , out var hit))
//                    continue;
//                Debug.DrawRay(rightForceCheckCenter.transform.position,
//                    o.transform.position-rightForceCheckCenter.transform.position, Color.red, 100);
//                if (hit.collider.attachedRigidbody.gameObject.GetInstanceID() !=
//                    o.Rigidbody.gameObject.GetInstanceID())
//                    continue;
                _objects.Add(o);
            }

            if (_objects.Count == 0) return null;

            GrabbableObject nearest = _objects[0];
            nearest.highlighter.enabled = false;
            nearest.highlightClose.enabled = true;
            float distance = Vector3.Distance(rightHand.transform.position, nearest.transform.position);
            foreach (var grabbableObject in _objects)
            {
                grabbableObject.highlighter.enabled = true;
                if (Vector3.Distance(rightHand.transform.position, grabbableObject.transform.position) <
                    distance)
                {
                    nearest.highlightClose.enabled = false;
                    nearest.highlighter.enabled = true;
                    nearest = grabbableObject;
                    distance = Vector3.Distance(rightHand.transform.position, nearest.transform.position);
                    nearest.highlighter.enabled = false;
                    nearest.highlightClose.enabled = true;
                }
            }

            return nearest;
        }

        public void ClearHighlight()
        {
            foreach (var grabbableObject in _objects)
            {
                grabbableObject.highlighter.enabled = false;
                grabbableObject.highlightClose.enabled = false;
            }

            _objects.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
            Gizmos.DrawWireSphere (rightForceCheckCenter.position, rightForceCheckSize.x);
        }

        public void ForceGrab()
        {
            if (rightGrabHandler.Grabbing) return;
            GrabbableObject grabbableObject = ForceCheck();
            if (grabbableObject != null)
            {
                rightGrabHandler.GrabObject(grabbableObject);
                ClearHighlight();
            }
        }

        public void ReleaseRight()
        {
            if (rightGrabHandler.FloatGrabbing)
                rightGrabHandler.ReleaseObject();
        }

        private void UpdateAnimations()
        {
            leftHandAnimator.SetBool(Closed, leftClosedAction.Value);
            rightHandAnimator.SetBool(Closed, rightClosedAction.Value);
        }

        private void Update()
        {
            UpdateAnimations();
            if (rightForceCheckAction.Value && !rightGrabHandler.Grabbing)
                ForceCheck();
            else
                ClearHighlight();
        }

        public void ReleaseGrabbed()
        {
            ReleaseRight();
            leftInteractor.Ungrab();
            rightGrabHandler.interactor.Ungrab();
        }

        public void SuspendRight()
        {
            if (!rightGrabHandler.FloatGrabbing) return;
            rightGrabHandler.Suspend();
        }
    }
}