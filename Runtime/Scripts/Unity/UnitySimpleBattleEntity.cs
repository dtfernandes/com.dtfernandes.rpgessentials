namespace RpgEssentials.TurnBased
{
    public abstract class UnitySimpleBattleEntity : SimpleBattleEntity
    {
        public UnitySimpleBattleEntity
            (EntityMold mold, IBattleBehaviour battleBehaviour, SelectionTeam team) :
            base(mold, battleBehaviour, team)
        {
        }
    }
}



