using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MouseVisibilityController : MonoBehaviour
{
    public static MouseVisibilityController instance;

    bool mouseEnabled = false;
    MouseLook cameraMove;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        cameraMove = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();
    }

    void Start()
    {
        mouseEnabled = false;
        DisableMouse();
    }

    public void DisableMouse()
    {
        if (!mouseEnabled)
        {
            return;
        }
        {
            cameraMove.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            mouseEnabled = false;
    }
}

    public void EnableMouse()
    {
        if (mouseEnabled)
            return;
        else
        {
            cameraMove.enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            mouseEnabled = true;
        }
    }
    

}
