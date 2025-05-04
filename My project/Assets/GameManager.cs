using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    //场上小鸟的列表
    private Bird[] birdList;
    private int index = -1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        birdList = FindObjectsByType<Bird>(FindObjectsSortMode.None);

        LoadNextBird();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void LoadNextBird()
    {
        index++;
        birdList[index].GoStage(SlingShot.Instance.getCenterPosition());

    }
}
