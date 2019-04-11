using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] State start;
    [SerializeField] State wrong;


    [SerializeField] Text undefined;
    [SerializeField] Image trident;
    [SerializeField] Image wave;
    [SerializeField] Image octo;
    List<int> arr = new List<int>();

    bool entering = false;


    [SerializeField] Image[] pos;
    Dictionary<string, int> posDict = new Dictionary<string, int>();


    [HideInInspector] string[] visited = new string[50];
    int counter;
    int i = 0;




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
        
        InitPos(pos);

       
        int i = posDict[start.name];
        Image startImg = pos[i];
        startImg.enabled = true;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        ManageState();
        ManageInventory();
        


    }

    private void ManageState()
    {
       
        if(!(visited.Contains(state.name)))
        {
            VisitState();
        }



        if (state.name == "Wrong")
        {
            textComponent.text = state.GetStoryText();
            WrongAnswer();
            return;
        }
        if (state.name.Contains("Win"))
        {
            Win();
            return;
        }

        if (state.name.Contains("END"))
        {
            End();
            return;
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

        if (state.name == "Cord08")
        {
            FinalDoor(state);
            return;
        }

        textComponent.text = state.GetStoryText();

        if(state.name == "Undefined")
        {
            state = curr;
            undefined.text =  "You can't go that way!";
        }

        if(state != curr)
        {
            UpdatePos(curr, state);
        }

        if(state.name == "EndGame08")
        {
            FinalDoor(state);
        }

        if (state.name == "Enter")
        {
            Enter();
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

    public void UpdatePos(State curr, State next)
    {
        int i = posDict[curr.name];
        Image currImage = pos[i];
        currImage.enabled = false;

        i = posDict[next.name];
        Image nextImage = pos[i];
        nextImage.enabled = true;
    }

    public void InitPos(Image[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                posDict[arr[i].name] = i;
                arr[i].enabled = false;
            }
        }
    }

    public void FinalDoor(State final)
    {

        int count = 0;
        if(octo.enabled)
        {
            count++;
        }
        if (wave.enabled)
        {
            count++;
        }
        if (trident.enabled)
        {
            count++;
        }

        if (count == 0)
        {
            textComponent.text = state.GetStoryText();
  

        }

        if (count == 1)
        {
            textComponent.text = state.GetStoryText() + "\nThe holes look similar in size to the stone you've picked up.";

        }

        if (count == 2)
        {
            textComponent.text = state.GetStoryText() + "\nMaybe if you just found the last stone....";
        }

        if (count == 3)
        {
            textComponent.text = state.GetStoryText() + "\n3 holes and 3 items in your inventory. Press SPACE";

        }

        if(Input.GetKeyDown(KeyCode.Space) && count == 3)
        {
           var states = state.NextStates();
           state = states[4];
        }

      
    }
    public void Enter()
 
    {
  
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            arr.Add(1);
            i++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            arr.Add(2);
            i++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            arr.Add(3);
            i++;
        }

        if(arr.Count == 3)
        {
            bool win = CheckAnswer(arr);
            if(win)
            {
                var states = state.NextStates();
                state = states[1];
            }
            else
            {
                var states = state.NextStates();
                state = states[0];
                
            }
        }
    }

    public bool CheckAnswer(List<int> ans)
    {
        List<int> correct = new List<int>(){ 2, 1, 3 };
        bool correctAns = true;
        for (int i = 0; i < correct.Count; i++)
        {
            if (ans[i] != correct[i])
            {
                Debug.Log(ans[i]);
                Debug.Log(correct[i]);
                correctAns = false;
            }
        }

        return correctAns;
    }

    public void WrongAnswer()
    {
        Debug.Log(state.name);

        arr = new List<int>();
        var nextStates = state.NextStates();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("here");

            state = nextStates[0];
            i = 0;

        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            state = nextStates[1];
            i = 0;
        }
 

    }

    public void Win()
    {
        textComponent.text = state.GetStoryText();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var states = state.NextStates();
            state = states[0];
        }
    }

    public void End()
    {
        textComponent.text = state.GetStoryText();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }

  


}
