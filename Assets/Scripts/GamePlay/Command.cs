using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Command : MonoBehaviour
{
    private List<GameObject> _listeners = new List<GameObject>();

    private void Start(){
        AddListener(gameObject);
    }
    public void AddListener(GameObject listener)
    {
        if(!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void CommandInput(string input)
    {
        string[] parts = input.Split(new char[] { '.', '(', ')' }, 4);
        GameObject go = _listeners.Where(obj => obj.name == parts[0]).SingleOrDefault();

        if(go != null){
            go.SendMessage(parts[1], parts[2]);
        }
    }

    public void CreateSphere(string input){
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Debug.Log(input);
    }
}
