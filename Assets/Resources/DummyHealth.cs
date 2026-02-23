using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DummyHealth : MonoBehaviour
{
    public float maxHealth = 100f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Banana")
        {
            Debug.Log("banana");
            maxHealth -= 5;
            Debug.Log(maxHealth);
        }

        if (collision.gameObject.tag == "Grape")
        {
            Debug.Log("grape");
            maxHealth -= 40;
            Debug.Log(maxHealth);

        }
        if (maxHealth == 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
