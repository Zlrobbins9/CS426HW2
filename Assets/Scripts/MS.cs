using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MS : MonoBehaviour
{
    // Define a public dictionary to map string colors to materials
    public Material defaultMat;
    public Material CPU;
    public Material IO;
    public Material keyboard;
    public Material mouse;
    public Material memory;
    public Material secondaryStorage;

    // Method to call to change the material
    public void ChangeMaterial(string colorName)
    {
        Renderer renderer = GetComponent<Renderer>(); // Get the Renderer component

        // Check the string and assign the material accordingly
        switch (colorName.ToLower())
        {
            case "CPU":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = CPU;
                break;
            case "IO":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = IO;
                break;
            case "mouse":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = mouse;
                break;
            case "keyboard":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = keyboard;
                break;
            case "memory":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = memory;
                break;   
            case "secondaryStorage":
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = secondaryStorage;
                break;                                         
            default:
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = defaultMat; // A default material if no match is found
                break;
        }
    }
    
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
