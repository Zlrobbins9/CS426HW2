using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Material MyMaterial;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Mouse"))
        {
            
            if (MyMaterial == other.gameObject.GetComponent<MS>().Material1 && other.gameObject.GetComponent<MS>().Dropped == true)
            {
                Destroy(other.gameObject);
            }

        }

    }
}
