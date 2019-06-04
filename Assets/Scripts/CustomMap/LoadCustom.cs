using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Bonus;
using BoxScripts;
using GamePlay;
using LevelEditor;
using Trap;
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
            var path = Application.persistentDataPath + "/game1.save";
            LoadData(path);
        }
        
        public void LoadData(string path)
        {
            SaveCustom save;
            
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                save = (SaveCustom) binaryFormatter.Deserialize(fileStream);
                foreach (var pref in save.SaveList)
                {
                    var pathObj = "Map_Asset/PREFAB/Models/" + pref.Item1;
                    var loadPrefab = Resources.Load(pathObj);
                    var prefab = loadPrefab as GameObject;
                    prefab.transform.position = new Vector3(pref.Item2, pref.Item3, pref.Item4);
                    Instantiate(prefab);
                    var newName = pref.Item1 + "(Clone)";
                    var selectedObject = GameObject.Find(newName);
                    selectedObject.AddComponent<GameData>();
                    selectedObject.name = pref.Item1;
                    if (selectedObject.GetComponent<BoxScript>() != null || selectedObject.GetComponent<StartingBox>() != null || selectedObject.GetComponent<EndBox>() != 
                        null || selectedObject.GetComponent<FlimsyBox>() != null || selectedObject.GetComponent<IceBox>() != null || selectedObject.GetComponent<InvisibleBox>() != null
                        || selectedObject.GetComponent<Transporters>() != null)
                    {
                        selectedObject.transform.SetParent(_map.transform);
                    }
                }
            }
        }
    }
}
