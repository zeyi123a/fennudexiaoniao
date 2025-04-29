using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BirdState
{
    Waiting,
    BeforeShoot,
    AfterShoot
}
public class Bird : MonoBehaviour
{
    

    public  BirdState state= BirdState.BeforeShoot;
    //等待  发射前 发射后
    public bool isMouseDown = false;

    //最大拖拽距离
    public float maxDistance = 2.4f;

    //飞翔速度
    public float flySpeed = 4;
    private Rigidbody2D rgd;
    
    // Start is called before the first frame update
    void Start()
    {
        rgd= GetComponent<Rigidbody2D>();
        rgd.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
     
        switch (state)
        {
            case BirdState.Waiting:
                break;
            case BirdState.BeforeShoot:
                MoveControll();
                break;
            case BirdState.AfterShoot:
                break;
            default:
                break;
        }
    }
    //onMousedown //onMouseup
    private void OnMouseDown()
    {
        if (state == BirdState.BeforeShoot)
        {
            isMouseDown = true;
            SlingShot.Instance.StartDraw(transform);
        }
    }
    private void OnMouseUp()
    {
        if (state == BirdState.BeforeShoot)
        {
            isMouseDown = false;
            SlingShot.Instance.EndDraw();
            Fly();
        }
    }
    private void MoveControll()
    {
        if (isMouseDown)
        {
            transform.position = GetMousePosition();
        }
    }
    private Vector3 GetMousePosition()
    {
       Vector3 mp= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 centerPosition = SlingShot.Instance.getCenterPosition();
        mp.z = 0;

       Vector3 mouseDir= mp-centerPosition ;
        float distance = mouseDir.magnitude;
        if(distance > maxDistance)
        {
            mp = mouseDir.normalized * maxDistance + centerPosition;
        }
        
        return mp;

    }
    private void Fly()
    {
        rgd.bodyType=RigidbodyType2D.Dynamic;

        rgd.velocity = (SlingShot.Instance.getCenterPosition() - transform.position) * flySpeed;
        state=BirdState.AfterShoot;
    }

    public void GoStage( Vector3 position)
    {
        state = BirdState.BeforeShoot;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            transform.position=SlingShot.Instance.getCenterPosition();
            rgd.bodyType = RigidbodyType2D.Static;
            state = BirdState.BeforeShoot;
        }
    }
}
