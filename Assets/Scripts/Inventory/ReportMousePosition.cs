using UnityEngine;
using UnityEngine.InputSystem;
public class ReportMousePosition : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Debug.Log("A key was Pressed!");
        }

        //if (Gamepad.current.aButton.wasPressedThisFrame)
        //{
        //    Debug.Log("A button was Pressed");
        //}
    }
}
