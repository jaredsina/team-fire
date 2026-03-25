using UnityEngine;
using UnityEngine.InputSystem;

public class GrapeThrowerFromStart : MonoBehaviour
{
    public enum ResetMode { OriginalPosition, InFrontOfCamera }

    [Header("References")]
    public Camera playerCamera;        // assign Main Camera or will try Camera.main
    public GameObject grapeObject;     // existing grape in scene (must have Rigidbody)

    [Header("Launch Settings")]
    public float launchSpeed = 10f;        // nominal speed used by ballistic solver
    public float maxTargetDistance = 40f;  // clamp how far a click can target
    public bool preferHighArc = false;     // false = use lower (shorter) arc

    [Header("Reset Settings")]
    public ResetMode resetMode = ResetMode.OriginalPosition;
    public Vector3 inFrontCameraOffset = new Vector3(0f, -0.2f, 0.6f); // local offset from camera when using InFrontOfCamera
    public float resetDelay = 4f;          // seconds before resetting for next throw

    private Rigidbody _grapeRb;
    private bool _launched = false;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    public ParticleSystem explosionParticles;

    void Start()
    {
        explosionParticles.Stop();
    }

    void OnCollisionEnter(Collision collision)
    {
        explosionParticles.Play();
    }
    
    private void Awake()
    {
        if (playerCamera == null) playerCamera = Camera.main;

        if (grapeObject == null)
        {
            Debug.LogWarning("GrapeThrowerFromStart: grapeObject not assigned.");
            return;
        }

        _grapeRb = grapeObject.GetComponent<Rigidbody>();
        if (_grapeRb == null)
        {
            Debug.LogWarning("GrapeThrowerFromStart: Rigidbody missing on grapeObject. Adding one automatically.");
            _grapeRb = grapeObject.AddComponent<Rigidbody>();
        }

        // Save the original transform so we can return to it later
        _originalPosition = grapeObject.transform.position;
        _originalRotation = grapeObject.transform.rotation;

        // Start kinematic so it doesn't fall before first throw (optional)
        _grapeRb.isKinematic = true;
    }

    void Update()
    {
        if (playerCamera == null || grapeObject == null || _grapeRb == null) return;

        // Prevent launching while already in flight
        if (_launched) return;

        // Only launch if the grape is at (or very near) its reset/original position.
        // This prevents launching while it's mid-air or not yet reset.
        if (Vector3.Distance(grapeObject.transform.position, _originalPosition) > 0.05f)
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = playerCamera.ScreenPointToRay(mousePos);

            // Visual debug
            Debug.DrawRay(ray.origin, ray.direction * 50f, Color.red, 2f);

            Vector3 target;
            if (Physics.Raycast(ray, out RaycastHit hit, 200f))
            {
                target = hit.point;
            }
            else
            {
                // If raycast misses, project to a horizontal plane at y = original Y (so clicks still work)
                Plane plane = new Plane(Vector3.up, new Vector3(0f, _originalPosition.y, 0f));
                if (plane.Raycast(ray, out float enter))
                    target = ray.GetPoint(enter);
                else
                    target = ray.origin + ray.direction * Mathf.Min(20f, maxTargetDistance);
            }

            // Clamp target distance so it never tries to reach absurdly far points
            Vector3 start = grapeObject.transform.position; // IMPORTANT: start from grape's current/original position
            Vector3 toTarget = target - start;
            float horizontalDist = new Vector3(toTarget.x, 0f, toTarget.z).magnitude;
            if (horizontalDist > maxTargetDistance)
            {
                Vector3 dir = new Vector3(toTarget.x, 0f, toTarget.z).normalized;
                target = start + dir * maxTargetDistance + Vector3.up * toTarget.y;
                Debug.Log("GrapeThrowerFromStart: target clamped to maxTargetDistance.");
            }

            // Ensure physics is enabled and launch from the grape's position (no teleport to camera)
            _grapeRb.isKinematic = false;
            _grapeRb.useGravity = true;

            bool launched = LaunchToTarget(start, target, launchSpeed, preferHighArc);
            Debug.Log("GrapeThrowerFromStart: Launch attempted = " + launched);
        }
    }

    bool LaunchToTarget(Vector3 start, Vector3 target, float speed, bool highArc)
    {
        Vector3 toTarget = target - start;
        Vector3 toTargetXZ = new Vector3(toTarget.x, 0f, toTarget.z);
        float x = toTargetXZ.magnitude;   // horizontal distance
        float y = toTarget.y;             // vertical difference
        float g = Mathf.Abs(Physics.gravity.y);

        if (x < 0.01f)
        {
            _grapeRb.linearVelocity = Vector3.up * Mathf.Min(5f, speed * 0.5f);
            _launched = true;
            StartCoroutine(ResetAfterSeconds(resetDelay));
            return true;
        }

        float v2 = speed * speed;
        float discriminant = v2 * v2 - g * (g * x * x + 2f * y * v2);

        if (discriminant >= 0f)
        {
            float sqrt = Mathf.Sqrt(discriminant);
            float numerator = highArc ? (v2 + sqrt) : (v2 - sqrt);
            float angle = Mathf.Atan(numerator / (g * x)); // radians

            Vector3 dirXZ = toTargetXZ.normalized;
            Vector3 velocity = dirXZ * (Mathf.Cos(angle) * speed) + Vector3.up * (Mathf.Sin(angle) * speed);

            _grapeRb.linearVelocity = velocity;
            _launched = true;
            StartCoroutine(ResetAfterSeconds(resetDelay));
            return true;
        }
        else
        {
            // Fallback time-based solver (always works)
            float fallbackTime = Mathf.Clamp(x / Mathf.Max(1f, speed * 0.8f), 0.5f, 4f);
            Vector3 vx = toTargetXZ / fallbackTime;
            float vy = (y + 0.5f * g * fallbackTime * fallbackTime) / fallbackTime;
            Vector3 velocity = vx + Vector3.up * vy;

            _grapeRb.linearVelocity = velocity;
            _launched = true;
            Debug.Log("GrapeThrowerFromStart: used fallback time-based solver.");
            StartCoroutine(ResetAfterSeconds(resetDelay));
            return true;
        }
    }

    System.Collections.IEnumerator ResetAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Stop motion
        _grapeRb.linearVelocity = Vector3.zero;
        _grapeRb.angularVelocity = Vector3.zero;
        _grapeRb.isKinematic = true;

        // Choose reset position
        if (resetMode == ResetMode.OriginalPosition)
        {
            grapeObject.transform.position = _originalPosition;
            grapeObject.transform.rotation = _originalRotation;
        }
        else // InFrontOfCamera
        {
            Vector3 worldOffset = playerCamera.transform.TransformVector(inFrontCameraOffset);
            grapeObject.transform.position = playerCamera.transform.position + worldOffset;
            grapeObject.transform.rotation = _originalRotation;
        }

        _launched = false;
        Debug.Log("GrapeThrowerFromStart: reset grape for next launch.");
    }
    
    
}