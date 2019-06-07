using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomMap;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelEditor
{
    public class SaveScript : MonoBehaviour
    {
        private string _pathMapDir;
        private string _pathMapFile;
        private string _pathHighScore;
        private string _pathHighScoreDir;



        private int _nbFiles;
        public List<(string,float,float,float,float,float,float,string,float,float,float)> listSave;        
        public List<(int, string, long)> highScore;


        void Start()
        {
            _pathMapDir = Application.persistentDataPath + "/Save";
            _pathHighScore = Application.persistentDataPath + "/highScore.save";
            if(!Directory.Exists(_pathMapDir))
            {
                Directory.CreateDirectory(_pathMapDir); // returns a DirectoryInfo object

            }
            DirectoryInfo dir = new DirectoryInfo(_pathMapDir);
            FileInfo[] filesDir =  dir.GetFiles();
            _pathMapFile = _pathMapDir + "/Save_"+  (filesDir.Length+1).ToString()+".save";

            
            
            print(Application.persistentDataPath);
            listSave = new List<(string,float,float,float,float,float,float,string,float,float,float)>();
            highScore = new List<(int, string, long)>();

        }

        public void SaveData()
        {
            if (SceneManager.GetActiveScene().name == "Editor")
            {
                var prefab = FindObjectsOfType<GameObject>();
                foreach (var pref  in prefab)
                {
                    if (pref.GetComponent<GameData>() != null)
                    {
                        var component = pref.GetComponent<GameData>();
                        component.posX = pref.transform.position.x;
                        component.posY = pref.transform.position.y;
                        component.posZ = pref.transform.position.z;
                        component.rotX = pref.transform.rotation.eulerAngles.x;
                        component.rotY = pref.transform.rotation.eulerAngles.y;
                        component.rotZ = pref.transform.rotation.eulerAngles.z;
                        listSave.Add((component.prefName,component.posX, component.posY, component.posZ, component.rotX, component.rotY, component.rotZ,
                            component.link,component.endX,component.endY,component.endZ));
                    }
                
                }
                var save = new SaveCustom
                {
                    SaveList = listSave
                };

                var binaryFormat = new BinaryFormatter();
                using (var fileStream = File.Create(_pathMapFile))
                {
                    binaryFormat.Serialize(fileStream, save);
                }

            }
            else
            {
                //Win
               var script = GameObject.Find("Win/Board");
               var counter = script.gameObject.transform.childCount;

               for (int i = 0; i < counter; i++)
               {
                   var data = script.transform.GetChild(i);
                   var rank = data.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                   var playerName = data.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                   var score = data.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

                   highScore.Add((int.Parse(rank.text),playerName.text, long.Parse(score.text)));
               }
               var save = new SaveCustom
               {
                   HighScore = highScore
               };
               var binaryFormat = new BinaryFormatter();
               using (var fileStream = File.Create(_pathHighScore))
               {
                   binaryFormat.Serialize(fileStream, save);
               }
            }
        }
    }
}
