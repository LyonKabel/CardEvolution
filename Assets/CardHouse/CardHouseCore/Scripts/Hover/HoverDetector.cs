using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CardHouse
{
    public class HoverDetector : Toggleable,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public UnityEvent OnHover;
        public UnityEvent OnUnHover;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsActive)
                return;

            OnHover.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsActive)
                return;

            OnUnHover.Invoke();
        }
    }
}