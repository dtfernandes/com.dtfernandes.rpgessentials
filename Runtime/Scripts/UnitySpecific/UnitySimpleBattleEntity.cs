namespace RpgEssentials.TurnBased
{
    public abstract class UnitySimpleBattleEntity : SimpleBattleEntity
    {
        public UnitySimpleBattleEntity
            (EntityMold mold ,int maxTurns, IBattleBehaviour battleBehaviour):
            base(maxTurns, battleBehaviour)
        {
            this.Mold = mold.Copy();
        }
    }
}



