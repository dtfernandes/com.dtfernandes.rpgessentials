namespace RpgEssentials.TurnBased
{
    public abstract class SimpleBattleEntity : BattleEntity
    {
        public int MaxTurns { get; protected set; }

        protected SimpleBattleEntity(EntityMold mold,
            IBattleBehaviour battleBehaviour) : base(mold, battleBehaviour)
        {

        }

        public override void ResetTurns()
        {
            Turn = MaxTurns; 
        }
    }

}


