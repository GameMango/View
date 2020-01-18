using UnityEngine;

namespace View.Character
{
    public class ForwardStabilizer: MonoBehaviour
    {
        public Transform headSet;

        public void FixedUpdate()
        {
            transform.position = headSet.transform.position;
            Vector3 eulerRot = headSet.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulerRot.y, 0);
        }
    }
}