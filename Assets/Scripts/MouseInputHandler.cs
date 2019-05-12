using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace com.paintpuzzle
{
    public class MouseInputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log($"Drag has started {eventData.button}");
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"Drag has ended {eventData.button}");
        }
    }
    
}
