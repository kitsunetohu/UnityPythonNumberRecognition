using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading;

public class NetManger : MonoBehaviour {



    Socket socket;

    public InputField hostInput;
    public InputField portInput;

    public Text recvText;
    public Text clientText;
    public Text test;

    public GameObject mainmenu;

    public  MyVector2 myVector2;

    const int BUFFER_SIZE = 1024;
    byte[] readBuff = new byte[BUFFER_SIZE];

    public void Connetion()
    {
        //Socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        string host = hostInput.text;
        int port = int.Parse(portInput.text);
        socket.Connect(host, port);
        clientText.text = "客户端地址" + socket.LocalEndPoint.ToString();

    }

    void send()
    {
        //Send
        string str = "exit";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        socket.Send(bytes);

        
    }

    void receive()
    {
        //Recv
        int count = socket.Receive(readBuff);
        string str = System.Text.Encoding.UTF8.GetString(readBuff, 0, count);
        //recvText.text = str;


        if (str == "5")
        {

            mainmenu.SetActive(true);
            mainmenu.GetComponent<Animation>().Play();
        }
        //Close
        socket.Close();
    }


    public void sendVectors(Vector2[] vector2s)
    {
        myVector2.X = new float[vector2s.Length];
        myVector2.Y = new float[vector2s.Length];
  
        for(int i=0;i<vector2s.Length;i++)
        {
            myVector2.X[i] = vector2s[i].x;
            myVector2.Y[i] = vector2s[i].y;        
        }
        var json= JsonUtility.ToJson(myVector2);
        
        Connetion();
        byte[] bytes = System.Text.Encoding.Default.GetBytes(json);
        socket.Send(bytes);

        Thread.Sleep(100);
        bytes = System.Text.Encoding.Default.GetBytes("exit");
        socket.Send(bytes);

        receive();
        
        

        test.text = json;
       
    }
}

[System.Serializable]
public class MyVector2
{
    public float[] X;
    public float[] Y;

}
