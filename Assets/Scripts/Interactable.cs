using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //Name of this item
    public string itemName;
    //Text to display on collision enter
    public string useText;
    //Whether the player can interact or not
    public bool canDo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerObj")
        {
            canDo = true;
            Debug.Log("Can Interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerObj")
        {
            canDo = false;
            Debug.Log("Out of Range");
        }
    }

    public void onInteract()
    {

    }

}
