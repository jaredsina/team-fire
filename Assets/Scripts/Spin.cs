using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinspeed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up,spinspeed*Time.deltaTime,Space.World);
    }
}
