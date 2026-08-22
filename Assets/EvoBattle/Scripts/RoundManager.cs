using UnityEngine;

namespace CardHouse
{
    public class RoundManager : MonoBehaviour
    {
        [Header("Mana Growth")]
        public CurrencyScriptable ManaCurrency;
        public int StartingMaxMana = 1;
        public int ManaGrowthPerRound = 1;
        public int MaxManaCap = 10;

        [Header("Attack Token (tracked only for now)")]
        public int AttackTokenPlayerIndex = 0;

        [Header("State (read-only, watch in Inspector)")]
        public int CurrentRound = 0;
        public int ConsecutivePasses = 0;

        PhaseManager MyPhaseManager;
        CurrencyRegistry MyCurrencyRegistry;

        [Header("Round Draw")]
        public CardTransferOperator P1RoundDraw;
        public CardTransferOperator P2RoundDraw;
        public static RoundManager Instance;
        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            MyPhaseManager = PhaseManager.Instance;
            MyCurrencyRegistry = CurrencyRegistry.Instance;

            // Mana setup for round 1 only. Does NOT advance the phase —
            // PhaseManager already starts its own first phase on its own.
            StartNewRound();
        }

        // Wire the PASS button here instead of PhaseManager.NextPhase()
        public void Pass()
        {
            ConsecutivePasses++;

            if (ConsecutivePasses >= 2)
            {
                EndRound();
                return;
            }

            MyPhaseManager.NextPhase();
        }

        // Call this later when a card play should pass priority
        public void RegisterAction()
        {
            ConsecutivePasses = 0;
            MyPhaseManager.NextPhase();
        }

        void EndRound()
        {
            AttackTokenPlayerIndex = 1 - AttackTokenPlayerIndex;
            StartNewRound();

            MyPhaseManager.GoToPhase(AttackTokenPlayerIndex);
        }

        void StartNewRound()
        {
            CurrentRound++;
            ConsecutivePasses = 0;

            int newMax = Mathf.Min(
                StartingMaxMana + (CurrentRound - 1) * ManaGrowthPerRound,
                MaxManaCap);

            for (int p = 0; p < MyCurrencyRegistry.PlayerWallets.Count; p++)
            {
                var container = MyCurrencyRegistry.PlayerWallets[p].FindCurrency(ManaCurrency.name);
                if (container != null)
                {
                    container.Max = newMax;
                    MyCurrencyRegistry.Refill(ManaCurrency.name, p);
                }
            }

            if (CurrentRound > 1)
            {
                P1RoundDraw.Activate();
                P2RoundDraw.Activate();
            }
            // No NextPhase() call here — this method only handles mana.
        }
    }
}