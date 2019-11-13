using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace View.Anmation
{
    public class CubeController : MonoBehaviour
    {

        public Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}

