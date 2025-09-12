namespace RpgEssentials.TurnBased
{
    public class PressTurnBattleBoard : BattleBoard
    {
        protected override BattleEntity PrepareTurnOrder()
        {
            return TurnEntity;
        }
    }

}


