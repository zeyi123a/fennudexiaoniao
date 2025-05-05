using UnityEngine;

public class SkillObject : MonoBehaviour
{
    private void Awake()
    {
        // 将碰撞器设置为触发器
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bird bird = other.GetComponent<Bird>();
        if (bird != null)
        {
            AudioManager.Instance.PlaySkillPickUp(transform.position);

            GameManager.Instance.OnSkillObjectPickedUp();

            Destroy(gameObject);
        }
    }
}