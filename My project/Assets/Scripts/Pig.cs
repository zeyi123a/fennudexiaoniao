using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private Bird bird;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bird = collision.gameObject.GetComponent<Bird>();
        if( bird != null)
        {
            
            Destroy(gameObject);
        }
    }
}
