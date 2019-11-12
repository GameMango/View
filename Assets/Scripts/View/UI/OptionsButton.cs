using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
   [RequireComponent(typeof(Button))]
    public class OptionsButton : MonoBehaviour
    {
        //diese button soll angezeigt werden
        public GameObject menu;
        
        //quitbutton soll auch nicht sichtbar sein
        public GameObject quit;

        //thisButton ist der dieser Button und soll verschwinden
        public GameObject thisButton;

        public void OptionButtonCLick()
        {
            menu.SetActive(true);
            thisButton.SetActive(false);
            quit.SetActive(false);
        }
    }
}

