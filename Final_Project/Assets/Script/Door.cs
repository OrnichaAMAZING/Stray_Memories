using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Door : MonoBehaviour
{


    public GameObject DoorLight;

    public ItemCollector itemcollector;

    public GameObject DoorOpen;

    public bool isOpen;

    // Start is called before the first frame update
    void Start()
    {

        DoorLight.SetActive(false);
        DoorOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (itemcollector.AllCollect)
        {
            isOpen = true;
            DoorLight.SetActive(true);
            DoorOpen.SetActive(true);

            // Find all objects with the "PP" tag and disable them
            GameObject[] ppObjects = GameObject.FindGameObjectsWithTag("PP");
            foreach (GameObject ppObject in ppObjects)
            {
                ppObject.SetActive(false);
            }
        }
    }

}
