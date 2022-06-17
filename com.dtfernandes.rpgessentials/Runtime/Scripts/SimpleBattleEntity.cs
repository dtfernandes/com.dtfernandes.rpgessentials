namespace RpgEssentials.TurnBase
{
    public abstract class SimpleBattleEntity : BattleEntity
    {
        public int MaxTurns { get; private set; }

        protected SimpleBattleEntity(int maxTurns) : base()
        {
            MaxTurns = maxTurns;
        }

        public override void ResetTurns()
        {
            Turn = MaxTurns;
        }
    }

}


