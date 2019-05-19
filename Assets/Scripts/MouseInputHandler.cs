using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace com.paintpuzzle
{
    public class MouseInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {

        private Vector3 _dragStartPosition;
        private Vector3 _dragUpdatePosition;
        private Vector3 _dragEndPosition;
        public static Action<Vector3,Vector3> OnDragPreview;
        public static Action<Vector3,Vector3> OnDragFinished;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPosition = eventData.pointerCurrentRaycast.worldPosition;
            Debug.Log($"eventData {eventData.pointerCurrentRaycast.worldPosition}");
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragUpdatePosition = eventData.pointerCurrentRaycast.worldPosition;
            OnDragPreview?.Invoke(_dragStartPosition,_dragUpdatePosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragEndPosition = eventData.pointerCurrentRaycast.worldPosition;
            OnDragFinished?.Invoke(_dragStartPosition,_dragEndPosition);
            Debug.Log($"eventData {eventData.pointerCurrentRaycast.worldPosition}");
        }
    }
    
}
