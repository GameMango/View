using UnityEngine;

namespace View.Character.Tool
{
    public class ToolController : MonoBehaviour
    {
        public GameObject grabbedObject;
        private Rigidbody _grabbedRigidbody;
        private float _origDrag;
        private float _origMass;

       
        public LineRenderer lineRendererTranslate;
        public float maxGrabDistance = 10;
        public float stickSensitivity = 0.1f;
        public float minGrabDistance = 0.3f;
        public float previewThreshold = 0.1f;
        public float triggerThreshold = 0.9f;
        public float forceMult = 0.5f;

        public float grabDrag = 8;
        public float grabMass = 1;

        public float rotationStickSensitivity = 1;
        public float rotationHandSensitivity = 1;

        private bool _grabbing;
        private bool _triggeredGrab;
        private bool _triggeredRotate;
        private bool _startRotate;
        private float _grabDistance;

        private float _rotateZLast;

        public bool Rotating => _triggeredRotate;
        public bool Grabbing => _grabbing;

//        private void FixedUpdate()
//        {
//            if (//primary trigger >= triggerThreshold)
//            {
//                if (_triggeredGrab) return;
//                if (_grabbing)
//                {
//                    _grabbedRigidbody.useGravity = true;
//                    _grabbedRigidbody.drag = _origDrag;
//                    _grabbedRigidbody.mass = _origMass;
//                    _grabbedRigidbody.freezeRotation = false;
//                    grabbedObject = null;
//                    _grabbedRigidbody = null;
//                    _grabbing = false;
//                    _triggeredRotate = false;
//                }
//                else
//                {
//                    Vector3 pos = //primary position;
//                    Vector3 rotEuler =
//                        //primary rotation;
//                    Quaternion rot = Quaternion.Euler(rotEuler);
//                    Ray ray = new Ray(pos, rot * Vector3.forward);
//                    if (Physics.Raycast(ray, out var hit, maxGrabDistance))
//                    {
//                        if (hit.transform.gameObject.CompareTag("Grabbable"))
//                        {
//                            grabbedObject = hit.transform.gameObject;
//                            _grabbedRigidbody = grabbedObject.GetComponent<Rigidbody>();
//                            _grabbedRigidbody.useGravity = false;
//                            _origDrag = _grabbedRigidbody.drag;
//                            _origMass = _grabbedRigidbody.mass;
//                            _grabbedRigidbody.mass = grabMass;
//                            _grabbedRigidbody.freezeRotation = true;
//                            _grabbedRigidbody.drag = grabDrag;
//                            _grabDistance = Vector3.Distance(pos, grabbedObject.transform.position);
//                            _grabbing = true;
//                        }
//                    }
//                }
//
//                _triggeredGrab = true;
//            }
//            else
//            {
//                _triggeredGrab = false;
//            }
//            
//            if (//secondary trigger >= triggerThreshold)
//            {
//                if (_triggeredRotate) return;
//                if (!_grabbing) return;
//
//                _startRotate = true;
//
//                _triggeredRotate = true;
//            }
//            else
//            {
//                _triggeredRotate = false;
//            }
//
//          
//        }
//
//        private void Update()
//        {
//            if (_grabbing)
//            {
//                Vector2 distance = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, vrCharacterController.primaryController);
//                _grabDistance = Mathf.Min(Mathf.Max(minGrabDistance, _grabDistance + distance.y * stickSensitivity),
//                    maxGrabDistance);
//
//                Vector3 pos = //primary position;
//                lineRendererTranslate.SetPosition(0, transform.position);
//                lineRendererTranslate.SetPosition(1, grabbedObject.transform.position);
//
//                Vector3 rotEuler =
//                   // primary rotation;
//                Quaternion rot = Quaternion.Euler(rotEuler);
//                _grabbedRigidbody.AddForce(
//                    (pos + rot * Vector3.forward * _grabDistance - grabbedObject.transform.position) * forceMult);
//
//                
//                
//                if (_triggeredRotate)
//                {
//                    if (_startRotate)
//                    {
//                        _rotateZLast = // secondary rotation;
//                        _startRotate = false;
//                    }
//                    
//                    Vector3 rotSecond = // seondary rotation;
//                    Vector2 stickInput = // secondary stick input;
//
//                    var grabPos = grabbedObject.transform.position;
//                    grabbedObject.transform.RotateAround(grabPos, // forward of head, (rotSecond.z - _rotateZLast) * rotationHandSensitivity);
//
//                    _rotateZLast = rotSecond.z;
//                    
//                    grabbedObject.transform.RotateAround(grabPos, // up of head, stickInput.x * rotationStickSensitivity);
//                    grabbedObject.transform.RotateAround(grabPos, // right of head, stickInput.y * rotationStickSensitivity);
//                }
//            }
//            else if (//primary trigger >= previewThreshold)
//            {
//                lineRendererTranslate.enabled = true;
//                Vector3 pos = //primary pos;
//                Vector3 rotEuler =
//                    // primary rot;
//                Quaternion rot = Quaternion.Euler(rotEuler);
//                lineRendererTranslate.SetPosition(0, pos);
//                lineRendererTranslate.SetPosition(1, pos + rot * Vector3.forward * maxGrabDistance);
//            }
//            else
//            {
//                lineRendererTranslate.enabled = false;
//            }
//        }
    }
}