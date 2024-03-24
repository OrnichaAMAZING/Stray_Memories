using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcontroller : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    Vector3 dest;

    void Update()
    {
        dest = player.position;
        ai.destination = dest;
        if (ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnim.ResetTrigger("walk");
            aiAnim.SetTrigger("idle");
        }
        else
        {
            aiAnim.ResetTrigger("idle");
            aiAnim.SetTrigger("walk");
        }
    }
}
