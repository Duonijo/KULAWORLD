using TMPro;
using UnityEngine;

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
        // Start is called before the first
        //frame update
        void Start()
        {
             _canvas = GameObject.Find("Canvas");
             _score = GameObject.Find("Canvas").GetComponent<Score>();
             _pathWin = "Map_Asset/PREFAB/Models/Win";
             _pathStats = "Map_Asset/PREFAB/Models/PlayerStats";

             _isInstantiate = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isInstantiate)
            {
                Instance();
            }
        }

        private void Instance()
        {
            var loadCanvas = Resources.Load(_pathWin);
            var canvas = loadCanvas as GameObject;
            var instance = Instantiate(canvas);
            var boardName = instance.name + "/Board";
            var board = GameObject.Find(boardName);
            var loadPanel = Resources.Load(_pathStats);
            var stats = loadPanel as GameObject;
            var instanceStats = Instantiate(stats, board.transform, true);

            var rank = instanceStats.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            var player = instanceStats.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            var score = instanceStats.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            rank.text = CompareRank().ToString();
            player.text = PlayerPrefs.GetString("Name", "Guest");
            score.text = _score.sharedScore.ToString();
            _isInstantiate = true;
            _canvas.SetActive(false);


        }

        private int CompareRank()
        {
            return 1; //actualiser le classement
        }
    }
}
