using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.Character.Tool
{
    public class Collision : MonoBehaviour
    {
    
        public GameObject model;
        public GameObject menuCube;
        public GameObject hand;
        public Animator anim;

        void OnCollisionEnter(UnityEngine.Collision col){
            if (col.gameObject.name == "RubiksCube")
            {
                model.SetActive(false);
                Destroy(col.gameObject);
                menuCube.SetActive(true);
                hand.SetActive(false);
                anim.SetTrigger("Open");
            }
        }
    
    }

}

