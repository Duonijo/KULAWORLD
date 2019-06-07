using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CustomMap
{
    public class MenuCustom : MonoBehaviour
    {
        private string _pathDirectory;
        private string _pathFile;
        void Start()
        {
        
        
            _pathDirectory = Application.persistentDataPath + "/Save";


            DirectoryInfo dir = new DirectoryInfo(_pathDirectory);
            FileInfo[] filesDir =  dir.GetFiles();

            int index = 0;
            foreach (var file in filesDir)
            {
                var panel = GameObject.Find("Canvas/Panel");
                var pathButton = "Map_Asset/PREFAB/Models/Button";
                var loadButton = Resources.Load(pathButton);
                var hierarchyButton = loadButton as GameObject;
                GameObject newBtn = Instantiate(hierarchyButton, panel.transform, false) as GameObject;
                newBtn.name = index.ToString();
                newBtn.GetComponent<Button>().onClick.AddListener(() => LoadMap(newBtn));
                newBtn.GetComponentInChildren<Text>().text = "Map " + (index+1);
                index++;
            }
        }
        void LoadMap(GameObject button)
        {
            PlayerPrefs.SetInt("SaveMap", int.Parse(button.name));
            SceneManager.LoadScene("Premap");
        }
        // Update is called once per frame

    }
}
