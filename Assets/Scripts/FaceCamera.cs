using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    
    public Vector3 offset = new(0, 180, 0);
    private Vector3 _lastCameraPos;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPos = _cameraTransform.position;
        Rotate();
    }

    private void Update()
    {
        if (_lastCameraPos != _cameraTransform.position)
            Rotate();
    }
    
    private void Rotate()
    {
        var camRot = _cameraTransform.rotation;
        transform.LookAt(transform.position + camRot * Vector3.back, camRot * Vector3.up);
        transform.Rotate(offset.x, offset.y, offset.z);
    }
}