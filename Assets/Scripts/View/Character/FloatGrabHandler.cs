using UnityEngine;
using View.Objects;
using VRTK.Prefabs.Interactions.Interactables;
using VRTK.Prefabs.Interactions.Interactors;
using Zinnia.Data.Attribute;
using Zinnia.Data.Type;

namespace View.Character
{
    public class FloatGrabHandler : MonoBehaviour
    {
        public InteractorFacade interactor;
        public Transform follow;
        
        public float distanceSensitivity = 0.1f;
        public float floatDrag = 8;
        public float floatMass = 1;
        public float floatForceMultiplier = 40f;
        
        [MinMaxRange(0f, 100f)] public FloatRange grabDistanceBounds = new FloatRange(0.3f, 10f);


        public bool FloatGrabbing => _floatGrabbing;
        public bool StandardGrabbing => interactor.GrabbedObjects.Count != 0;
        public bool Grabbing => _floatGrabbing || StandardGrabbing;
        public GrabbableObject GrabbedObject => _grabbedObject;

        private GrabbableObject _grabbedObject;
        private bool _floatGrabbing;

        private float _origDrag;
        private float _origMass;
        private float _grabDistance;
        
        public void MoveGrabDistance(float input)
        {
            if (!_floatGrabbing) return;
            float currentDistance = Vector3.Distance(follow.transform.position, _grabbedObject.transform.position);
            if (currentDistance <= grabDistanceBounds.minimum)
            {
                InteractableFacade interactable = _grabbedObject.gameObject.GetComponent<InteractableFacade>();
                if (interactable != null)
                {
                    ReleaseObject();
                    interactor.Grab(interactable);
                    return;
                }
            }
                
            _grabDistance = Mathf.Clamp(_grabDistance + input * distanceSensitivity, grabDistanceBounds.minimum,
                grabDistanceBounds.maximum);
        }
        
        public void GrabObject(GrabbableObject o)
        {
            _grabDistance = Vector3.Distance(follow.position, o.transform.position);
            if (!grabDistanceBounds.Contains(_grabDistance)) return;
            _grabbedObject = o;
            _floatGrabbing = true;

            o.Rigidbody.freezeRotation = true;
            o.Rigidbody.useGravity = false;
            _origDrag = o.Rigidbody.drag;
            o.Rigidbody.drag = floatDrag;
            _origMass = o.Rigidbody.mass;
            o.Rigidbody.mass = floatMass;
        }

        public void ReleaseObject()
        {
            _grabbedObject.Rigidbody.freezeRotation = false;
            _grabbedObject.Rigidbody.useGravity = true;
            _grabbedObject.Rigidbody.drag = _origDrag;
            _grabbedObject.Rigidbody.mass = _origMass;
            _grabbedObject = null;
            _floatGrabbing = false;
        }

        private void FixedUpdate()
        {
            if (_floatGrabbing)
            {
                _grabbedObject.Rigidbody.AddForce(
                    (follow.position + follow.rotation * Vector3.forward * _grabDistance -
                     _grabbedObject.transform.position) * floatForceMultiplier);
            }
        }
    }
}