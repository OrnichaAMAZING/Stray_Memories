using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolingDownBG : MonoBehaviour
{

    public Image PowerImage;
    public float cooldownPower = 10f;
    public bool isCooldown = false;
    public KeyCode Ablity1;

    public Image Power_image;

    public static CoolingDownBG instance;

    private void Awake()
    {
        instance = this;

    }

        // Start is called before the first frame update
        void Start()
    {
        PowerImage.fillAmount = 0;

        Power_image.fillAmount = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        Ablity();


    }

    void Ablity()
    {

        if (PlayerController.instance.isPower == true && isCooldown == false)
        {
            Power_image.fillAmount = 1;


            if (Input.GetKeyDown(KeyCode.E) && !isCooldown)
            {
                // Activate the ability
                isCooldown = true;
                PowerImage.fillAmount = 1;
                Power_image.fillAmount = 0;
            }

            if (Input.GetKeyDown(KeyCode.E) && !isCooldown && ItemCollector.instance.AllCollect == true)
            {
                GhostController.instance.CanTalk = true;
            }
        }

        if(isCooldown)
        {
            PowerImage.fillAmount -= 1 / cooldownPower * Time.deltaTime;

            if (PowerImage.fillAmount <= 0)
            {
                PowerImage.fillAmount = 0;
                isCooldown = false;
                Power_image.fillAmount = 0;

            }
        }
        if (PlayerController.instance.isPower == false)
        {
            Power_image.fillAmount = 0;
        }
    }
}
