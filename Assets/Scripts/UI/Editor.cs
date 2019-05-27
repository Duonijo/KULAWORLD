using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EventTrigger = UnityEngine.Analytics.EventTrigger;

namespace UI
{
    public class Editor : MonoBehaviour
    {
        private GameObject prefab;
        public void InstantiatePrefabs(Button button)
        {
            var path = "Map_Asset/PREFAB/Models/" + button.name;
            var loadPrefab = Resources.Load(path);
            prefab = loadPrefab as GameObject;
            Instantiate(prefab);
            prefab.transform.position = new Vector3(0, 0, 0);
            Debug.Log(SceneManager.GetActiveScene().name);
            Debug.Log(SceneManager.GetActiveScene().buildIndex);

        }
        
        public void OnClick(BaseEventData data)
        {
            PointerEventData pData = (PointerEventData)data;
        }

    }
}
