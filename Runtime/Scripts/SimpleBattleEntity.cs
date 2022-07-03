namespace RpgEssentials.TurnBased
{
    public abstract class SimpleBattleEntity : BattleEntity
    {
        public int MaxTurns { get; private set; }

        protected SimpleBattleEntity(int maxTurns, 
            IBattleBehaviour battleBehaviour) : base(battleBehaviour)
        {
            MaxTurns = maxTurns;
        }

        internal override void ResetTurns()
        {
            Turn = MaxTurns;
        }
    }

}


