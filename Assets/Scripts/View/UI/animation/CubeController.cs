using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.UI.animation
{
    public class CubeController : MonoBehaviour
    {

        private Animator _anim;
        private bool _open = false;

        // Start is called before the first frame update
        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                if (_open)
                {
                    _anim.SetTrigger("Close");
                    _open = !_open;
                }
                else
                {
                    _anim.SetTrigger("Open");
                    _open = !_open;
                }
            }
            else if (Input.GetKeyDown("space"))
            {
                if (_open)
                {
                    _anim.SetTrigger("Close");
                    _open = !_open;
                }
                else
                {
                    _anim.SetTrigger("Open");
                    _open = !_open;
                }


            }
        }
    }

}

