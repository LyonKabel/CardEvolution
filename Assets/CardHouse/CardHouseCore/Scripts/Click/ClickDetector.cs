using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CardHouse
{
    public class ClickDetector : Toggleable,
        IPointerDownHandler,
        IPointerClickHandler
    {
        public UnityEvent OnPress;
        public UnityEvent OnButtonClicked;

        public GateCollection<NoParams> ClickGates;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (IsActive && ClickGates.AllUnlocked(null))
            {
                OnPress.Invoke();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            if (IsActive && ClickGates.AllUnlocked(null))
            {
                OnButtonClicked.Invoke();
            }
        }
    }
}