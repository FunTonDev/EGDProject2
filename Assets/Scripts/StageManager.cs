using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    //Which stage the player is playing through
    public int stageNum = 0;
    //Index of the task
    public int taskNum = 0;
    //Index of current action
    public int actionNum = 0;
    //What task does the player have to do
    public string currentTask;
    //List of tasks that need to be done
    public List<string> tasks;
    //List of actions that can be done
    public List<string> actions;
    //Textbox for current task
    public Text taskText;
    //Textbox for chaos button
    public Text chaosText;
    //Textbox for interactions
    public Text interactText;
    //Actual button
    public GameObject interacting;
    //Whether the game is in chaos mode or not
    public bool chaos = false;
    //Player got keys
    public bool gotKeys;
    //Player got lunch
    public bool gotLunch;

    //Make interact button glow after interacting (and destroy item if it is not the door)
    public IEnumerator GlowButton(GameObject item)
    {
        interacting.GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        interacting.GetComponent<Image>().color = new Color(1.0f, 1.0f, 0.0f);
        yield return new WaitForSeconds(0.0f);
        interacting.SetActive(false);

        if (item.GetComponent<Interactable>().itemName != "Door")
            Destroy(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        tasks = new List<string>();
        tasks.Add("Get out of Bed");
        tasks.Add("Find your keys");
        tasks.Add("Get to the car");
        tasks.Add("Drive to WackDonalds");
        tasks.Add("Pay for your food at the window");
        tasks.Add("Head to work");
        tasks.Add("Make your way through traffic");

        actions = new List<string>();
        actions.Add("Flip");
        actions.Add("Bat");
        actions.Add("Push");
        actions.Add("Jump");

        if (SceneManager.GetActiveScene().name == "House")
        {
            stageNum = 0;
            taskNum = 0;
            actionNum = 0;
        }
        else if (SceneManager.GetActiveScene().name == "DriveThru")
        {
            stageNum = 1;
            taskNum = 4;
            actionNum = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Street")
        {
            stageNum = 2;
            taskNum = 6;
            actionNum = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        taskText.text = tasks[taskNum];
        //If any input is detected, move out of bed (rotation of player will be sideways, so set to normal after
        if (taskNum == 0 && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            taskNum += 1;
        }
        if (chaos)
        {
            chaosText.text = actions[actionNum] + " with E";
        }
        else
        {
            chaosText.text = "Press SPACE for something NEW";
        }
    }
}
