using UnityEngine;

namespace View.Character.Player
{
    public class OurPlayer : MonoBehaviour
    {
        public int level;

        public void SavePlayer()
        {
            SaveSystem.SavePlayer(this);
            
        }

        public void LoadPlayer()
        {
            OurPlayerData data = SaveSystem.LoadPlayer();

            level = data.level;

            Vector3 position;
            position.x = data.position[0];
            position.y= data.position[0];
            position.z = data.position[0];
            transform.position = position;

        }


    }
    
}
