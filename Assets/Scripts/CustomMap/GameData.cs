using UnityEngine;

namespace CustomMap
{
    [System.Serializable]
    public class GameData :  MonoBehaviour
    {
        [SerializeField]
        public string prefName;
        [SerializeField]
        public float posX;
        [SerializeField]
        public float posY;
        [SerializeField]
        public float posZ;
        [SerializeField]
        public float rotX;
        [SerializeField]
        public float rotY;
        [SerializeField]
        public float rotZ;
        [SerializeField]
        public string link;
        [SerializeField]
        public float endX;
        [SerializeField]
        public float endY;
        [SerializeField]
        public float endZ;


        
    }
    
}
