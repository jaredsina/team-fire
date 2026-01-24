using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private Renderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && renderer.enabled){
            renderer.enabled = false;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(15f);

        renderer.enabled = true;
    }
}
