using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using View.UI.animation;

namespace View.Character.Tool
{
    public class Collision : MonoBehaviour
    {
    
        public GameObject model;
        public GameObject menuCube;
        public GameObject hand;
        public CubeController controller;

        void OnCollisionEnter(UnityEngine.Collision col){
            if (col.gameObject.name == "RubiksCube")
            {
                model.SetActive(false);
                Destroy(col.gameObject);
                menuCube.SetActive(true);
                hand.SetActive(false);
                controller.SwitchMenu();
                
            }
        }
    
    }

}

