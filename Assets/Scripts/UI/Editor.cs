using System;
using Bonus;
using CustomMap;
using GamePlay;
using TMPro;
using Trap;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Color = UnityEngine.Color;

namespace UI
{
    public class Editor : MonoBehaviour
    {
        private GameObject _prefab;
        public GameObject hierarchyButton;
        public GameObject canvasParent;
        public string axisGame;
        public GameObject selectedObject;
        public GameObject pairedObject;

        public GameObject selectButton;
        private GameObject _posInspector;
        private GameObject _rotInspector;
        private GameObject _scriptInspector;
        private Vector3 _startCameraPos;
        private Quaternion _startCameraRot;



        public GameObject Prefab
        {
            get => _prefab;
            set => _prefab = value;
        }

        private void Start()
        {
            _startCameraPos = GameObject.Find("MainCamera").transform.position;
            _startCameraRot = GameObject.Find("MainCamera").transform.rotation;

            _posInspector = GameObject.Find("Canvas/Inspector/Panel/Position/Input").gameObject;
            _rotInspector = GameObject.Find("Canvas/Inspector/Panel/Rotation/Input").gameObject;
            _scriptInspector = GameObject.Find("Canvas/Inspector/Panel/Script/Input").gameObject;
            GameObject.Find("Canvas/Inspector/Panel/Script").SetActive(false);
            
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
                InspectorInfo();

            }

            else if (Input.GetKeyDown("down"))
            {
                if (axisGame == "X")
                    selectedObject.transform.position -= Vector3.right;
                else if (axisGame == "Y")
                    selectedObject.transform.position -= Vector3.up;
                else if (axisGame == "Z") selectedObject.transform.position -= Vector3.forward;
                InspectorInfo();

            }
            if(selectButton.name != "Laser") DrawAxis(selectedObject.GetComponent<LineRenderer>());
        }
        
        public void InstantiatePrefabs(GameObject go)
        {

            var path = "Map_Asset/PREFAB/EditorModels/" + go.GetComponent<GameData>().prefName;
            var loadPrefab = Resources.Load(path);
            _prefab = loadPrefab as GameObject;
            _prefab.transform.position = new Vector3(0, 0, 0);
            pairedObject = Instantiate(_prefab);
            pairedObject.AddComponent<GameData>();
            pairedObject.AddComponent<LineRenderer>();
            var red = pairedObject.GetComponent<LineRenderer>();
            DrawAxis(red);
            var data = pairedObject.GetComponent<GameData>();
            data.prefName = go.GetComponent<GameData>().prefName;

            if (selectedObject.GetComponent<GameData>().prefName == "Transporters")
            {
                selectedObject.GetComponent<GameData>().link = data.prefName;
                data.link = selectedObject.GetComponent<GameData>().prefName;
            }
            selectedObject = pairedObject;
            InstantiateButton();

        }
        public void InstantiatePrefabs(Button button)
            {

                if(selectButton!= null && selectedObject!= null) selectedObject.GetComponent<LineRenderer>().enabled = false;
                var path = "Map_Asset/PREFAB/EditorModels/" + button.name;
                var loadPrefab = Resources.Load(path);
                _prefab = loadPrefab as GameObject;
                _prefab.transform.position = new Vector3(0, 0, 0);
                selectedObject = Instantiate(_prefab);
                selectedObject.AddComponent<GameData>();
                var data = selectedObject.GetComponent<GameData>();
                data.prefName = button.name;
                if (button.name == "Laser") return;
                selectedObject.AddComponent<LineRenderer>();
                var red = selectedObject.GetComponent<LineRenderer>();
                DrawAxis(red);
                
            }
        public void InstantiateButton(Button button)
        {
            canvasParent = GameObject.Find("Canvas/Hierarchy");
            var pathButton = "Map_Asset/PREFAB/Models/Button";
            var loadButton = Resources.Load(pathButton);
            hierarchyButton = loadButton as GameObject;
            GameObject newBtn = Instantiate(hierarchyButton, canvasParent.transform, false);
            newBtn.GetComponent<Button>().onClick.AddListener(() => SelectPrefab(newBtn));
            newBtn.name = button.name;
            newBtn.GetComponentInChildren<Text>().text = button.name;
            newBtn.GetComponent<Hierarchy>().prefab = selectedObject;
            selectButton = newBtn;
            CheckScript(selectedObject);
            InspectorInfo();

        }
        public void InstantiateButton()
        {
            canvasParent = GameObject.Find("Canvas/Hierarchy");
            var pathButton = "Map_Asset/PREFAB/Models/Button";
            var loadButton = Resources.Load(pathButton);
            hierarchyButton = loadButton as GameObject;
            GameObject newBtn = Instantiate(hierarchyButton, canvasParent.transform, false);
            newBtn.GetComponent<Button>().onClick.AddListener(() => SelectPrefab(newBtn));
            newBtn.name = selectedObject.GetComponent<GameData>().prefName;
            newBtn.GetComponentInChildren<Text>().text =selectedObject.GetComponent<GameData>().prefName;
            newBtn.GetComponent<Hierarchy>().prefab = selectedObject;
            selectButton = newBtn;
        }

        private void SelectPrefab(GameObject button)
        {
            selectButton = button;
            if(selectButton!= null && selectedObject!= null) selectedObject.GetComponent<LineRenderer>().enabled = false;
            selectedObject = button.GetComponent<Hierarchy>().prefab;
            selectedObject.GetComponent<LineRenderer>().enabled = true;
            if(button.name != "Captivators" && button.name != "Laser")
            {
                GameObject.Find("Canvas/Inspector/Panel/Script").SetActive(false);
            }
            else
            {
                GameObject.Find("Canvas/Inspector/Panel/Script").SetActive(true);

            }
            InspectorInfo();
        }

