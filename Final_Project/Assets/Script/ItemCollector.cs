using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int Memories;

    public TextMeshProUGUI MemText;
    public GameObject PickupText;
    public GameObject TalkText;
    public GameObject dialog1;
    public GameObject dialog2;
    public GameObject dialog3;
    public GameObject dialog4;
    public GameObject dialog5;
    public GameObject dialog6;
    public GameObject dialogLast;

    public GameObject cat_npc;

    public int memtocollect = 6;
    public bool AllCollect = false;

    public static ItemCollector instance;

    public bool DoorClose;
    public GameObject DoorCloseText;

    Music_sfx audioManager;

    private void Awake()
    {
        instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Music_sfx>();
    }


    // Start is called before the first frame update
    void Start()
    {
        cat_npc.SetActive(true);
        PickupText.SetActive(false);
        TalkText.SetActive(false);
        DoorCloseText.SetActive(false);
        dialog1.SetActive(true);
        dialog2.SetActive(false);
        dialog3.SetActive(false);
        dialog4.SetActive(false);
        dialog5.SetActive(false);
        dialog6.SetActive(false);
        dialogLast.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Memory")
        {
            PickupText.SetActive(true);
            TalkText.SetActive(false);
            if (Input.GetKey(KeyCode.E))
            {
                Memories++;
                MemText.text = "Memories : " + Memories.ToString() + "/6";
                audioManager.PlaySFX(audioManager.collect);
                Destroy(other.gameObject);
                PickupText.SetActive(false);
                dialog2.SetActive(true);
            }

            if (Memories == 2)
            {
                dialog3.SetActive(true);
            }

            if (Memories == 3)
            {
                dialog4.SetActive(true);
            }

            if (Memories == 4)
            {
                dialog5.SetActive(true);
            }

            if (Memories == 5)
            {
                dialog6.SetActive(true);
            }


            if (Memories >= memtocollect)
            {
                AllCollect = true;
                dialogLast.SetActive(true);
                cat_npc.SetActive(false);
            }

        }

        if (other.gameObject.tag == "PP")
        {
            DoorClose = true;
            DoorCloseText.SetActive(true);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        PickupText.SetActive(false);
        DoorCloseText.SetActive(false);
    }

}
