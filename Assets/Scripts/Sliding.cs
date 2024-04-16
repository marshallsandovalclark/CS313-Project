using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    private Rigidbody rb;
    private PlayerMovement1 pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement1>();

        startYScale = player.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
        {
            startSlide();
        }

        if (Input.GetKeyUp(slideKey) && pm.sliding)
        {
            stopSlide();
        }
    }

    private void LateUpdate()
    {
        if (pm.sliding)
        {
            slidingMovement();
        }
    }

    private void startSlide()
    {
        pm.sliding = true;

        player.localScale = new Vector3(player.localScale.x, slideYScale, player.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void slidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (!pm.onSlope() || rb.velocity.y > -0.1)
        {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }
        else
        {
            rb.AddForce(pm.getSlopeAngle(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
        {
            stopSlide();
        }
    }

    private void stopSlide()
    {
        pm.sliding = false;

        player.localScale = new Vector3(player.localScale.x, startYScale, player.localScale.z);
    }
}
