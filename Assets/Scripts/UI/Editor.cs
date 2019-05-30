using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using CustomMap;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using EventTrigger = UnityEngine.Analytics.EventTrigger;

namespace UI
{
    public class Editor : MonoBehaviour
    {
        private GameObject _prefab;
        public GameObject hierarchyButton;
        public GameObject canvasParent;
        public string axisGame;
        public GameObject selectedObject;

        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        private void Start()
        {
            axisGame = "X";
            selectedObject = null;
        }

        void Update()
        {

            if (Input.GetKeyDown("up"))
            {
                if (axisGame == "X")
                    selectedObject.transform.position += Vector3.right;

                else if (axisGame == "Y")
                {
                    selectedObject.transform.position += Vector3.up;
                }
                else if (axisGame == "Z")
                {
                    selectedObject.transform.position += Vector3.forward;
                }
            }

            else if (Input.GetKeyDown("down"))
            {
                if (axisGame == "X")
                    selectedObject.transform.position -= Vector3.right;
                else if (axisGame == "Y")
                    selectedObject.transform.position -= Vector3.up;
                else if (axisGame == "Z") selectedObject.transform.position -= Vector3.forward;
            }
    }
        
    public void InstantiatePrefabs(Button button)
        {

            var path = "Map_Asset/PREFAB/EditorModels/" + button.name;
            var loadPrefab = Resources.Load(path);
            _prefab = loadPrefab as GameObject;
            _prefab.transform.position = new Vector3(0, 0, 0);
            Instantiate(_prefab);
            var newName = button.name + "(Clone)";
            selectedObject = Instantiate(_prefab);
            selectedObject.AddComponent<GameData>();
            var data = selectedObject.GetComponent<GameData>();
            data.prefName = button.name;

        }
        public void InstantiateButton(Button button)
        {
            canvasParent = GameObject.Find("Canvas/Hierarchy");
            var pathButton = "Map_Asset/PREFAB/Models/Button";
            var loadButton = Resources.Load(pathButton);
            hierarchyButton = loadButton as GameObject;
            GameObject newBtn = Instantiate(hierarchyButton, canvasParent.transform, false) as GameObject;
            newBtn.GetComponent<Button>().onClick.AddListener(() => SelectPrefab(newBtn));
            newBtn.name = button.name;
            newBtn.GetComponentInChildren<Text>().text = button.name;
            newBtn.GetComponent<Hierarchy>().prefab = selectedObject;

        }

        public void SelectPrefab(GameObject button)
        {
            selectedObject = button.GetComponent<Hierarchy>().prefab;
        }

        public void SwapAxis(Button button)
        {
            var value = button.name;
            switch (value)
            {
                case "X":
                    axisGame = "X";
                    break;
                case "Y":
                    axisGame = "Y";
                    break;
                case "Z":
                    axisGame = "Z";
                    break;
            }
        }

    }
}
