using UnityEngine;

namespace CardHouse
{
    public class BoardActionDetector : MonoBehaviour
    {
        private CardGroup group;
        private int previousCardCount;

        void Awake()
        {
            group = GetComponent<CardGroup>();
            previousCardCount = group.MountedCards.Count;
        }

        public void OnGroupChanged()
        {
            int currentCount = group.MountedCards.Count;

            // A card was added to this board.
            if (currentCount > previousCardCount)
            {
                RoundManager.Instance.RegisterAction();
            }

            previousCardCount = currentCount;
        }
    }
}