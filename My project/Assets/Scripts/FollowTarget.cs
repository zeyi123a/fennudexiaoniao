using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    public float smoothSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            Vector3 position = transform.position;
            position.x = target.position.x;
            position.x = Mathf.Clamp(position.x,0,20);
            transform.position = Vector3.Lerp(transform.position,position,Time.deltaTime*smoothSpeed);
        }
    }

    public void SetTarget(Transform transform){
        this.target = transform;
    }
}
