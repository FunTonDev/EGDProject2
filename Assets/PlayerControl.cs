using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    PlayerControl player;
    public FirstPersonCamera head;

    public float playerSpeed;
    public Vector3 playerDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        head.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * (playerSpeed * Time.deltaTime));
    }
}
