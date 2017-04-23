using System.Collections;

public class MainModel
{
    public UserData User = new UserData();

    private static MainModel _instance;
    public static MainModel Instance {
        get {
            if (_instance == null)
            {
                _instance = new MainModel();
            }
            return _instance;
        }
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
