using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform gun;
    public LayerMask whatIsGrapple;
    public LineRenderer lineRenderer;

    [Header("Grappling")]
    public float maxDistance;
    public float grappleDelay;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grappleCD;
    private float grappleCDTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse0;

    private bool grappled;

    private void Start()
    {
        // movement = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) { startGrapple(); }

        if (grappleCDTimer > 0) { grappleCDTimer -= Time.deltaTime; }
    }

    private void LateUpdate()
    {
        if (grappled) { lineRenderer.SetPosition(0, gun.position); }
    }

    private void startGrapple()
    {
        if (grappleCDTimer > 0) { return; }

        grappled = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, whatIsGrapple))
        {
            grapplePoint = hit.point;

            Invoke(nameof(executeGrapple), grappleDelay);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxDistance;

            Invoke(nameof(stopGrapple), grappleDelay);
        }

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(1, grapplePoint);
    }

    private void executeGrapple()
    {
    }

    private void stopGrapple()
    {
        grappled = false;

        grappleCDTimer = grappleCD;

        lineRenderer.enabled = false;
    }
}
