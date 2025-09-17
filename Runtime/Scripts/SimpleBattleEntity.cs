namespace RpgEssentials.TurnBased
{
    public abstract class SimpleBattleEntity : BattleEntity
    {
        public int MaxTurns { get; protected set; }

        protected SimpleBattleEntity(EntityMold mold,
            IBattleBehaviour battleBehaviour, SelectionTeam team) : base(mold, battleBehaviour, team)
        {

        }

        public override void ResetTurns()
        {
            Turn = MaxTurns;
        }
    }

}


