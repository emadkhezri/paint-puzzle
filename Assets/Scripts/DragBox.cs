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
            Bounds bound = new Bounds();
            bound.SetMinMax(dragStart,dragEnd);
            _boxCollider.center = bound.center;
            _boxCollider.size = bound.extents;
        }

        private void OnDragPreview(Vector3 dragStart, Vector3 dragEnd)
        {
        }
    }
}
