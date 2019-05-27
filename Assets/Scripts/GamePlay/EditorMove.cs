using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMove : MonoBehaviour
{
    private bool editorMode;
    // Start is called before the first frame update
    void Start()
    {
        editorMode = SceneManager.GetActiveScene().name == "Editor";
        print(editorMode);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!editorMode) return;
        if (Input.GetKey("right"))
        {
            var transform1 = transform;
            transform.position = Vector3.MoveTowards(transform1.position, transform1.forward,0);
        }
        else if (Input.GetKeyDown("up"))
        {
            print(transform.position);
            transform.position+=Vector3.forward;
            print(transform.position);
            print(transform.position+Vector3.forward);


        }
    }
    
    
}
