using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuSnap : MonoBehaviour
{
    
    public UnityEvent triggered = new UnityEvent();
    public GameObject letter; 
    void OnTriggerEnter(Collider collider)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collider.gameObject.name == "LetterT")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Start Menu");
            letter = collider.gameObject;
            letter.transform.position = new Vector3(0.2f, 2.255f, 1.36f);
            //letter.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
            transform.rotation = Quaternion.Euler(90, 0, 0);
            triggered.Invoke();
        }

        
    }
}
