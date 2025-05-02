using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState
{
    waiting,
    beforeShoot,
    afterShoot,
    waitToDie
}

public class Bird : MonoBehaviour
{
    public BirdState state = BirdState.beforeShoot;
    private bool isMouseDown = false;
    public float maxDistance = 2.4f;
    public float flySpeed = 5;
    private Rigidbody2D rgd;
    private TrailRenderer trailRenderer;

    public int lives = 2;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive!");
        rgd = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case BirdState.waiting:
                break;
            case BirdState.beforeShoot:
                MoveControll();
                break;
            case BirdState.afterShoot:
                StopControll();
                break;
            case BirdState.waitToDie:
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (state == BirdState.beforeShoot)
        {
            isMouseDown = true;
            Slingshot.Instance.StartDraw(transform);
            AudioManager.Instance.PlayBirdSelect(transform.position);
        }
    }

    private void OnMouseUp()
    {
        if (state == BirdState.beforeShoot)
        {
            isMouseDown = false;
            Slingshot.Instance.EndDraw();
            Fly();
        }
    }

    private void MoveControll()
    {
        if (isMouseDown)
        {
            transform.position = GetMounsePosition();
        }
    }

    private Vector3 GetMounsePosition()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;

        Vector3 centerPosition = Slingshot.Instance.getCenterPosition();
        Vector3 mouseDir = mp - centerPosition;
        float distance = mouseDir.magnitude;
        if (distance > maxDistance)
        {
            mp = mouseDir.normalized * maxDistance + centerPosition;
        }
        return mp;
    }

    private void Fly()
    {
        rgd.bodyType = RigidbodyType2D.Dynamic;

        rgd.velocity = (Slingshot.Instance.getCenterPosition() - transform.position).normalized * flySpeed;

        state = BirdState.afterShoot;

        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }

        AudioManager.Instance.PlayBirdFlying(transform.position);
    }

    public void GoStage(Vector3 position)
    {
        state = BirdState.beforeShoot;
        transform.position = position;
    }

    private void StopControll()
    {
        if (rgd.velocity.magnitude < 0.1f)
        {
            state = BirdState.waitToDie;
            Invoke("HandleDeath", 1f);
        }
    }

    private void HandleDeath()
    {
        lives--;
        if (lives > 0)
        {
            state = BirdState.beforeShoot;
            transform.position = Slingshot.Instance.getCenterPosition();
            rgd.velocity = Vector2.zero;
            rgd.bodyType = RigidbodyType2D.Static;
            transform.rotation = Quaternion.identity;
            
            if (trailRenderer != null)
            {
                trailRenderer.enabled = false;
            }
            Slingshot.Instance.EndDraw(); 
        }
        else
        {
            Destroy(gameObject);
            GameObject.Instantiate(Resources.Load("Boom1"), transform.position, Quaternion.identity);
            GameManager.Instance.GameEnd(); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == BirdState.afterShoot && collision.relativeVelocity.magnitude > 5)
        {
            AudioManager.Instance.PlayBirdCollision(transform.position);
        }
    }
}    