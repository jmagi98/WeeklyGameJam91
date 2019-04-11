using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class Game : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] State start;

    [SerializeField] Text undefined;
    [SerializeField] Image trident;
    [SerializeField] Image wave;
    [SerializeField] Image octo;

    [HideInInspector] string[] visited = new string[50];
    int counter;



    State state;
    // Start is called before the first frame update
    void Start()
    {

        trident.enabled = false;
        wave.enabled = false;
        octo.enabled = false;

        state = start;
        textComponent.text = state.GetStoryText();
        undefined.text = "";

        visited[0] = start.name;
        counter = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
        ManageInventory();
    }

    private void ManageState()
    {
        if(!(visited.Contains(state.name)))
        {
            VisitState();
            Debug.Log(counter);
        }
        var nextState = state.NextStates();
        var curr = state;
        // North
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            state = nextState[0];
            undefined.text = "";
        }

        // South
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            state = nextState[1];
            undefined.text = "";

        }

        //East
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            state = nextState[2];
            undefined.text = "";

        }

        //West
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            state = nextState[3];
            undefined.text = "";

        }


        textComponent.text = state.GetStoryText();

        if(state.name == "Undefined")
        {
            state = curr;
            undefined.text =  "You can't go that way!";
        }

    }

    public void VisitState()
    {
        visited[counter] = state.name;
        counter++;
    }

    public void ManageInventory()
    {
        if(visited.Contains("Cord11"))
        {
            trident.enabled = true;
        }

        if (visited.Contains("Cord96"))
        {
            wave.enabled = true;
        }

        if (visited.Contains("Cord74"))
        {
            octo.enabled = true;
        }
    }

    
}
