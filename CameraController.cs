using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //UI Toggles
    public UI ui;

    // Camera View Transform Position
    // Publicly Known For Tile To Access
    public float xPos;
    public float yPos;
    public float zPos;

	public float xLimitMin;
	public float xLimitMax;
	public float yLimitMin;
	public float yLimitMax;

	private static int PAN_CAMERA_BORDER = 20;

    void Start ()
    {
        // No Default Text Should Display on Game Startup
        ui.statusCanvas.SetActive(false);

        // Script Object's Point of Origin
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;
    }

    // Update is Called Once per Frame
    void Update () {
        CameraViewUpdate();
    }

    // Pan Camera Based on Mouse Going OffScreen
    private void CameraViewUpdate()
    {
        // Modify as Needed
        float panSpeed = 0.1f;

        float mouseX  = Input.mousePosition.x;
        float mouseY  = Input.mousePosition.y;
        float screenX = Screen.width;
        float screenY = Screen.height;

        // Horizontal Pan
        if (mouseX > screenX - PAN_CAMERA_BORDER && xPos < xLimitMax)
        {
            xPos += panSpeed;
        }
		else if (mouseX < PAN_CAMERA_BORDER && xPos > xLimitMin)
        {
            xPos -= panSpeed;
        }

        // Vertical Pan
		if (mouseY > screenY - PAN_CAMERA_BORDER && yPos < yLimitMax)
        {
            yPos += panSpeed;
        }
		else if (mouseY < PAN_CAMERA_BORDER && yPos > yLimitMin)
        {
            yPos -= panSpeed;
        }

        transform.position = new Vector3(xPos, yPos, zPos);
    }

    public void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        style.richText = true;

        style.alignment = TextAnchor.UpperCenter;
        GUI.Box(new Rect(0, 0, Screen.width, PAN_CAMERA_BORDER), "");
        GUI.Box(new Rect(0, 0, Screen.width, PAN_CAMERA_BORDER), "<color=white>N</color>", style);

        style.alignment = TextAnchor.LowerCenter;
        GUI.Box(new Rect(0, Screen.height - PAN_CAMERA_BORDER, Screen.width, PAN_CAMERA_BORDER), "");
        GUI.Box(new Rect(0, Screen.height - PAN_CAMERA_BORDER, Screen.width, PAN_CAMERA_BORDER), "<color=white>S</color>", style);

        style.alignment = TextAnchor.MiddleLeft;
        GUI.Box(new Rect(0, 0, PAN_CAMERA_BORDER, Screen.height), "");
        GUI.Box(new Rect(0, 0, PAN_CAMERA_BORDER, Screen.height), "<color=white> W</color>", style);

        style.alignment = TextAnchor.MiddleRight;
        GUI.Box(new Rect(Screen.width - PAN_CAMERA_BORDER, 0, PAN_CAMERA_BORDER, Screen.height), "");
        GUI.Box(new Rect(Screen.width - PAN_CAMERA_BORDER, 0, PAN_CAMERA_BORDER, Screen.height), "<color=white>E </color>", style);
    }
}