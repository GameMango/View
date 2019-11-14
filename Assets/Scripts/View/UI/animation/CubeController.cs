using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collision = View.Character.Tool.Collision;


namespace View.UI.animation
{
    public class CubeController : MonoBehaviour
    {

        private Animator _anim;
        private bool _open = false;

        // Start is called before the first frame update
        private void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               SwitchMenu();
            }
        }

        public void SwitchMenu()
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

