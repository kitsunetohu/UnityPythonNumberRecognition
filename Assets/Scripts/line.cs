using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Vector2sEvent: UnityEvent<Vector2[]>
{

}


public class line : MonoBehaviour
{
    public Vector2sEvent drawOver;//画完事件
    

    //LineRenderer
    private LineRenderer lineRenderer;
    //定义一个Vector3,用来存储鼠标点击的位置
    Vector3 position;
    //用来索引端点
    private int index = 0;
    //端点数
    private int LengthOfLineRenderer = 0;

    private List<Vector2> scrPoints=new List<Vector2>();

    Vector3 wordPostion;

    private void Start()
    {
        NetManger netManger = FindObjectOfType<NetManger>();
        drawOver.AddListener(netManger.sendVectors);
    }


    void Update()
    {
        //获取LineRenderer组件
       lineRenderer = GetComponent<LineRenderer>();
        //鼠标左击
        if (Input.GetMouseButton(0))
        {
            //将鼠标点击的屏幕坐标转换为世界坐标，然后存储到position中
            wordPostion = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);

            scrPoints.Add(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            position  = Camera.main.ScreenToWorldPoint(wordPostion);
            
            //端点数+1
            LengthOfLineRenderer++;
            //设置线段的端点数
            lineRenderer.positionCount = (LengthOfLineRenderer);

        }

        if (Input.GetMouseButtonUp(0))
        {

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(lineRenderer.startColor, 0.0f), new GradientColorKey(lineRenderer.startColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 1.0f) }
                );
            lineRenderer.colorGradient = gradient;
            //Vector2[] posts2 = new Vector2[lineRenderer.positionCount];
            //for(int i=0;i< lineRenderer.positionCount; i++)
            //{
            //    posts2[i] = new Vector2(lineRenderer.GetPosition(i).x, lineRenderer.GetPosition(i).y);
            //}

            drawOver.Invoke(scrPoints.ToArray());
            Destroy(gameObject);
        }

        //连续绘制线段
        while (index < LengthOfLineRenderer)
        {
            //两点确定一条直线，所以我们依次绘制点就可以形成线段了
            lineRenderer.SetPosition(index, position);
            index++;
        }
    }

  
}

