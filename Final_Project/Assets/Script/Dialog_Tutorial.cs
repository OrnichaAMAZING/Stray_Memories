using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog_Tutorial : MonoBehaviour
{
    private int Memories;

    public TextMeshProUGUI MemText;
    public GameObject Dialog;

    public static bool GameIsPaused = false;

    public GameObject PressE;
    public GameObject PickupText;

    public bool istalk;

    public Button button;

    Music_sfx audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Music_sfx>();
    }


    // Start is called before the first frame update
    void Start()
    {
        PickupText.SetActive(false);
        Dialog.SetActive(false);
        PressE.SetActive(false);

        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(BackToGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (istalk == true && Input.GetKeyDown(KeyCode.E)) // Change from Input.GetKey to Input.GetKeyDown
        {
            audioManager.PlaySFX(audioManager.nextPage);
            Dialog.SetActive(true);
            Time.timeScale = 0f;
            PressE.SetActive(false);
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Cat"))
        {
            istalk = true;
            PressE.SetActive(true);
            PickupText.SetActive(false);
        }
        else if (other.CompareTag("Memory"))
        {
            PickupText.SetActive(true);
            PressE.SetActive(false);
            if (Input.GetKey(KeyCode.E))
            {
                Memories++;
                MemText.text = "Memories : " + Memories.ToString() + "/6";
                Destroy(other.gameObject);
                PickupText.SetActive(false); // Deactivate PickupText after collecting the memory
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressE.SetActive(false);
        istalk = false;

    }

    public void BackToGame()
    {
        Dialog.SetActive(false);
        Time.timeScale = 1f;
    }
    
    


}
