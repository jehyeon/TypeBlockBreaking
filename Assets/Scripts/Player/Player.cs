﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 공격
    [SerializeField]
    private AttackRange attackRange;
    public int Damage = 1;
    public bool GuardMode = false;

    // 이동
    public int Index = 0;
    bool isGround = true;
    bool isMoving = false;
    public float moveTime = 0.45f;
    public float jumpPower = 5.0f;

    private Animator animator;
    private Rigidbody rigid;

    public float TempGroundGuardPower = 10f;
    public float Power
    { 
        get
        {
            if (isGround)
            {
                return TempGroundGuardPower;
            }

            return rigid.velocity.y;
        }
    }

    // Init
    #region "Init"
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        attackRange.Off();
    }
    #endregion

    #region "Act"
    IEnumerator CAttack()
    {
        attackRange.On();
        yield return new WaitForSeconds(0.1f);
        attackRange.Off();
    }

    public void Attack()
    {
        StartCoroutine(CAttack());
    }

    public void Guard()
    {
        GuardMode = true;
        attackRange.On();
    }

    public void CancelGuard()
    {
        GuardMode = false;
        attackRange.Off();
    }

    public void Guarded(float blocksPower)
    {
        rigid.AddForce(Vector3.down * blocksPower, ForceMode.Impulse);
    }

    IEnumerator CJump()
    {
        isGround = false;
        animator.SetBool("isGround", false);
        animator.SetTrigger("jump");

        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

        yield return new WaitForSeconds(0.05f);

        while (transform.position.y > 10.0f)
        {
            yield return null;
        }

        animator.SetBool("isGround", true);
        yield return new WaitForSeconds(0.1f);
        isGround = true;
    }

    public void Jump()
    {
        if (!isGround || isMoving)
        {
            return;
        }

        StartCoroutine(CJump());
    }

    IEnumerator CMove(Vector3 dir)
    {
        isMoving = true;
        animator.SetBool("isMoving", true);
        float elapsedTime = 0.0f;

        if (dir.x > 0)
        {
            animator.SetTrigger("right");
        }
        else
        {
            animator.SetTrigger("left");
        }

        Vector3 originPos = transform.position;
        Vector3 targetPos = transform.position + dir;

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        animator.SetBool("isMoving", false);
        isMoving = false;
    }

    public void MoveLeft()
    {
        if (!isGround || isMoving)
        {
            return;
        }

        if (Index < 0)
        {
            return;
        }

        Index--;
        StartCoroutine(CMove(Vector3.left * 2));
    }

    public void MoveRight()
    {
        if (!isGround || isMoving)
        {
            return;
        }

        if (Index > 0)
        {
            return;
        }

        Index++;
        StartCoroutine(CMove(Vector3.right * 2));
    }
    #endregion
}
