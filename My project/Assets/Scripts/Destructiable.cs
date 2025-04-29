using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructiable : MonoBehaviour
{
   //���ƻ����Ѫ��
    public int maxHP = 100;
    private int currentHP;

    //����ͼƬ�ļ���
    public List<Sprite> injuredSpriteList;

    //���˵����
    private SpriteRenderer spriteRenderer;

    //��ը��Ч
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
