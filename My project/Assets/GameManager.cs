using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Bird singleBird;

    private int pigDeadCount;
    private int pigTotalCount;

    private FollowTarget CameraFollowTarget;

    private void Awake()
    {
        Instance = this;
        pigDeadCount = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        singleBird = FindFirstObjectByType<Bird>();
        pigTotalCount = FindObjectsByType<Pig>(FindObjectsSortMode.None).Length;
        CameraFollowTarget = Camera.main.GetComponent<FollowTarget>();

        if (singleBird != null)
        {
            singleBird.GoStage(Slingshot.Instance.getCenterPosition());
            CameraFollowTarget.SetTarget(singleBird.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (singleBird != null && singleBird.lives <= 0)
        {
            GameEnd();
        }
    }

    public void OnPigDead()
    {
        pigDeadCount++;
        if (pigDeadCount >= pigTotalCount)
        {
            GameEnd();
        }
    }

    public void GameEnd()
    {
        print("Game End!");
    }
}    