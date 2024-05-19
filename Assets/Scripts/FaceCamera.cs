using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform CameraTransform
        => _cam.transform;
    
    public Vector3 offset = new(0, 180, 0);
    private Vector3 _lastCameraPos;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        _lastCameraPos = CameraTransform.position;
        Rotate();
    }

    private void Update()
    {
        if (!GMgr.Instance.Loaded) return;
        
        if (_lastCameraPos != CameraTransform.position)
            Rotate();
    }
    
    private void Rotate()
    {
        var camRot = CameraTransform.rotation;
        transform.LookAt(transform.position + camRot * Vector3.back, camRot * Vector3.up);
        transform.Rotate(offset.x, offset.y, offset.z);
    }
}