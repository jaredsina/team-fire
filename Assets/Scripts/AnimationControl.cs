using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        float currentSpeed = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", currentSpeed);

    }
}
