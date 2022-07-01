namespace RpgEssentials.TurnBased
{
    public class PressTurnBattleBoard : BattleBoard<BattleEntity>
    {  
        protected override BattleEntity PrepareTurnOrder()
        {
            return TurnEntity;
        }
    }

}


