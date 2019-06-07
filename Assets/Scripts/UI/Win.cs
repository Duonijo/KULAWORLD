using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CustomMap;
using LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class Win : MonoBehaviour
    {
        private bool _isInstantiate;
        private string _pathWin;
        private string _pathStats;

        private GameObject _canvas;
        private GameObject _stats;

        private Score _score;

        private SaveScript _save;

        private string _pathScore;

        // Start is called before the first
        //frame update
        void Start()
        {
            _save = FindObjectOfType<SaveScript>();
            _canvas = GameObject.Find("Canvas");
            _score = GameObject.Find("Canvas").GetComponent<Score>();
            _pathWin = "Map_Asset/PREFAB/Models/Win";
            _pathStats = "Map_Asset/PREFAB/Models/PlayerStats";

            _isInstantiate = false;

            _pathScore = Application.persistentDataPath + "/highScore.save";
            if (File.Exists(_pathScore))
            {
                LoadData(_pathScore);
            }

            Instance(2, PlayerPrefs.GetString("Name"), _score.sharedScore);
            UpdateRank();
            _save.SaveData();
        }


        // Update is called once per frame

        private void Instance(int rank, string playerName, long score)
        {
            if (GameObject.Find("Win") == null)
            {
                var loadCanvas = Resources.Load(_pathWin);
                var canvas = loadCanvas as GameObject;
                var instance = Instantiate(canvas);
                instance.name = "Win";

            }

            var board = GameObject.Find("Win/Board");
            var loadPanel = Resources.Load(_pathStats);
            var stats = loadPanel as GameObject;
            var instanceStats = Instantiate(stats, board.transform, true);
            instanceStats.name = "PlayerStats";
            instanceStats.tag = "Stats";
            instanceStats.AddComponent<GameData>();
            instanceStats.GetComponent<GameData>().posX = score;

            var rankText = instanceStats.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var playerText = instanceStats.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            var scoreText = instanceStats.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            rankText.text = rank.ToString();
            playerText.text = playerName;
            scoreText.text = score.ToString();
            _isInstantiate = true;
            _canvas.SetActive(false);


        }

        private void LoadData(string path)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                var save = (SaveCustom) binaryFormatter.Deserialize(fileStream);
                foreach (var stats in save.HighScore)
                {
                    Instance(stats.Item1, stats.Item2, stats.Item3);
                }
            }
        }

        public void UpdateRank()
        {
            List<GameObject> statsList = new List<GameObject>();
            GameObject[] allStats = GameObject.FindGameObjectsWithTag("Stats");
            foreach (GameObject stats in allStats)
            {
                statsList.Add(stats);
            }

            //Sorting list and check it count
            if (statsList.Count > 0)
            {
                statsList.Sort((a, b) => (a.GetComponent<GameData>().posX).CompareTo(b.GetComponent<GameData>().posX));
            }

            int index = statsList.Count - 1 ;
            foreach (GameObject stats in allStats)
            {
                //stats.transform.SetSiblingIndex(index);
                stats.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (index+1).ToString();
                stats.GetComponent<GameData>().posY = float.Parse(stats.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);

                index--;
            }

            foreach (GameObject stats in allStats)
            {
                stats.transform.SetSiblingIndex((int)(stats.GetComponent<GameData>().posY) -1);
            }
        }
    }
}
