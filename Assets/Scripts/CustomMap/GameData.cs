using System;
using Bonus;
using TreeEditor;
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
        public string tpLink;
        [SerializeField]
        public string link;

        
    }
    
}
