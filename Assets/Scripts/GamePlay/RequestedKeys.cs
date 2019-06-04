using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay
{
    public class RequestedKeys : MonoBehaviour
    {
        public int KeysGot
        {
            get => _keysGot;
            set => _keysGot = value;
        }

        public Image Key1
        {
            get => _key1;
            set => _key1 = value;
        }

        public Image Key2
        {
            get => _key2;
            set => _key2 = value;
        }

        public Image Key3
        {
            get => _key3;
            set => _key3 = value;
        }

        private bool _isDisplay;
        private int _keysFound;
        public Sprite hereKey;
        public Image spriteKey;
        private Image _key1;
        private Image _key2;
        private Image _key3;
        private int _keysGot;

        public int KeysFound
        {
            get => _keysFound;
            set => _keysFound = value;
        }

        // Start is called before the first frame update
        void Start()
        {
            _isDisplay = false;
            _keysFound = 0;
            _keysGot = 0;
            _key1 = GameObject.Find("Canvas/Panel/Key1").GetComponent<Image>();
            _key2 = GameObject.Find("Canvas/Panel/Key2").GetComponent<Image>();
            _key3 = GameObject.Find("Canvas/Panel/Key3").GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_keysFound != 0 && !_isDisplay)
            {
                DisplayRequest();
            }
        }

        void DisplayRequest()
        {
            switch (_keysFound)
            {
                case 3:
                    GameObject.Find("Canvas/Panel/Key3").SetActive(true);
                    GameObject.Find("Canvas/Panel/Key2").SetActive(true);
                    GameObject.Find("Canvas/Panel/Key1").SetActive(true);

                    switch (name)
                    {
                        case "Key1":
                            spriteKey = _key1;
                            break;
                        case "Key2":
                            spriteKey = _key2;
                            break;
                        default:
                            spriteKey = _key3;
                            break;
                    }
                    break;
				
                case 2:
                    GameObject.Find("Canvas/Panel/Key3").SetActive(false);
                    GameObject.Find("Canvas/Panel/Key2").SetActive(true);

                    spriteKey = name == "Key1" ? _key1 : _key2;
                    break;
                case 1:
                    spriteKey = _key1;
                    GameObject.Find("Canvas/Panel/Key2").SetActive(false);
                    GameObject.Find("Canvas/Panel/Key3").SetActive(false);
                    break;
					
            }

            _isDisplay = true;

        }
    }
}
