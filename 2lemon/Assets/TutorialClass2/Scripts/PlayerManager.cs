using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 单例模式
    public static PlayerManager Instance;

    public int playerHealth = 3;
    public int coin = 0;
    
    public bool isDead = false;

    private PlayerMovement playerMovement;

    // 在Awake中实例化单例
    private void Awake()
    {
        Instance = this;
        playerMovement = GetComponent<PlayerMovement>(); // 如果有2个玩家怎么办？
    }

    // 角色受伤方法，传递参数为伤害值
    public void TakeDamage(int damage, Vector3 knockbackDirection)
    {
        playerHealth -= damage;

        // 受伤后硬直
        playerMovement.Knockback(knockbackDirection);

        if (playerHealth <= 0)
        {
            Dead();
        }
    }

    // 角色获得金币方法
    public void GetCoin()
    {
        coin++;

        Debug.Log("You get a coin!");

        // 这里可以方便地再插入获取一定数量金币后增加的其它逻辑，比如增加一条命
    }

    //角色死亡方法
    public void Dead()
    {
        isDead = true;
        
        // 死亡事件触发


        Debug.Log("You are dead!");
    }

    //角色胜利方法
    public void Win()
    {
        Debug.Log("You win!");
    }
}