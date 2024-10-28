using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float turnSpeed = 20f;
    public float knockbackForce = 10f;//受击力度 
    public float knockbackDuration = 0.5f;// 受击硬直时间

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    // 是否屏蔽input
    private bool isInputBlocked;


    // 是否在受击硬直
    private bool isKnockback;


    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        // 订阅死亡事件
    }

    void FixedUpdate()
    {
        if (isInputBlocked)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking)
        {
            // 用soundmanager播放脚步声
            SoundManager.Instance.Play("FootSteps");
        }
        else
        {
            SoundManager.Instance.Pause("FootSteps");
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    // 受到某个方向的冲击，传递参数为3D向量
    public void Knockback(Vector3 direction)
    {
        if(isKnockback)
        {
            return;
        }

        m_Rigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
        
        m_Animator.SetTrigger("Hit");

        StartCoroutine(KnockbackRoutine());
    }

    // 硬直协程
    private IEnumerator KnockbackRoutine()
    {
        isInputBlocked = true;
        isKnockback = true;

        yield return new WaitForSeconds(knockbackDuration);

        isInputBlocked = false;
        isKnockback = false;
    }

    // 死亡动画
    public void Dead()
    {
        m_Animator.SetTrigger("Death");
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
