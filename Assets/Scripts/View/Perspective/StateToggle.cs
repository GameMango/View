using System;
using UnityEngine;

namespace View.Perspective
{
    public class StateToggle : MonoBehaviour
    {
        [Serializable]
        public struct AnimatorTrigger
        {
            public Animator animator;
            public String trigger;
        }
        
        public GameObject[] toEnable;
        public GameObject[] toDisable;
        public AnimatorTrigger[] triggers;

        public void Trigger()
        {
            foreach (var o in toEnable)
            {
                o.SetActive(true);
            }

            foreach (var o in toDisable)
            {
                o.SetActive(false);
            }

            foreach (var trigger in triggers)
            {
                trigger.animator.SetTrigger(trigger.trigger);
            }
        }

        private void FixedUpdate()
        {
            GetComponent<Rigidbody>().velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}