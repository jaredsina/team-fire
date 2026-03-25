using System;
using UnityEngine;
using System.Collections;

public class Carrot : MonoBehaviour
{
    public GameObject CarrotPrefab;
    public bool CanAttack = true;
    public float AttackCoolDown = 1.0f;

    public void CarrotAttack()
    {
        CanAttack = false;
        Animator anim = CarrotPrefab.GetComponent<Animator>();
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
                CarrotAttack();
            }
        }
    }
}