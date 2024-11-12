using System.Collections;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public LayerMask grappleLayer; // Set the layer of objects you can grapple to
    public Transform hook; // Reference to the hook GameObject
    public LineRenderer lineRenderer; // LineRenderer for visualizing the rope
    public float maxDistance = 10f; // Maximum grappling distance
    public float pullSpeed = 5f; // Speed of the player pulling toward the hook

    private Rigidbody2D rb;
    private Vector2 grapplePoint;
    private bool isGrappling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Launch the grappling hook on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }

        // Stop grappling if hook is attached
        if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            StopGrapple();
        }

        if (isGrappling)
        {
            // Update the line renderer to show the grappling rope
            lineRenderer.SetPosition(0, hook.position);
            lineRenderer.SetPosition(1, grapplePoint);

            // Move the player toward the grapple point
            Vector2 direction = (grapplePoint - (Vector2)transform.position).normalized;
            rb.linearVelocity = direction * pullSpeed;

            // Stop grappling if player reaches close to the grapple point
            if (Vector2.Distance(transform.position, grapplePoint) < 0.5f)
            {
                StopGrapple();
            }
        }
    }

    void StartGrapple()
    {
        Debug.Log("Start Grapple");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grappleLayer);

        if (hit.collider != null)
        {
            isGrappling = true;
            grapplePoint = hit.point;

            // Enable line renderer and set positions
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, hook.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.enabled = false;
        rb.linearVelocity = Vector2.zero;
    }
}
