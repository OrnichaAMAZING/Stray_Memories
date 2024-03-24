using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class GhostController : MonoBehaviour
{

    public Transform[] targetPoint;
    public int currentPoint;

    public NavMeshAgent agent;
    public Animator animator;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float StunTime = 5f;
    public float stunCounter;

    public GameObject BF;

    public GameObject lastSentence;

    public bool fiveCheck;

    public bool CanTalk;

    public GameObject Dialog8;

    public GameObject PressE;

    public bool istalkG;
    public GameObject PickupText;

    public bool isLove;


    public static GhostController instance;

    private void Awake()
    {
        instance = this;
    }


    public enum AIState
    {
        isDead, isPower, isSeekTargetPoint, isSeekPlayer, isAttack, CanTalk
    }
    public AIState state;


    // Start is called before the first frame update
    void Start()
    {
        BF.SetActive(true);
        PickupText.SetActive(false);
        lastSentence.SetActive(false);
        waitCounter = waitAtPoint;
        stunCounter = StunTime;
        fiveCheck = false;
        CanTalk = false;
        Dialog8.SetActive(false);
        PressE.SetActive(false);
        
        isLove = false;
    }
    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (!PlayerController.instance.isDead)
        {
            if (distanceToPlayer >= 1 && distanceToPlayer <= 15f)
            {
                state = AIState.isSeekPlayer;
            }
            else if (CanTalk && Input.GetKeyDown(KeyCode.E) && distanceToPlayer <= 10f)
            {
                state = AIState.CanTalk;
            }
            else if (distanceToPlayer >= 15f && distanceToPlayer > 15)
            {
                state = AIState.isSeekTargetPoint;
            }
            else
            {
                state = AIState.isAttack;
            }
        }
        else
        {
            state = AIState.isSeekTargetPoint;
            animator.SetBool("Attack", false);
            animator.SetBool("Run", true);
        }

        if (PlayerController.instance.CooldownCheck == true && fiveCheck == false)
        {

            state = AIState.isPower;
        }
        else if (distanceToPlayer >= 8f && distanceToPlayer > 8)
        {
            PlayerController.instance.isPower = false; 
        }


        switch (state)
        {
            case AIState.isDead:
                break;

            case AIState.isPower:
                if (stunCounter > 0)
                {

                    stunCounter -= Time.deltaTime;
                    animator.SetBool("Counter", true);
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", false);
                    agent.isStopped = true;


                }
                else
                {
                    stunCounter = StunTime;
                    animator.SetBool("Counter", false);
                    animator.SetBool("Run", true);
                    agent.isStopped = false;
                    agent.SetDestination(targetPoint[currentPoint].position);
                    fiveCheck = true;
                }
                break;
            case AIState.isSeekPlayer:
                agent.SetDestination(PlayerController.instance.transform.position);
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                break;
            case AIState.isSeekTargetPoint:
                agent.SetDestination(targetPoint[currentPoint].position);
                agent.stoppingDistance = 0;
                if (agent.remainingDistance <= .2f)
                {
                    if (waitCounter > 0)
                    {
                        waitCounter -= Time.deltaTime;
                        animator.SetBool("Run", false);
                    }
                    else
                    {
                        currentPoint++;
                        waitCounter = waitAtPoint;
                        animator.SetBool("Run", true);
                    }
                    if (currentPoint >= targetPoint.Length)
                    {
                        currentPoint = 0;
                    }


                    agent.SetDestination(targetPoint[currentPoint].position);
                }
                break;

            case AIState.isAttack:
                RotateTowardsPlayer();
                agent.stoppingDistance = 1;
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true); // Start or continue attack animation
                break;
            case AIState.CanTalk:
                agent.isStopped = true;
                animator.SetBool("Counter", false);
                animator.SetBool("Run", false);
                animator.SetBool("Attack", false);
                break;
        }

        if (PlayerController.instance.CooldownCheck == false)
        {
            fiveCheck = false;
        }

        if (ItemCollector.instance.AllCollect == true && distanceToPlayer <= 10f)
        {
            PressE.SetActive(true);
            PickupText.SetActive(false);
            CanTalk = true;
            if (CanTalk == true && Input.GetKeyDown(KeyCode.E))
            {
                Dialog8.SetActive(true);
                isLove = true;
                BF.SetActive(false);
                PressE.SetActive(false);
            }
        }



        void RotateTowardsPlayer()
        {
            Vector3 direction = (PlayerController.instance.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }


    }

    IEnumerator EndDialogAndDisappear()
    {
        // Wait until dialog ends
        while (!DialogIsClosed())
        {
            yield return null;
        }

        // Check if the last dialog is active
        if (lastSentence.activeSelf)
        {
            // Hide ghost model
            BF.SetActive(false);
        }
    }

    bool DialogIsClosed()
    {
        // Replace this with your condition to check if the dialog is closed
        // For example, if the dialog8 game object is inactive, return true
        return !Dialog8.activeSelf;
    }

/*    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BoxDamage"))
        {
            PressE.SetActive(true);
            PickupText.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PressE.SetActive(false);
    } */

}

