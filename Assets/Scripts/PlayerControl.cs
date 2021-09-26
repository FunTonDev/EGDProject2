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

    public Rigidbody carBody;

    public float fowardAccel = 8f, reverseAccel = 4f, maxSpeed = 30, turnStrength = 180, gravityForce = 10f;
    public float speedInput, turnInput;

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
        head.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        if (!carMode)
        {
            head.GetComponent<FirstPersonCamera>().notAble = false;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            playerDirection = new Vector3(horizontal, 0, vertical);
            if (!carMode)
                playerDirection = Camera.main.transform.TransformDirection(playerDirection);

            transform.Translate(new Vector3(playerDirection.x, 0, playerDirection.z) * (playerSpeed * Time.deltaTime));
        }
        else
        {
            head.GetComponent<FirstPersonCamera>().notAble = true;
            speedInput = 0f;
            if (Input.GetAxis("Vertical") > 0)
            {
                speedInput = Input.GetAxis("Vertical") * fowardAccel * 10f;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                speedInput = Input.GetAxis("Vertical") * reverseAccel * 10f;
            }

            turnInput = Input.GetAxis("Horizontal");

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            head.transform.rotation = transform.rotation;
            transform.position = carBody.transform.position;
        }
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

            stagie.GetComponent<StageManager>().StartCoroutine(stagie.GetComponent<StageManager>().GlowButton(inter));
            if (inter.GetComponent<Interactable>().itemName == "Keys")
            {
                //Pick up keys and move to the door and/or get more stuff
            }
            else if (inter.GetComponent<Interactable>().itemName == "Bag")
            {
                //Pick up bag from drive thru and move on
            }
            else if (inter.GetComponent<Interactable>().itemName == "Door")
            {
                //Move outside/ to drivethru scene
            }
        }
        //Perform current chaos ability (check stage manager for index/text
        else if (Input.GetButtonDown("Fire1") && stagie.GetComponent<StageManager>().chaos)
        {
            //If in car, jump
            if (carMode && carBody.velocity.y == 0)
            {
                Debug.Log("Do the jump");
                carBody.AddForce(transform.up * 500);
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (carBody.velocity.y == 0)
        {
            carBody.drag = 5;
            if (Mathf.Abs(speedInput) > 0 && carMode)
            {
                carBody.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            carBody.drag = 0.1f;
            carBody.AddForce(transform.up * -gravityForce);
        }
    }
}
