using UnityEngine;
using System.Collections;


public class Coin : MonoBehaviour
{
    private Renderer renderer;
    private GameObject gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<Renderer>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && renderer.enabled){
            renderer.enabled = false;
            gameManager.GetComponent<GoldScript>().ChangeGold(1);
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(15f);

        renderer.enabled = true;
    }
}
