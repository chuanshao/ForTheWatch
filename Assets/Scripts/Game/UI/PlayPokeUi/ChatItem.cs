using UnityEngine;
using System.Collections;

public class ChatItem : MonoBehaviour , IData , ISelect
{
    public WData data {
        get {
            return _data;
        }
        set {
            _data = value;
        }
    }
    public bool select
    {
        get
        {
            return _select;
        }
        set
        {
            _select = value;
        }
    }
  
    private WData _data;
    private bool _select;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
