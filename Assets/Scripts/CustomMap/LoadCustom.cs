using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LevelEditor;
using UnityEngine;

namespace CustomMap
{
    public class LoadCustom : MonoBehaviour
    {
        // Start is called before the first frame update
        public void Start()
        {
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
                    var data = selectedObject.GetComponent<GameData>();
                    data.prefName = pref.Item1;
                }
            }
        }
    }
}
