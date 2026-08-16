using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CardHouse
{
    public class DragDetector : Toggleable, IPointerDownHandler, IPointerUpHandler
    {
        public GateCollection<NoParams> DragGates;
        public UnityEvent OnDragStart;

        [FormerlySerializedAs("DropGates")]
        public GateCollection<DropParams> GroupDropGates;

        public GateCollection<TargetCardParams> TargetCardGates;
        public UnityEvent OnDragEnd;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsActive || !DragGates.AllUnlocked(null))
                return;

            OnDragStart.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!IsActive || !DragGates.AllUnlocked(null))
                return;

            OnDragEnd.Invoke();
        }
    }
}