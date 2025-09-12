namespace RpgEssentials.TurnBased
{
    public abstract class UnitySimpleBattleEntity : SimpleBattleEntity
    {
        public UnitySimpleBattleEntity
            (EntityMold mold, IBattleBehaviour battleBehaviour):
            base(mold, battleBehaviour)
        {
        }
    }
}



