using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    
    [RequireComponent(typeof(Button))]

    public class showAndHide : MonoBehaviour
    {
        //diese menu soll erscheinen
        public GameObject showMenu;

        //thisButton ist der dieser Button und soll verschwinden
        public GameObject hideMenu;

        public void Show()
        {
            showMenu.SetActive(true);
            hideMenu.SetActive(false);
        }
 
    }

}

