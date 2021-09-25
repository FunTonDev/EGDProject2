using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    //The player body object
    PlayerControl player;
    //The camera/head
    public FirstPersonCamera head;
    //Stage manager
    public GameObject stagie;
    //Bool to determine movement controls
    public bool carMode;
    //Speed the player moves at
    public float playerSpeed;
    //The direction the player is facing in
    public Vector3 playerDirection = Vector3.zero;
    //Filters for UI during the different modes
    public RawImage chaos_filter;
    public RawImage serenity_filter;
    //Interactable object
    public GameObject inter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            inter = other.gameObject;
            stagie.GetComponent<StageManager>().interactText.text = inter.GetComponent<Interactable>().useText;
            stagie.GetComponent<StageManager>().interacting.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            inter = null;
            stagie.GetComponent<StageManager>().interacting.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
        stagie = GameObject.FindGameObjectWithTag("StageManager");
        serenity_filter.enabled = true;
        chaos_filter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        head.transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerDirection = new Vector3(horizontal, 0, vertical);
        playerDirection = Camera.main.transform.TransformDirection(playerDirection);

        transform.Translate(new Vector3(playerDirection.x, 0, playerDirection.z) * (playerSpeed * Time.deltaTime));

        //If player presses the chaos button
        if (Input.GetButtonDown("Jump"))
        {
            stagie.GetComponent<StageManager>().chaos = !stagie.GetComponent<StageManager>().chaos;
            if (stagie.GetComponent<StageManager>().chaos)
            {
                Debug.Log("here1");
                chaos_filter.enabled = true;
                serenity_filter.enabled = false;
            }
            else
            {
                Debug.Log("here2");
                serenity_filter.enabled = true;
                chaos_filter.enabled = false;
            }
        }

        //Interact with item
        if (Input.GetButtonDown("Fire1") && inter != null)
        {
            if (inter.GetComponent<Interactable>().itemName != "Door")
            {
                stagie.GetComponent<StageManager>().StartCoroutine(stagie.GetComponent<StageManager>().GlowButton(inter));
                
            }
        }
        //Perform current chaos ability (check stage manager for index/text
        else if (Input.GetButtonDown("Fire1") && stagie.GetComponent<StageManager>().chaos)
        {
            
        }
    }
}
