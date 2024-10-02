using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMouse : MonoBehaviour
{
    private const int PIXELS_TO_UNITY_UNITY_UNITS_RATIO = 100;
    [SerializeField] Camera cam;
    float height;
    float width;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    public void UpdateMouse(InputAction.CallbackContext ctx)
    {
        // Vector that points to screen center from mouse pos zero
        Vector3 halfScreenSize = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Vector2 mousePos = ctx.ReadValue<Vector2>();
        float hWidth = width / 2;
        float hHeight = height / 2;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10)); 

         /* (Debug.Log(
            $"Mouse Position = {mousePos}\n" +
            $"Mouse Position in units = {mousePos / PIXELS_TO_UNITY_UNITY_UNITS_RATIO}\n" +
            $"HalfScreen vector = {halfScreenSize}\n" +
            $"Cursor Position = {transform.position}\n" + 
            $"Width and height = {width}, {height}");
            */
    }
}
