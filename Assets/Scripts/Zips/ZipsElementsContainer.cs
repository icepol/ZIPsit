using pixelook;
using UnityEngine;

public class ZipsElementsContainer : MonoBehaviour
{
    [SerializeField] private float zipsElementSize = 0.5f;
    [SerializeField] private float zipsItDuration = 0.25f;

    private float _nextPosition;

    private bool _isMoving;
    private Vector3 _zipsEndPosition;
    private Vector3 _currentVelocity;

    private void Update()
    {
        if (_isMoving)
            ZipsMovement();
    }

    public void AddElement(ZipsElement element)
    {
        element.transform.SetParent(transform);
        element.transform.localPosition = new Vector3(0, 0, _nextPosition);
        element.transform.rotation = transform.rotation;

        _nextPosition += zipsElementSize;
    }

    public void ZipsIt()
    {
        if (_isMoving) return;
        
        _zipsEndPosition = transform.localPosition - Vector3.forward * zipsElementSize * 0.5f;
        
        _isMoving = true;
        
        EventManager.TriggerEvent(Events.ZIPS_MOVE_STARTED);
    }

    private void ZipsMovement()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.localPosition, _zipsEndPosition, ref _currentVelocity, zipsItDuration);
        
        transform.localPosition = targetPosition;

        if (Vector3.Distance(targetPosition, _zipsEndPosition) > 0.005f) return;
        
        transform.localPosition = _zipsEndPosition;
        _isMoving = false;
        
        EventManager.TriggerEvent(Events.ZIPS_MOVE_FINISHED);
    }
}
