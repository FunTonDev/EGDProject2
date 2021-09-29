using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public AudioSource navSource;
    public AudioClip choiceClip;
    public AudioClip confirmClip;

    public int toSwitch;
    public Text backStory;


    public void MouseAudioTrigger(AudioClip tClip)
    {
        navSource.PlayOneShot(tClip);
    }

    public void sceneSwitch(int index)
    {
        switch (index)
        {
            case 0:
                SceneManager.LoadScene("House");
                break;
            case 1:
                SceneManager.LoadScene("DriveThru");
                break;
            case 2:
                SceneManager.LoadScene("Street");
                break;
            case 3:
                SceneManager.LoadScene("TitleScreen");
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toSwitch == 0)
        {
            backStory.text = "The sounds of an alarm clock wake you up.\n\n" +
            "(Bzzzzzzzt Bzzzzzt Bzzzzzt)\n\n" +
            "“Good morning, time to wake up for another fabulous Monday!” says the alarm clock.\n" +
            "* click *\n" + "The alarm clock gets shut off.You check your phone, seeing a message from your boss, " +
            "saying “John Gene Eric, you better get your ass to work!” Ugh, just another Monday, you think to yourself." +
            "You put on some tunes based on the mood.Time to get to work!";
        }
        else if (toSwitch == 1)
        {
            backStory.text = "As you leave the house, the paperboy hocks the weekly paper at your face. \n“Hot off the press!” " +
                "the boy says.\n Oh look, some new Wack Donald’s coupons! Guess we know where we're going for lunch.";
        }
        else if (toSwitch == 2)
        {
            backStory.text = "Oh shoot, I’m cutting it close, hopefully I can beat the morning traffic.";
        }
        else if (toSwitch == 3)
        {
            backStory.text = "You made it to work on Time!\n\nGAME COMPLETED";
        }
    }
}
