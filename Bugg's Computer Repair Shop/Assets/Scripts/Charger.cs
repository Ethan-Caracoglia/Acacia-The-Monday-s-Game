using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : WorldObject
{
    [SerializeField] public Collider2D charger;
    [SerializeField] public Collider2D chargedPart;
    public float interval = 0.5f;

    private bool beingUsed = false;
    private bool isPowerMeterActive = false;
    private float powerMeterValue = 0f;
    private bool increasing = true;
    [SerializeField] public GameObject powerMeterSlider;
    public float timingThreshold = 0.2f; // Range for hitting the center
    public float centerPoint = 0.5f; // Center value of the meter
    private bool hasProcessedClick = false; // Tracks if a right-click has been processed

    private void Start()
    {
        powerMeterSlider.gameObject.SetActive(false); // Hide initially
    }

    private void Update()
    {
        if (beingUsed)
        {
            HeldUse();
        }

        if (isPowerMeterActive)
        {
            UpdatePowerMeter();
        }
    }

    public override void SetDown()
    {
        base.SetDown();

        beingUsed = false;
        isPowerMeterActive = false;
        powerMeterSlider.gameObject.SetActive(false); // Hide power meter
        hasProcessedClick = false; // Reset click tracking
    }

    public override void GetInput(Player player)
    {
        // Activate only on a single right-click while holding the charger
        if (player.MBPressed[1] && !hasProcessedClick)
        {
            beingUsed = true;
            hasProcessedClick = true; // Mark click as processed
            ActivatePowerMeter();
        }
        else if (!player.MBPressed[1])
        {
            hasProcessedClick = false; // Reset when button is released
        }
    }

    protected void HeldUse()
    {
        if (chargedPart != null && charger.bounds.Intersects(chargedPart.bounds))
        {
            Debug.Log("Charging in progress...");
            ActivatePowerMeter();
        }
    }

    private void ActivatePowerMeter()
    {
        if (!isPowerMeterActive)
        {
            isPowerMeterActive = true;
            powerMeterValue = 0f;
            increasing = true;
            powerMeterSlider.gameObject.SetActive(true); // Show power meter
        }
    }

    private void UpdatePowerMeter()
    {
        // Oscillate the power meter
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

        // Update the UI Slider
        //powerMeterSlider.value = powerMeterValue;

        // Check for player input to stop the meter
        if (Input.GetMouseButtonDown(1)) // Right-click to stop the meter
        {
            CheckPowerMeterSuccess();
        }
    }

    private void CheckPowerMeterSuccess()
    {
        //isPowerMeterActive = false;
        //powerMeterSlider.gameObject.SetActive(false); // Hide power meter

        // Check if the player hit the center
        if (Mathf.Abs(powerMeterValue - centerPoint) <= timingThreshold)
        {
            Debug.Log("Perfect Charge!");
            // Add logic for a successful charge
        }
        else
        {
            Debug.Log("Failed Charge!");
            // Add logic for failure
        }
    }
}
