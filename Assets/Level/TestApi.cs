using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class TestApi : MonoBehaviour {
    public UICanvans canvas;
    public GameObject mainPanel;
    private void Awake()
    {
        var g1 = GameObject.Instantiate(mainPanel);
        var g2 = GameObject.Instantiate(mainPanel);
        Debug.Log("g0" + mainPanel.GetInstanceID());
        Debug.Log("g1" + g1.GetInstanceID());
        Debug.Log("g2" + g2.GetInstanceID());
        //Ass.Instance.GetPrefab("Panel", delegate (GameObject go)
        //{
        //     mainPanel = GameObject.Instantiate(go);
        //});
    }
    // Use this for initialization
    void Start ()
    {
        //TTT();
    }
    static void TTT()
    {
        var url = "http://img.cyol.com/img/news/attachement/jpg/site2/20170327/IMG484d7ea271d544057911920.jpg";
        var len = GetHttpLength(url);
    }

    static long GetHttpLength(string url)
    {
        var length = (long)0;
        try
        {
            var req = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            req.Method = "HEAD";
            req.Timeout = 5000;
            var res = (HttpWebResponse)req.GetResponse();
            if (res.StatusCode == HttpStatusCode.OK)
            {
                length = res.ContentLength;
            }

            res.Close();
            return length;
        }
        catch (WebException wex)
        {
            return 0;
        }
    }
    // Update is called once per frame
    void Update () {
        //Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);
        //if (mainPanel == null) return;
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    GameObject main = GameObject.Instantiate(mainPanel);
        //    canvas.OpenGui(main.GetComponent<GuiBase>());
        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    canvas.CloseCurrentGui();
        //}
    }
}
