using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MS : MonoBehaviour
{
    // Define a public dictionary to map string colors to materials

    // Method to call to change the material
    public void ChangeMaterial(string colorName)
    {
        
    }
    public bool Dropped = false;
    public Material Material1;
    public int PickUP = 0;
    // Start is called before the first frame update
    void Start()
    {

        //gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Material1;
        //Debug.Log(this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if (Dropped == true)
        {
            PickUP += 1;
            if (PickUP == 1500)
            {
                Dropped = false;
            }
            GameObject skin = gameObject.transform.Find("Maus").gameObject;
            skin.GetComponent<SkinnedMeshRenderer>().material = Material1;
        }

    }
}
