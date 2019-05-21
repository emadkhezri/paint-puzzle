using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  com.paintpuzzle
{
    public class DragBox : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider _boxCollider;

        private void Start() {
            MouseInputHandler.OnDragFinished += OnDragFinished;
            MouseInputHandler.OnDragPreview += OnDragPreview;
        }

        private void OnDragFinished(Vector3 dragStart, Vector3 dragEnd)
        {
            Vector3 center = (dragStart+dragEnd)/2f;
            _boxCollider.center = center;
            Vector3 signedSize = dragEnd-dragStart;
            _boxCollider.size = new Vector3(Mathf.Abs(signedSize.x),Mathf.Abs(signedSize.y),Mathf.Abs(signedSize.z));
        }

        private void OnDragPreview(Vector3 dragStart, Vector3 dragEnd)
        {
        }
    }
}
