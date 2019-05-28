using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EventTrigger = UnityEngine.Analytics.EventTrigger;

namespace UI
{
    public class Editor : MonoBehaviour
    {
        private GameObject _prefab;
        public GameObject hierarchyButton;
        public GameObject canvasParent;


        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        void Update()
        {
            if (Input.GetKey("right"))
            {
                _prefab.transform.position = Vector3.MoveTowards(_prefab.transform.position, _prefab.transform.forward,0);
            }
            else if (Input.GetKeyDown("up"))
            {
                
                _prefab.transform.position+=Vector3.forward;
            }
        }
        
        public void InstantiatePrefabs(Button button)
        {

            var path = "Map_Asset/PREFAB/Models/" + button.name;
            var loadPrefab = Resources.Load(path);
            _prefab = loadPrefab as GameObject;
            _prefab.transform.position = new Vector3(0, 0, 0);
            _prefab = Instantiate(_prefab);
            
        }
        public void InstantiateButton(Button button)
        {
            canvasParent = GameObject.Find("Canvas/Hierarchy");
            var pathButton = "Map_Asset/PREFAB/Models/Button";
            var loadButton = Resources.Load(pathButton);
            hierarchyButton = loadButton as GameObject;
            GameObject newBtn = Instantiate(hierarchyButton, canvasParent.transform, false) as GameObject;
            //hierarchyButton = Instantiate(hierarchyButton, canvasParent.transform, false);
            newBtn.GetComponent<Button>().onClick.AddListener(() => SelectPrefab(newBtn));
            newBtn.name = button.name;
            newBtn.GetComponentInChildren<Text>().text = button.name;
            newBtn.GetComponent<Hierarchy>().prefab = _prefab;
            
        }

        public void SelectPrefab(GameObject button)
        {
            _prefab = button.GetComponent<Hierarchy>().prefab;
        }

    }
}
