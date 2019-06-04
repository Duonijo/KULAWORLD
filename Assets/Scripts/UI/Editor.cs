using System;
using System.Collections;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Bonus;
using CustomMap;
using Trap;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Color = UnityEngine.Color;
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
        public GameObject selectButton;

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
            if (selectedObject == null) return;
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
            DrawAxis(selectedObject.GetComponent<LineRenderer>());
        }
        
    public void InstantiatePrefabs(GameObject go)
    {

        var path = "Map_Asset/PREFAB/EditorModels/" + go.name;
        var loadPrefab = Resources.Load(path);
        _prefab = loadPrefab as GameObject;
        _prefab.transform.position = new Vector3(0, 0, 0);
        //Instantiate(_prefab);
        var newName = go.name + "(Clone)";
        selectedObject = Instantiate(_prefab);
        selectedObject.AddComponent<GameData>();
        selectedObject.AddComponent<LineRenderer>();
        var red = selectedObject.GetComponent<LineRenderer>();
        DrawAxis(red);
        
        var data = selectedObject.GetComponent<GameData>();
        data.prefName = go.name;

    }
    public void InstantiatePrefabs(Button button)
        {

            if(selectButton!= null && selectedObject!= null) selectedObject.GetComponent<LineRenderer>().enabled = false;
            var path = "Map_Asset/PREFAB/EditorModels/" + button.name;
            var loadPrefab = Resources.Load(path);
            _prefab = loadPrefab as GameObject;
            _prefab.transform.position = new Vector3(0, 0, 0);
            //Instantiate(_prefab);
            var newName = button.name + "(Clone)";
            selectedObject = Instantiate(_prefab);
            selectedObject.AddComponent<GameData>();
            selectedObject.AddComponent<LineRenderer>();
            var red = selectedObject.GetComponent<LineRenderer>();
            DrawAxis(red);
            
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
            selectButton = newBtn;

        }

        public void SelectPrefab(GameObject button)
        {
            selectButton = button;
            if(selectButton!= null && selectedObject!= null) selectedObject.GetComponent<LineRenderer>().enabled = false;
            selectedObject = button.GetComponent<Hierarchy>().prefab;
            selectedObject.GetComponent<LineRenderer>().enabled = true;
        }

        public void DrawAxis(LineRenderer line)
        {
            line.SetPosition(0, selectedObject.transform.position);
            switch (axisGame)
            {
                case "X":
                    axisGame = "X";
                    line.material.color = Color.red;
                    line.SetPosition(1,selectedObject.transform.position + selectedObject.transform.right*4);
                    break;
                case "Y":
                    axisGame = "Y";
                    line.material.color = Color.green;
                    line.SetPosition(1,selectedObject.transform.position + selectedObject.transform.up*4);
                    break;
                case "Z":
                    axisGame = "Z";
                    line.material.color = Color.blue;
                    line.SetPosition(1,selectedObject.transform.position + selectedObject.transform.forward*4);
                    break;
            }
            
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
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

        public void checkScript(GameObject instance)
        {
            if (instance.GetComponent<Laser>() != null)
            {
                InstantiatePrefabs(instance);
            }
            else if (instance.GetComponent<Transporters>() != null)
            {
                
            }
        }

        public void DeleteSelection()
        {
            if (selectButton != null || selectedObject != null)
            {
                Destroy(selectButton);
                Destroy(selectedObject);
                selectButton = null;
                selectedObject = null;
            }
            


        }

    }
}
