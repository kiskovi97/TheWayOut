using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheWayOut.Gameplay
{
    public class DragPlacement : MonoBehaviour, IDropHandler
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

        public virtual void RemoveItem()
        {
            IsPlaced = false;
        }
    }
}
