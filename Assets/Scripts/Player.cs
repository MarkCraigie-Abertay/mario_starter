using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// variables taken from CharacterController.Move example script
	// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
	public float speed = 6.0F;
	public float jumpSpeed = 14.0F;
	public float gravity = 20.0F;
    private float previousPos = 0;
	private Vector3 moveDirection = Vector3.zero;
    private bool blockScore = true; // Makes sure the player can only increase score once from a jump

    public int Lives = 3; // number of lives the player hs
    public int Score = 0; //  The score the player currently has

	Vector3 start_position; // start position of the player


	void Start()
	{
		// record the start position of the player
		start_position = transform.position;
	}

	public void Reset()
	{
		// reset the player position to the start position
		transform.position = start_position;
	}

	void Update()
	{
		// get the character controller attached to the player game object
		CharacterController controller = GetComponent<CharacterController>();


		// check to see if the player is on the ground
		if (controller.isGrounded) 
		{
			// set the movement direction based on user input and the desired speed
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;

			// check to see if the player should jump
			if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

            // Resets boolean to allow the player to get score from block
            blockScore = true;
        }

        //check if the player is in the air
        if (controller.isGrounded == false)
        {
            if (previousPos == gameObject.transform.position.y)
                moveDirection.y = 0;

            previousPos = gameObject.transform.position.y;
        }

		// apply gravity to movement direction
		moveDirection.y -= gravity * Time.deltaTime;

		// make the call to move the character controller
		controller.Move(moveDirection * Time.deltaTime);
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check that the player has hit a platform
        if (hit.collider.gameObject.CompareTag("Platform"))
        {
            // If the player is allowed to hit a platform and get score
            if (blockScore == true)
            {
                // If the player has hit the platform from the bottom
                if (hit.collider.gameObject.transform.position.y > gameObject.transform.position.y + gameObject.GetComponent<CapsuleCollider>().height - 0.2)
                {
                    // Stop the player from gaining anymore score from platforms while in the air
                    blockScore = false;

                    // Increase the players score
                    Score = Score + 200;
                }
            }
        }
    }
}