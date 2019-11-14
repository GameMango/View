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

        private void Start()
        {
            _anim = GetComponent<Animator>();
        }

        public void SwitchMenuState()
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