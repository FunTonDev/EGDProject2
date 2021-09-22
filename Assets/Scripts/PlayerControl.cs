using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerControl player;
    public FirstPersonCamera head;
    public GameObject stagie;

    public float playerSpeed;
    public Vector3 playerDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
        stagie = GameObject.FindGameObjectWithTag("StageManager");
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

        if (Input.GetButtonDown("Jump"))
        {
            stagie.GetComponent<StageManager>().chaos = !stagie.GetComponent<StageManager>().chaos;
            if (stagie.GetComponent<StageManager>().chaos)
            {
                
            }
            else
            {

            }
        }
    }
}
