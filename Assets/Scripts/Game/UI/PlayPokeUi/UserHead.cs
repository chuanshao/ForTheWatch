using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UserHead : MonoBehaviour , IUEventListener
{

    public bool EventHandler(string eventType,
                      UEventData eventData,
                      GameObject eventGameObject)
    {
        if (eventType == "touch") {

        }
        return false;
    }
    public Image Head;
    public void SetHeadUrl(string HeadUrl) {

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
