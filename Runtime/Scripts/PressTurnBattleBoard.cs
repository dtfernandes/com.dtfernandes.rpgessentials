namespace RpgEssentials.TurnBase
{
    public class PressTurnBattleBoard : BattleBoard<BattleEntity>
    {
        protected override BattleEntity PrepareTurnOrder()
        {
            return TurnEntity;
        }
    }

}


