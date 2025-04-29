using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructiable : MonoBehaviour
{
   //可破坏物的血量
    public int maxHP = 100;
    private int currentHP;

    //受伤图片的集合
    public List<Sprite> injuredSpriteList;

    //受伤的组件
    private SpriteRenderer spriteRenderer;

    //爆炸特效
    private GameObject boomPrefab;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        boomPrefab = Resources.Load<GameObject>("Boom_1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentHP -= (int)(collision.relativeVelocity.magnitude * 8);

        if(currentHP <= 0)
        {
            Dead();
        }
        else
        {
            int index = (int)((maxHP - currentHP) / (maxHP / (injuredSpriteList.Count + 1.0f))) - 1;
            if (index != -1)
            {
                spriteRenderer.sprite = injuredSpriteList[index];
            }
        }
    }
    private void Dead()
    {
        GameObject.Instantiate(boomPrefab,transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