        private void DrawAxis(LineRenderer line)
        {
            line.SetPosition(0, selectedObject.transform.position);
            switch (axisGame)
            {
                case "X":
                    axisGame = "X";
                    line.material.color = Color.red;
                    line.SetPosition(1,selectedObject.transform.position + Vector3.right*4);
                    break;
                case "Y":
                    axisGame = "Y";
                    line.material.color = Color.green;
                    line.SetPosition(1,selectedObject.transform.position + Vector3.up*4);
                    break;
                case "Z":
                    axisGame = "Z";
                    line.material.color = Color.blue;
                    line.SetPosition(1,selectedObject.transform.position + Vector3.forward*4);
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
        public void CheckScript(GameObject instance)
        {
            if (instance.GetComponent<Laser>() != null)
            {
                GameObject.Find("Canvas/Inspector/Panel/Script").SetActive(true);
            }
            else if (instance.GetComponent<Transporters>() != null)
            {
                InstantiatePrefabs(instance);
            }
            else if (instance.GetComponent<Captivators>() != null)
            {

                GameObject.Find("Canvas/Inspector/Panel/Script").SetActive(true);
            }
            InspectorInfo();

        }
        public void DeleteSelection()
        {
            if (selectButton != null || selectedObject != null)
            {
                Destroy(selectButton);
                Destroy(selectedObject);
                selectButton = null;
                selectedObject = null;
                InspectorInfo();
            }
        }
        private void InspectorInfo()
        {
            var position = _posInspector.GetComponentsInChildren<InputField>();
            var rotation = _rotInspector.GetComponentsInChildren<InputField>();
            var script = _scriptInspector.GetComponentsInChildren<InputField>();
            var _lr = selectedObject.GetComponent<LineRenderer>();

            if (selectedObject != null)
            {
                position[0].text = selectedObject.transform.position.x.ToString();
                position[1].text = selectedObject.transform.position.y.ToString();
                position[2].text = selectedObject.transform.position.z.ToString();
                rotation[0].text = selectedObject.transform.rotation.eulerAngles.x.ToString();
                rotation[1].text = selectedObject.transform.rotation.eulerAngles.y.ToString();
                rotation[2].text = selectedObject.transform.rotation.eulerAngles.z.ToString();
                if (selectedObject.GetComponent<Captivators>() != null)
                {
                    var data = selectedObject.GetComponent<GameData>();
                    script[0].text = data.endX.ToString();
                    script[1].text = data.endY.ToString();
                    script[2].text = data.endZ.ToString();
                }

                if (selectedObject.GetComponent<LineRenderer>())
                {
                    var data = selectedObject.GetComponent<GameData>();
                    if (data.prefName == "Laser")
                    {
                        var start = new Vector3(float.Parse(position[0].text), float.Parse(position[1].text), float.Parse(position[2].text));
                        var _dir = new Vector3(data.endX, data.endY, data.endZ);
                        _lr.SetPosition(0, start);
                        _lr.SetPosition(1, _dir);
                    }
                }
            }
            else
            {
                int i = 0;
                foreach (var el in position)
                {
                    el.text = "0";
                    rotation[i].text = "0";
                    script[i].text = "0";
                    i++;
                }
            }
        }
        
        public void Apply(){
            Debug.Log("BUTTON APPLY");
            Text posX = _posInspector.transform.GetChild(0).GetChild(2).GetComponent<Text>();
            Text posY = _posInspector.transform.GetChild(1).GetChild(2).GetComponent<Text>();
            Text posZ = _posInspector.transform.GetChild(2).GetChild(2).GetComponent<Text>();
            Text rotX = _rotInspector.transform.GetChild(0).GetChild(2).GetComponent<Text>();
            Text rotY = _rotInspector.transform.GetChild(1).GetChild(2).GetComponent<Text>();
            Text rotZ = _rotInspector.transform.GetChild(2).GetChild(2).GetComponent<Text>();
            
            
            selectedObject.transform.position = new Vector3(float.Parse(posX.text), float.Parse(posY.text), float.Parse(posZ.text));
            selectedObject.transform.rotation = Quaternion.Euler(float.Parse(rotX.text), float.Parse(rotY.text), float.Parse(rotZ.text));
            if (selectButton.name == "Captivators" || selectButton.name =="Laser")
            {
                Text endX = _scriptInspector.transform.GetChild(0).GetChild(2).GetComponent<Text>();
                Text endY = _scriptInspector.transform.GetChild(1).GetChild(2).GetComponent<Text>();
                Text endZ = _scriptInspector.transform.GetChild(2).GetChild(2).GetComponent<Text>();

                var data = selectedObject.GetComponent<GameData>();
                data.endX = float .Parse(endX.text);
                data.endY = float .Parse(endY.text);
                data.endZ = float .Parse(endZ.text);

                if (selectButton.name == "Laser")
                {
                    var _lr = selectedObject.GetComponent<LineRenderer>();
                    var _dir = new Vector3(data.endX, data.endY, data.endZ);
                    _lr.SetPosition(1, _dir);
                }

            }
            InspectorInfo();
            
        }
        
        public void ResetView()
        {
            var cam = GameObject.Find("MainCamera");
            cam.transform.position = _startCameraPos;
            cam.transform.rotation = _startCameraRot;
        }

    }
}
