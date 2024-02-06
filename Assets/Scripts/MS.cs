using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MS : MonoBehaviour
{
    public bool Dropped = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
