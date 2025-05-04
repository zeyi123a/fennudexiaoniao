using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHand : MonoBehaviour
{
    //≥ı ºÀŸ∂»
    public Vector2 initialspeed;
    private Rigidbody2D rb;

    private SpriteRenderer rbSprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        rbSprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbSprite.enabled = true;
            BigHandShoot();
        }
    }
    private void BigHandShoot()
    {
        rb.velocity = initialspeed;
    }
    
}
