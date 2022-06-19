namespace RpgEssentials.TurnBased
{
    public abstract class UnitySimpleBattleEntity : SimpleBattleEntity
    {

        public SimpleEntityTemplate Template { get; protected set; }

        public UnitySimpleBattleEntity(SimpleEntityTemplate template ,int maxTurns):
            base(maxTurns)
        {
            this.Template = template;
            this.Mold = template.Mold.Copy();
        }


        public override bool Equals(BattleEntity other)
        {
            if (!(other is UnitySimpleBattleEntity)) return false;
            UnitySimpleBattleEntity o = other as UnitySimpleBattleEntity;
            //Isto ta mal
            return o.Template == Template;
        }
    }

}



