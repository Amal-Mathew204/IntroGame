using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Transform pickup; // First pickup block
    public Transform pickup1; // Second pickup block
    public Transform pickup2; // Third pickup block
    public Transform pickup3; // Fourth pickup block
    public Transform pickup4; // Fifth pickup block

    public TextMeshProUGUI distanceText; // Text UI element for displaying the closest distance

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        UpdateClosestPickup();
    }

    void UpdateClosestPickup()
    {
        Transform closestPickup = null;
        float closestDistance = Mathf.Infinity;

        // Check each pickup individually
        closestPickup = CheckClosestPickup(pickup, closestPickup, ref closestDistance);
        closestPickup = CheckClosestPickup(pickup1, closestPickup, ref closestDistance);
        closestPickup = CheckClosestPickup(pickup2, closestPickup, ref closestDistance);
        closestPickup = CheckClosestPickup(pickup3, closestPickup, ref closestDistance);
        closestPickup = CheckClosestPickup(pickup4, closestPickup, ref closestDistance);

        if (closestPickup != null)
        {
            // Update the distance label
            distanceText.text = "Distance to closest pickup: " + closestDistance.ToString("F2");

            // Draw a line from the player to the closest pickup
            lineRenderer.SetPosition(0, player.position);
            lineRenderer.SetPosition(1, closestPickup.position);

            // Highlight the closest pickup in blue and reset others to white
            SetPickupColor(pickup, closestPickup);
            SetPickupColor(pickup1, closestPickup);
            SetPickupColor(pickup2, closestPickup);
            SetPickupColor(pickup3, closestPickup);
            SetPickupColor(pickup4, closestPickup);
        }
    }

    // Function to check if a pickup is closer than the current closest pickup
    Transform CheckClosestPickup(Transform pickup, Transform currentClosest, ref float closestDistance)
    {
        if (pickup.gameObject.activeSelf) // Check only active pickups
        {
            float distance = Vector3.Distance(player.position, pickup.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                return pickup;
            }
        }
        return currentClosest;
    }

    // Function to set the color of the pickups
    void SetPickupColor(Transform pickup, Transform closestPickup)
    {
        if (pickup == closestPickup)
        {
            pickup.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            pickup.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
