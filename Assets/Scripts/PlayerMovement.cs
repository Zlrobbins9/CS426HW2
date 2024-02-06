using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;
// because we are using the NetworkBehaviour class
// NewtorkBehaviour class is a part of the Unity.Netcode namespace
// extension of MonoBehaviour that has functions related to multiplayer
public class PlayerMovement : NetworkBehaviour
{
    public float speed = 2f;
    public float rotationSpeed = 90;
    public float force = 700f;
    public bool MouseAte;
    public GameObject Mouth;
    public GameObject Mouse;
    public MS colorAssigner;


    public int MyCount; // Start count once mouse picked up.
    public int toCount; // To count when mouse will be dropped.

    Rigidbody rb;
    Transform t;
    // create a list of colors
    public List<Color> colors = new List<Color>();

    // getting the reference to the prefab
    [SerializeField]
    private GameObject spawnedPrefab;
    // save the instantiated prefab
    private GameObject instantiatedPrefab;

    public GameObject Mouth;        // Slide Child: Mouth here.
    public GameObject Mouse;        // Mouse prefab with MS script
    public GameObject EmptyMouse;  // Slide Child: MouseNOScript here

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;


    // Start is called before the first frame update
    void Start()
    {
        MouseAte = false;
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        colorAssigner.ChangeMaterial("keyboard");
        MyCount = 0;
        toCount = 1500 + Random.Range(0, 2147);

    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;


        // Time.deltaTime represents the time that passed since the last frame
        //the multiplication below ensures that GameObject moves constant speed every frame
        if (Input.GetKey(KeyCode.W))
            rb.velocity += this.transform.forward * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            rb.velocity -= this.transform.forward * speed * Time.deltaTime;

        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(t.up * force);


        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(t.up * force);


        /*
        if (Input.GetButtonDown("Fire1"))
        {
            if (MouseAte == true) //spit out mouse
            {
                GameObject newMouse = GameObject.Instantiate(Mouse, Mouth.transform.position, Mouth.transform.rotation) as GameObject;
                newMouse.GetComponent<MS>().Dropped = true;
                newMouse.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
                newMouse.GetComponent<Rigidbody>().AddForce(newMouse.transform.forward * 1500);
                newMouse.transform.parent = null;
                Debug.Log(newMouse.GetComponent<MS>().Dropped);
                MouseAte = false;
            }
        }
        */

        // if I is pressed spawn the object 
        // if J is pressed destroy the object
        if (Input.GetKeyDown(KeyCode.I))
        {
            //instantiate the object
            instantiatedPrefab = Instantiate(spawnedPrefab);
            // spawn it on the scene
            instantiatedPrefab.GetComponent<NetworkObject>().Spawn(true);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            //despawn the object
            instantiatedPrefab.GetComponent<NetworkObject>().Despawn(true);
            // destroy the object
            Destroy(instantiatedPrefab);
        }

        
        if (Input.GetButtonDown("Fire1") && MouseAte == true)
        {
            // call the BulletSpawningServerRpc method
            // as client can not spawn objects
            BulletSpawningServerRpc(Mouth.transform.position, Mouth.transform.rotation);
            MouseAte = false;
        }

        if (MouseAte == true)
        {
            /*if (MyCount % 20 == 0)     // Uncomment to wiggle mouse. ----------------
            {
                gameObject.transform.Find("MouseNOscript").gameObject.transform.rotation = new Quaternion(0, 26f, 0, 1);
            } else
            {
                gameObject.transform.Find("MouseNOscript").gameObject.transform.rotation = new Quaternion(0, 0.003f, 0, 1);
            }*/

            MyCount += 1;
            if (MyCount == toCount)
            {
                BulletSpawningServerRpc(Mouth.transform.position, Mouth.transform.rotation);
            }
        }



    }

    // this method is called when the object is spawned
    // we will change the color of the objects
    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object
        // enable the camera and the audio listener
        audioListener.enabled = true;
        playerCamera.enabled = true;
    }

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void BulletSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        BulletSpawningClientRpc(position, rotation);
    }

    [ClientRpc]
    private void BulletSpawningClientRpc(Vector3 position, Quaternion rotation)
    {
        GameObject newMouse = Instantiate(Mouse, position, rotation);
        newMouse.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
        newMouse.GetComponent<Rigidbody>().AddForce(newMouse.transform.forward * 1500);
        newMouse.GetComponent<MS>().Dropped = true;

        // Makes Child MouseNOscript hidden(spit out):
        GameObject mb = gameObject.transform.Find("MouseNOscript").gameObject;
        GameObject mt = mb.transform.Find("Maus").gameObject;
        mt.GetComponent<SkinnedMeshRenderer>().enabled = false;

        // New Random drop time for next mouse:
        toCount = 1500 + Random.Range(0, 2147);
        MyCount = 0;
        MouseAte = false;


    }



    private void OnCollisionEnter(Collision other)
    {

        if (MouseAte == false && other.gameObject.CompareTag("Mouse"))
        {
            if (other.gameObject.GetComponent<MS>().Dropped == false)
            {
                Destroy(other.gameObject);

                // Makes Child MouseNOscript visible:
                GameObject mb = gameObject.transform.Find("MouseNOscript").gameObject;
                GameObject mt = mb.transform.Find("Maus").gameObject;
                mt.GetComponent<SkinnedMeshRenderer>().enabled = true;

                MouseAte = true;
            }

        }

    }

    private void OnCollisionEnter(Collision other)
    {

        if (MouseAte == false && other.gameObject.CompareTag("Mouse"))
        {
            Debug.Log("Ate:" + other.gameObject.GetComponent<MS>().Dropped);
            //other.gameObject.transform.parent = gameObject.transform;
            if (other.gameObject.GetComponent<MS>().Dropped == false)
            {
                Destroy(other.gameObject);
                MouseAte = true;
            }

        }

    }
}