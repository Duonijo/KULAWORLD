using System;
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
        
    }
    
}
