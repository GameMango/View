using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace View.Character.Player
{


    public class SaveSystem : MonoBehaviour
    {

        public static void SavePlayer(OurPlayer player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/ourPlayer.view";
            
            FileStream stream = new FileStream(path, FileMode.Create);
            OurPlayerData data = new OurPlayerData(player);
            
            formatter.Serialize(stream,data);
            stream.Close();
            
        }

        public static OurPlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + "/ourPlayer.view";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path,FileMode.Open);

                OurPlayerData data = formatter.Deserialize(stream) as OurPlayerData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("File not found in " + path);
                return null;
            }
        }

    }
}