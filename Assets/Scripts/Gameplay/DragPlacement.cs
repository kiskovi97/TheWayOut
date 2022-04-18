using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheWayOut.Gameplay
{
    public class DragPlacement : MonoBehaviour, IDropHandler, IPointerDownHandler
    {
        public bool IsPlaced { get; private set; }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                OnElementDroped(eventData.pointerDrag.GetComponent<DragObject>());
            }
        }

        public virtual void OnElementDroped(DragObject dragObject)
        {
            if (!dragObject.isDragable) return;
            dragObject.SetInPlace(this);
            IsPlaced = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var lastSelected = DragObject.lastSelected;
            if (lastSelected != null)
            {
                lastSelected.SetLastPosition();
                lastSelected.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
                OnElementDroped(lastSelected);
                lastSelected.OnDeselect();
            }
        }

        public virtual void RemoveItem()
        {
            IsPlaced = false;
        }
    }
}
