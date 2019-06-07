using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Bonus;
using BoxScripts;
using GamePlay;
using LevelEditor;
using Trap;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace CustomMap
{
    public class LoadCustom : MonoBehaviour
    {
        private GameObject _map;
        // Start is called before the first frame update
        public void Start()
        {
            _map = GameObject.Find("Map");
            
            var path = Application.persistentDataPath + "/Save/Save_" +PlayerPrefs.GetInt("SaveMap") + ".save";
            LoadData(path);
        }

        private void LoadData(string path)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                var save = (SaveCustom) binaryFormatter.Deserialize(fileStream);
                foreach (var pref in save.SaveList)
                {
                    var pathObj = "Map_Asset/PREFAB/Models/" + pref.Item1;
                    var loadPrefab = Resources.Load(pathObj);
                    var prefab = loadPrefab as GameObject;
                    if (!prefab) return;
                    prefab.transform.position = new Vector3(pref.Item2, pref.Item3, pref.Item4);
                    prefab.transform.rotation = Quaternion.Euler(pref.Item5, pref.Item6, pref.Item7);
                    Instantiate(prefab);
                    var newName = pref.Item1 + "(Clone)";
                    var selectedObject = GameObject.Find(newName);
                    selectedObject.AddComponent<GameData>();
                    selectedObject.name = pref.Item1;
                    var data = selectedObject.GetComponent<GameData>();
                    data.prefName = pref.Item1;
                    if(pref.Item8 != null) data.link = pref.Item8;

                    if (data.prefName == "Captivators" || data.prefName == "Laser")
                    {
                        data.endX = pref.Item9;
                        data.endY = pref.Item10;
                        data.endZ = pref.Item11;
                        if (data.prefName == "Laser")
                        {
                            selectedObject.GetComponent<Laser>().transform.position = new Vector3(pref.Item2, pref.Item3, pref.Item4);
                        }
                    }
                    if (selectedObject.GetComponent<BoxScript>() != null || selectedObject.GetComponent<StartingBox>() != null || selectedObject.GetComponent<EndBox>() != 
                        null || selectedObject.GetComponent<FlimsyBox>() != null || selectedObject.GetComponent<IceBox>() != null || selectedObject.GetComponent<InvisibleBox>() != null
                        || selectedObject.GetComponent<Transporters>() != null)
                    {
                        selectedObject.transform.SetParent(_map.transform);
                    }
                }
            }
            LoadScript();
        }

        private void LoadScript()
        {
            try
            {
                var listTransporters = FindObjectsOfType<Transporters>();
                var listCaptivators = FindObjectsOfType<Captivators>();
                var listLasers = FindObjectsOfType<Laser>();
                var listKeys = FindObjectsOfType<Keys>();
                if (listTransporters.Length != 0)
                {
                    listTransporters[0].link = listTransporters[1].transform.parent.gameObject;
                    listTransporters[0].tpLink = listTransporters[1];
                    listTransporters[1].link = listTransporters[0].transform.parent.gameObject;
                    listTransporters[1].tpLink = listTransporters[0];
                }

                if (listCaptivators.Length != 0)
                {
                    foreach (var captivator in listCaptivators)
                    {
                        var data = captivator.GetComponent<GameData>();
                        captivator.endPointV = new Vector3(data.endX, data.endY,data.endZ);
                    }
                }
                if (listKeys.Length != 0)
                {
                    var parent = GameObject.Find("Key");
                    int i = 1;
                    foreach (var key in listKeys)
                    {
                        key.gameObject.name = "Key" + i;
                        key.transform.SetParent(parent.transform,true);
                        i++;
                    }
                }

                if (listLasers.Length != 0)
                {
                    foreach (var laser in listLasers)
                    {
                        var data = laser.GetComponent<GameData>();
                        laser.Direction = new Vector3(data.endX, data.endY,data.endZ);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }
    }
}
