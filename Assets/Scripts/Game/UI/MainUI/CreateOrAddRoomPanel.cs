using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CreateOrAddRoomPanel : BaseWindow
{
    public Button CreateRoom;
    public Button AddButton;

    // Use this for initialization
    void Start()
    {
        AddButton.onClick.AddListener(delegate ()//show Add Room Window
        {

        });
        CreateRoom.onClick.AddListener(delegate ()
        {
            
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
