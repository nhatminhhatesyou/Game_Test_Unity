using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementScript : MonoBehaviour
{
    public Rigidbody player;
    public Transform groundCheck;

    public LayerMask ground;

    public float movementSpeed = 5f;
    public float jumpForce = 5f;

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        player.velocity = new Vector3(horizontalInput * movementSpeed, player.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
        }

        //If player fall down the platform, game over
        if (player.transform.position.y < -3.5)
            SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 2);
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }


}
