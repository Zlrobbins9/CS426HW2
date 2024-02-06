using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MS : MonoBehaviour
{
    public bool Dropped = false;
    public Material Material1;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Material1;
        Debug.Log(this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
