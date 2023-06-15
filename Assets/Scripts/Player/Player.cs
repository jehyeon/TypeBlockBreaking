using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Fire = 0,
    Ice = 1,
    Wood = 2
}

public class Player : MonoBehaviour
{
    // 공격
    [SerializeField]
    private AttackRange attackRange;
    public bool GuardMode = false;
    public WeaponType Type;
    [SerializeField]
    private ParticleSystem[] attackEffect = new ParticleSystem[3];
    [SerializeField]
    private ParticleSystem[] attackAura = new ParticleSystem[3];

    // 이동
    public int Index = 1;
    bool isGround = true;
    bool isMoving = false;
    bool isHit = false;
    bool isAttack = false;
    public float moveTime = 0.45f;
    public float jumpPower = 5.0f;

    private Animator animator;
    private Rigidbody rigid;

    // 기타
    private int nowHp;
    private int maxHp = 50;

    public float TempGroundGuardPower = 10f;
    public float TempAttackedBouncePower = 50f;
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
        Index = 1;
        attackRange.Off();

        nowHp = maxHp;
        UIManager.Instance.UpdateHPSlider(nowHp, maxHp);

        Type = WeaponType.Wood;     // temp
        ChangeType(0, false);
    }
    #endregion

    #region "Act"
    IEnumerator CAttack()
    {
        isAttack = true;
        attackRange.On();
        attackEffect[(int)Type].Play();
        SoundManager.Instance.PlayAttackSound();
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(0.2f);
        attackRange.Off();
        isAttack = false;
    }

    public void Attack()
    {
        if (!isAttack)
        {
            StartCoroutine(CAttack());
        }
    }

    public void Guard()
    {
        animator.SetBool("isGuard", true);
        GuardMode = true;
        attackRange.On();
    }

    public void CancelGuard()
    {
        animator.SetBool("isGuard", false);
        GuardMode = false;
        attackRange.Off();
    }

    public void Guarded(float blocksPower)
    {
        SoundManager.Instance.PlayGuardSound();
        rigid.AddForce(Vector3.down * blocksPower, ForceMode.Impulse);
    }

    private void Attacked()
    {
        nowHp--;
        UIManager.Instance.UpdateHPSlider(nowHp, maxHp);

        animator.SetTrigger("hit");
        SoundManager.Instance.PlayAttackedSound();
    }

    IEnumerator CJump()
    {
        isGround = false;
        animator.SetBool("isGround", false);
        animator.SetTrigger("jump");
        SoundManager.Instance.PlayJumpSound();

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
        SoundManager.Instance.PlayMoveSound();
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

        if (Index < 1)
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

        if (Index > 1)
        {
            return;
        }

        Index++;
        StartCoroutine(CMove(Vector3.right * 2));
    }

    public void ChangeType(int typeNum, bool soundPlay = true)
    {
        if (typeNum == (int)Type)
        {
            return;
        }

        Type = ((WeaponType)typeNum);

        for (int i = 0; i < 3; i++)
        {
            attackAura[i].gameObject.SetActive(false);
        }
        attackAura[typeNum].gameObject.SetActive(true);
        attackAura[typeNum].Play();

        if (soundPlay)
        {
            SoundManager.Instance.PlayEnchantSound();
        }
    }
    #endregion

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Blocks") && isGround)
        {
            // 바닥에 있을 때 블럭과 충돌하면
            Attacked();
            other.gameObject.GetComponent<Blocks>().Guarded(TempAttackedBouncePower);      // 다시 팅겨나감
        }
    }
}
