using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform cameraAnchor;
    public float smoothSpeed = 100f;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraAnchor.position, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraAnchor.rotation, smoothSpeed * Time.deltaTime);
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
