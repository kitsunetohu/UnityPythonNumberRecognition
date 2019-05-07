using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManger : MonoBehaviour {

    public GameObject draw;

    void OnGUI()
    {
        GUILayout.Label("当前鼠标X轴位置：" + Input.mousePosition.x);
        GUILayout.Label("当前鼠标Y轴位置：" + Input.mousePosition.y);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(draw);
        }
		
	}
}
