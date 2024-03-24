using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    public float cooldownTime = 10f;
    public float cooldownCount;
    public bool CooldownCheck = false;

    public Image StaminaBar;
    public  float Stamina , maxStamina;
    public float StaminaCost;
    public float ChargeRate;

    private Coroutine recharge;

    
    [SerializeField] public float playerspeed = 5f;
    [SerializeField] private Camera followcamera;

    

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    [SerializeField] private Cooldown cooldown;

    public Animator animator;

    public bool isDead , isPower, isPress, isUsingStamina;

    public static PlayerController instance;

    Music_sfx audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Music_sfx>();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cooldownCount = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
      switch(isDead == true)
      {
        case true:
                animator.SetBool("Dead", true);
            break;
        case false:
            Movement();
            break;
      }


        if (PlayerController.instance.isDead)
        {
            audioManager.StopBackgroundMusic();
        }
        if(CheckWinner.instance.isWinner)
        {
            audioManager.StopBackgroundMusic();
        }


        UsePower();
      UseStamina();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followcamera.transform.eulerAngles.y, 0)
                                * new Vector3(horizontalInput, 0, verticalInput);

        Vector3 movementDirection = movementInput.normalized;

        characterController.Move(movementDirection * playerspeed * Time.deltaTime);


        if (movementDirection != Vector3.zero)
        {
            Quaternion desirdRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desirdRotation, rotationSpeed * Time.deltaTime);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));

        if (Input.GetKey(KeyCode.LeftShift) && Stamina > 0 && movementDirection != Vector3.zero)
        {
            characterController.Move(movementDirection * playerspeed * 4f * Time.deltaTime);
            Stamina -= StaminaCost * Time.deltaTime;

            if (Stamina <= 0) Stamina = 0;
                StaminaBar.fillAmount = Stamina / maxStamina;

                
            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
        else
        {
            characterController.Move(movementDirection * playerspeed * Time.deltaTime); // Move at normal speed
        }
    }

    void UseStamina()
    {
        if (Stamina > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Stamina", true); // Assuming this triggers the run animation
            isUsingStamina = true; // Set isUsingStamina to true when left shift key is held down
            isPress = true;
        }
        else
        {
            animator.SetBool("Stamina", false); // Assuming you have an opposite condition to stop the run animation
            isUsingStamina = false; // Set isUsingStamina to false when left shift key is released
        }
    }

    void UsePower()
    {
        if (Input.GetKey(KeyCode.E) && isPower == true && !CooldownCheck)
        {
            audioManager.PlaySFX(audioManager.Stun);
            animator.SetBool("Power", true);
            CooldownCheck = true;
            isPress = true;

        }


        if (CooldownCheck == true)
        {
            cooldownCount -= Time.deltaTime;

            if (cooldownCount <= 9.8)
            {
                animator.SetBool("Power", false);
            }

            if (cooldownCount <= 0)
            {
                cooldownCount = cooldownTime;
                CooldownCheck = false;
                isPress = false;
                isPower = false;
            }

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BoxDamage"))
        {
            isDead = true;
        }

         if(other.CompareTag("BoxPower"))
         {
             isPower = true;
         }

    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(Stamina < maxStamina)
        {
            Stamina += ChargeRate / 10f;
            if(Stamina > maxStamina) Stamina = maxStamina;
            StaminaBar.fillAmount = Stamina / maxStamina;

            yield return new WaitForSeconds(.1f);
        }
    }


}
