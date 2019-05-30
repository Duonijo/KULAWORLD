using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomMap;
using UnityEngine;

namespace LevelEditor
{
    public class SaveScript : MonoBehaviour
    {
        private string _path;
        public List<(string,float, float,float)> listSave;

        void Start()
        {
            _path = Application.persistentDataPath + "/game1.save";
            print(Application.persistentDataPath);
            listSave = new List<(string, float, float, float)>();

        }

        public void SaveData()
        {
            var prefab = FindObjectsOfType<GameObject>();
            
            foreach (var pref  in prefab)
            {
                if (pref.GetComponent<GameData>() != null)
                {
                    var component = pref.GetComponent<GameData>();
                    component.posX = component.transform.position.x;
                    component.posY = component.transform.position.y;
                    component.posZ = component.transform.position.z;
                    listSave.Add((component.prefName,component.posX,component.posY,component.posZ));
                }
                
            }
            var save = new SaveCustom
            {
                SaveList = listSave
            };

            var binaryFormat = new BinaryFormatter();
            using (var FileStream = File.Create(_path))
            {
                binaryFormat.Serialize(FileStream, save);
            }

        }
        
    }
}
