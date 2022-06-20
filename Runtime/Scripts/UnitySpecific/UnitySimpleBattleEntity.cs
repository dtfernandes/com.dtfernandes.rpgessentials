namespace RpgEssentials.TurnBased
{
    public abstract class UnitySimpleBattleEntity : SimpleBattleEntity
    {

        public SimpleEntityTemplate Template { get; protected set; }

        public UnitySimpleBattleEntity
            (SimpleEntityTemplate template ,int maxTurns, IBattleBehaviour battleBehaviour):
            base(maxTurns, battleBehaviour)
        {
            this.Template = template;
            this.Mold = template.Mold.Copy();
        }
    }
}



