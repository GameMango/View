namespace View.Character.Player{

[System.Serializable]
    public class OurPlayerData
    {
        public int level;
        public float[] position;
        public float[] rgb;

        public OurPlayerData(OurPlayer player)
        {
            level = player.level;
            position = new float[3];
            position[0] = player.transform.position.x;
            position[1] = player.transform.position.y;
            position[2] = player.transform.position.z;

        }
    }
}