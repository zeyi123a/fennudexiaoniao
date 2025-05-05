using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperManager : MonoBehaviour
{
    public Vector2 initialspeed;
    private Rigidbody2D rgb;

    private SpriteRenderer rgbSprite;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        rgbSprite = GetComponent<SpriteRenderer>();
        rgbSprite.enabled = false;
    }

    public void HelperShoot()
    {
        rgbSprite.enabled = true;
        rgb.velocity = initialspeed;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.GetComponent<Destructiable>()){
            Destructiable[] destructiables = FindObjectsByType<Destructiable>(FindObjectsSortMode.None);
            foreach(Destructiable destructiable in destructiables){
                destructiable.Dead();
            }
            Destroy(gameObject);
        }
    }
    
}