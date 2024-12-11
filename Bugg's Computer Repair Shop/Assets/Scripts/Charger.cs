using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : WorldObject
{
    [SerializeField] public Collider2D charger;
    [SerializeField] public Collider2D chargedPart;
    public float interval = 0.5f;

    private bool beingUsed = false;
    private bool isPowerMeterActive = false;
    private bool isIndicatorActive = false;
    private float powerMeterValue = 0f;
    private bool increasing = true;

    [SerializeField] public GameObject powerMeterSprite; // The power meter bar background
    [SerializeField] public GameObject indicatorSprite;
    [SerializeField] public Transform powerIndicator; // The visual indicator (slider handle or cursor)
    [SerializeField] public Transform powerBarStart; // Start position of the bar
    [SerializeField] public Transform powerBarEnd; // End position of the bar

    public float timingThreshold = 0.2f; // Range for hitting the center
    public float centerPoint = 0.5f; // Center value of the meter

    private void Start()
    {
        powerMeterSprite.SetActive(false); // Hide initially
        indicatorSprite.SetActive(false);
        powerIndicator.gameObject.SetActive(false);
        UpdatePowerMeterVisual();
    }

    private void Update()
    {
        if (isPowerMeterActive)
        {
            UpdatePowerMeter();
        }

        // Check for right-click release to stop the meter
        if (Input.GetMouseButtonUp(1) && isPowerMeterActive)
        {
            CheckPowerMeterSuccess();
            DeactivatePowerMeter();
        }
    }

    public override void SetDown()
    {
        base.SetDown();
        beingUsed = false;
        DeactivatePowerMeter();
    }

    public override void GetInput(Player player)
    {
        if (player.MBPressed[1] && !beingUsed)
        {
            // Right-click pressed
            beingUsed = true;

            // Ensure the charger is in contact with the part
            if (chargedPart != null && charger.bounds.Intersects(chargedPart.bounds))
            {
                ActivatePowerMeter();
            }
            else
            {
                Debug.Log("Charger is not properly aligned with the part.");
                beingUsed = false;
            }
        }
        else if (!player.MBPressed[1])
        {
            beingUsed = false; // Reset when the button is released
        }
    }

    private void ActivatePowerMeter()
    {
        if (!isPowerMeterActive)
        {
            isPowerMeterActive = true;
            powerMeterValue = 0f;
            increasing = true;

            // Show the power meter and indicator sprite
            powerMeterSprite.SetActive(true);
            indicatorSprite.SetActive(true);

            // Ensure the power indicator is shown when activated
            powerIndicator.gameObject.SetActive(true);

            // Call the visual update method to synchronize the meter
            UpdatePowerMeterVisual();
        }
    }

    private void UpdatePowerMeter()
    {
        // Oscillate the power meter value
        if (increasing)
        {
            powerMeterValue += Time.deltaTime / interval;
            if (powerMeterValue >= 1f)
            {
                powerMeterValue = 1f;
                increasing = false;
            }
        }
        else
        {
            powerMeterValue -= Time.deltaTime / interval;
            if (powerMeterValue <= 0f)
            {
                powerMeterValue = 0f;
                increasing = true;
            }
        }

        // Update the visual representation
        UpdatePowerMeterVisual();
    }

    private void UpdatePowerMeterVisual()
    {
        if (powerIndicator != null && powerBarStart != null && powerBarEnd != null)
        {
            // Interpolate the position of the indicator between the start and end of the bar
            powerIndicator.position = Vector3.Lerp(
                powerBarStart.position,
                powerBarEnd.position,
                powerMeterValue
            );
        }
    }

    private void CheckPowerMeterSuccess()
    {
        // Determine success or failure
        if (Mathf.Abs(powerMeterValue - centerPoint) <= timingThreshold)
        {
            Debug.Log("Perfect Charge!");
            // Logic for a successful charge can go here
        }
        else
        {
            Debug.Log("Failed Charge!");
            // Logic for a failed charge can go here
        }
    }

    private void DeactivatePowerMeter()
    {
        isPowerMeterActive = false;
        isIndicatorActive = false;
        powerMeterSprite.SetActive(false); // Hide the power meter
        indicatorSprite.SetActive(false);
        powerIndicator.gameObject.SetActive(false);
    }
}
