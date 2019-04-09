using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Driver : MonoBehaviour
{

    [HideInInspector] public RoomNav roomNav;
    List<string> actionLog = new List<string>();
    public Text displayText;
    // Start is called before the first frame update
    void Awake()
    {
        roomNav = GetComponent<RoomNav>();
    }
    
    public void DisplayLoggedText()
    {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    private void Start()
    {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayRoomText()
    {
        string combinedText = roomNav.curr.description + "\n";
        LogStringWithReturn(combinedText);
    }

    public void LogStringWithReturn(string stringToAdd)
    {
        actionLog.Add(stringToAdd + "\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
