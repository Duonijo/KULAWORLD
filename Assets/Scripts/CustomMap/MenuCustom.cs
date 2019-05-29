using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCustom : MonoBehaviour
{
    void Start()
    {
        var path = Application.persistentDataPath + "/game1.save";

        if (File.Exists(path))
        {
            var panel = GameObject.Find("Canvas/Panel");
            var pathButton = "Map_Asset/PREFAB/Models/Button";
            var loadButton = Resources.Load(pathButton);
            var hierarchyButton = loadButton as GameObject;
            GameObject newBtn = Instantiate(hierarchyButton, panel.transform, false) as GameObject;
            newBtn.name = "map";
            newBtn.GetComponent<Button>().onClick.AddListener(() => LoadMap());
            newBtn.GetComponentInChildren<Text>().text = "map";
        }
    }

    void LoadMap()
    {
        SceneManager.LoadScene("Premap");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
