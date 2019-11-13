using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.UI.animation
{
    public class CubeController : MonoBehaviour
    {

        public Animator anim;
        public bool open = false;

        // Start is called before the first frame update
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                anim.Play("Cube");
            }
            else if (Input.GetKeyDown("space"))
            {
                if (open)
                {
                    anim.SetTrigger("Close");
                    open = !open;
                }
                else
                {
                    anim.SetTrigger("Open");
                    open = !open;
                }


            }
        }
    }

}

