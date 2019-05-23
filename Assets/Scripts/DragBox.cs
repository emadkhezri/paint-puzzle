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

        [SerializeField]
        private TileBoard _tileBoard;

        private void Start() {
            _boxCollider.size = Vector3.zero;
            _boxCollider.center = Vector3.one;
            MouseInputHandler.OnDragFinished += OnDragFinished;
            MouseInputHandler.OnDragPreview += OnDragPreview;
        }

        private void OnDragFinished(Vector3 dragStart, Vector3 dragEnd)
        {
            Vector3 dragCenter = (dragStart+dragEnd)/2f;
            _boxCollider.center = dragCenter-_tileBoard.TilePrefabPivot;
            Vector3 signedSize = dragEnd-dragStart;
            _boxCollider.size = new Vector3(Mathf.Abs(signedSize.x),Mathf.Abs(signedSize.y),2);
        }

        private void OnDragPreview(Vector3 dragStart, Vector3 dragEnd)
        {
        }
    }
}
