using System;
using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
    private Vector3 dragOrigin;
    private Vector2 lastPoint;
    private float rotationAngle;

    // Update is called once per frame
    void Update() {
        if (gameObject == null) return;

        KeyboardDrag();
        MouseDrag();
        MouserRotate();
        TouchRotate();
        MouseZoom();
    }

    private void MouserRotate() {
        if (!Input.GetKey(KeyCode.LeftShift)) return;
        if (!(Input.GetAxis("Mouse ScrollWheel") < 0 || Input.GetAxis("Mouse ScrollWheel") > 0)) return;

        var angle = Input.GetAxis("Mouse ScrollWheel") < 0 ? 10f : -10f;
        rotationAngle += angle;
        Camera.main.transform.RotateAround(Camera.main.transform.position, Vector3.up, angle);
    }

    private void MouseZoom() {
        if (Input.GetKey(KeyCode.LeftShift)) return;

        //http://answers.unity3d.com/questions/20228/mouse-wheel-zoom.html
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            Camera.main.fieldOfView = Mathf.Min(Camera.main.fieldOfView + 1, 75.0f);
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            Camera.main.fieldOfView = Mathf.Max(Camera.main.fieldOfView - 1, 10.0f);
    }

    private void MouseDrag() {
        if (Input.touchCount == 1 || Input.touchCount == 3) return;

        if (Input.GetMouseButtonDown(0)) {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        var pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        var move = new Vector3(pos.x, 0, pos.y);

        move = Quaternion.AngleAxis(rotationAngle, Vector3.up) * move;
        transform.Translate(move, Space.World);
    }

    private void TouchRotate() {
        // From: http://answers.unity3d.com/questions/564558/dragging-camera-based-on-touch.html
        if (Input.touches.Length == 0) return;

        var t0 = Input.touches[0];
        if (t0.phase == TouchPhase.Began) {
            lastPoint = t0.position;
            return;
        }

        if (t0.phase != TouchPhase.Moved) return;

        var offset = t0.position.x - lastPoint.x;
        if (Input.touches.Length == 3) {
            //transform.Rotate(0, offset * 0.3f, 0);
            var angle = offset*0.3f;
            rotationAngle += angle;
            Camera.main.transform.RotateAround(Camera.main.transform.position, Vector3.up, angle);
        }
        lastPoint = t0.position;
    }

    private void KeyboardDrag() {
        Drag(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void Drag(float xAxisValue, float yAxisValue) {
        var move = new Vector3(xAxisValue, 0, yAxisValue);
        move = Quaternion.AngleAxis(rotationAngle, Vector3.up) * move;
        gameObject.transform.Translate(move);
    }
}
