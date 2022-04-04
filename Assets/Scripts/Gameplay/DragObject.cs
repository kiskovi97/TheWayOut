using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheWayOut.Gameplay
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class DragObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        protected RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private Vector3 lastPosition;
        private DragPlacement InPlace;

        [NonSerialized] public bool isDragable = true;

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            lastPosition = rectTransform.position;
        }

        internal virtual void SetInPlace(DragPlacement placement)
        {
            InPlace = placement;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isDragable) return;
            if (InPlace != null)
            {
                InPlace.RemoveItem();
            }
            else
            {
                lastPosition = rectTransform.position;
            }
            InPlace = null;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragable) return;
            rectTransform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            if (!isDragable) return;
            if (!InPlace)
                ReturnToPosition();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }

        internal void ReturnToPosition()
        {
            rectTransform.position = lastPosition;
        }

        private void Update()
        {
            if (InPlace != null)
                transform.position = InPlace.transform.position;
        }
    }
}


