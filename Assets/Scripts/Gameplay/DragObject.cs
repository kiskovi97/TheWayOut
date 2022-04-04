using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TheWayOut.Gameplay
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class DragObject : Selectable, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        protected RectTransform rectTransform;
        private CanvasGroup canvasGroup;

        private Vector3 lastPosition;
        protected DragPlacement InPlace;

        [SerializeField] private Image selectionImage;
        [NonSerialized] public bool isDragable = true;

        public static DragObject lastSelected { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            lastPosition = rectTransform.position;
        }

        internal virtual void SetInPlace(DragPlacement placement)
        {
            InPlace = placement;
            OnDeselect();
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

        internal void OnDeselect()
        {
            if (lastSelected == this)
                lastSelected = null;
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

        internal void ReturnToPosition()
        {
            rectTransform.position = lastPosition;
        }

        protected virtual void Update()
        {
            if (InPlace != null)
                transform.position = InPlace.transform.position;
            if (selectionImage != null)
                selectionImage.enabled = lastSelected == this;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            if (InPlace == null)
                lastSelected = this;
        }
    }
}


