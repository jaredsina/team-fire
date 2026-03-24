using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  

    public CharacterController controller;    
    public Animator anim;
    //[SerializeField] private InputActionProperty moveAction;

    public float walkSpeed;
    public float jumpForce;
    public float gravity;
    public Vector3 velocity;
    private bool isGrounded;
    private float rotationSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.isGrounded;


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(0f, horizontalInput * rotationSpeed * Time.deltaTime, 0f);


        //Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        Vector3 direction = transform.forward * verticalInput;
        controller.Move(direction * walkSpeed * Time.deltaTime);

        //float SpaceInput = Input.GetAxis("Jump");
        if(direction.magnitude > 0.1f && isGrounded)
        {
            anim.SetBool("Walking", true);

        }
        else
        {
            anim.SetBool("Walking", false);
        }
        
        if (isGrounded){

            // maybe this
            anim.SetBool("isJumping", false);
            anim.SetBool("isBreathing", true);
            


            if (velocity.y < 0)
            {
                
                velocity.y = -2f;
            }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
         {
            // Jump
            anim.SetBool("isJumping", true);
            anim.SetBool("Walking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isBreathing", false);
            isGrounded = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            //Sprint
            //Debug.Log(isGrounded);
            walkSpeed = 3f;
            anim.SetBool("isRunning", true);

            anim.SetBool("Walking", false);
        }
            else
            {
                walkSpeed = 1f;
                anim.SetBool("isRunning", false);


            }



        //float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed : runSpeed;
            //Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
            //transform.position += move * currentSpeed * Time.deltaTime;
        }
        //else
        //{
        //    anim.SetBool("isJumping", true);
        //}

        //Debug.Log("Horizontal Input: " + horizontalInput + " Vertical Input: " + verticalInput);

        //Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);

        //Debug.Log("Movement Vector: " + movement);

        //transform.position += movement * speed * Time.deltaTime;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
}

//make the changes  to the code and then add summary, then commit, then after commit push into remote, then pull request for dev
//once the changes are in dev, delete current branch, and make a new branch from updated dev, and then make the player walk in main scene