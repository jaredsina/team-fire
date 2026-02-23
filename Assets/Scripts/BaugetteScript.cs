using System;
using UnityEngine;
using System.Collections;

public class Baguette : MonoBehaviour
{
    public GameObject BaguetteSword;
    public bool CanAttack = true;
    public float AttackCoolDown = 1.0f;

    public void BaguetteAttack()
    {
        CanAttack = false;
        Animator anim = BaguetteSword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCoolDown());
    }

    IEnumerator ResetAttackCoolDown()
    {
        yield return new WaitForSeconds(AttackCoolDown);
        CanAttack = true;
    }        
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CanAttack)
            {
                BaguetteAttack();
            }
        }
    }
}