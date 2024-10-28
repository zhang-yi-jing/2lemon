using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;

    public float speed = 10f;

    public GameObject hitEffect;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // 子弹伤害角色生命值的方法
    // 将“伤害”和“受伤害”分开，伤害可以单独计算、受伤害也可以再次计算
    // 如果“伤害”是debuff呢？
    // 那么是不是还可以把“伤害”本身分离出来成为一个小组件呢？
    public void Damage()
    {
        // 当前物体速度在地面上的投影向量
        Vector3 knockbackDirection = new Vector3(
            rb.velocity.x, 0, rb.velocity.z
            );

        PlayerManager.Instance.TakeDamage(damage, knockbackDirection);
    }
    

    private void OnTriggerEnter(Collider other)
    {
        // 碰撞到角色时
        if (other.CompareTag("Player"))
        {
            Damage();
            Destroy(gameObject);
            // 在碰撞的地方生成特效
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            return;
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            // 在碰撞的地方生成特效
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            return;
        }
    }
}