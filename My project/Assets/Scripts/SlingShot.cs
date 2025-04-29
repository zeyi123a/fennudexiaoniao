using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    public static SlingShot Instance { get; private set; }
    private LineRenderer leftLineRenderer;
    private LineRenderer rightLineRenderer;
    private Transform leftpoint;
    private Transform rightpoint;
    private Transform centerpoint;
    
    private bool isDrawing=false;
    private Transform birdTransform;

    private void Awake()
    {
        Instance = this;

        leftLineRenderer = transform.Find("LeftLineRenderer").GetComponent<LineRenderer>();
        rightLineRenderer = transform.Find("RightLineRenderer").GetComponent<LineRenderer>();
        leftpoint = transform.Find("LeftPoint");
        rightpoint = transform.Find("RightPoint");
        centerpoint = transform.Find("CenterPoint");
    }

    // Start is called before the first frame update
    void Start()
    {
         
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrawing)
        {
            Draw();
        }
    }
    public void StartDraw(Transform birdTransform)
    {
        isDrawing=true;
        this.birdTransform=birdTransform;
        ShowLine();
    }
    public void EndDraw()
    {
        isDrawing=false;
        HideLine();
    }
    public void Draw()
    {
        Vector3 birdPosition = birdTransform.position;
        birdPosition = (birdPosition - centerpoint.position).normalized * 0.25f + birdPosition;

        leftLineRenderer.SetPosition(0, birdPosition);
        leftLineRenderer.SetPosition(1, leftpoint.position);
        
        rightLineRenderer.SetPosition(0, birdPosition);
        rightLineRenderer.SetPosition(1, rightpoint.position);

    }
    public Vector3 getCenterPosition()
    {
        return centerpoint.transform.position;
    }
    private void HideLine()
    {
        rightLineRenderer.enabled = false;
        leftLineRenderer.enabled=false;
       }
    private void ShowLine()
    {
        leftLineRenderer.enabled = true;
        rightLineRenderer.enabled=true;
    }
}
