using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // 金币旋转速度
    public float rotateSpeed = 1f;

    // 金币旋转方法
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0);
    }

    // 角色获取金币的方法
    // 把金币的获取方法放在金币脚本中，把金币增加得分的方法放在角色管理脚本中
    // 这样可以更好地分离金币和角色的逻辑，可以方便处理比如“角色获得100个金币后增加一条命”这样的逻辑
    public void GetCoin()
    {
        PlayerManager.Instance.GetCoin();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetCoin();
            Destroy(gameObject);
        }
    }
}