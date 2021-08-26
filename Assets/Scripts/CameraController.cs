using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform cameraTransform;

    public Transform Target;

    public float Smooth = 0.8f;

    public Vector3 Offset = new Vector3(0,0,-10f);

    private void Awake() {
        cameraTransform = GetComponent<Camera>().transform;

        Target = GameObject.FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate() {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, Target.position + Offset, Smooth);
    }

}
