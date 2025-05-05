using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Bird singleBird;

    // 以下为了保证其他代码不报错，正式游戏逻辑中没有
    public Bird[] birdList;
    private int index = -1;

    private int pigDeadCount;
    private int pigTotalCount;

    private FollowTarget CameraFollowTarget;

    // 新增：技能物体的数量
    public int skillObjectCount;

    // 新增：大手对象
    public HelperManager helper;

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

        // 新增：统计技能物体的数量
        skillObjectCount = FindObjectsByType<SkillObject>(FindObjectsSortMode.None).Length;

        if (singleBird != null)
        {
            singleBird.GoStage(Slingshot.Instance.getCenterPosition());
            CameraFollowTarget.SetTarget(singleBird.transform);
        }

        helper = FindFirstObjectByType<HelperManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 检查小鸟生命值是否用完
        if (singleBird != null && singleBird.lives <= 0)
        {
            GameEnd();
        }

        // 新增：检查技能物体是否被拾取
        if (skillObjectCount <= 0)
        {
            GameEnd();
        }

        if(singleBird != null && singleBird.lives == 1){
            riggleHelper();
        }
    }

    // 新增：当技能物体被拾取时调用此方法
    public void OnSkillObjectPickedUp()
    {
        skillObjectCount--;
    }

    private void TriggleHelper(){
        if(helper != null){
            helper.gameObject.SetActive(true);
            helper.HelperShoot();
        }
    }

    // 以下为了保证其他代码不报错，正式游戏逻辑中没有
    public void LoadNextBird()
    {
        index++;

        if (index >= birdList.Length)
        {
            GameEnd();
        }
        else
        {
            birdList[index].GoStage(Slingshot.Instance.getCenterPosition());
            CameraFollowTarget.SetTarget(birdList[index].transform);
        }
    }

    // 以下为了保证其他代码不报错，正式游戏逻辑中没有
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