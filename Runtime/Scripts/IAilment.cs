
namespace RpgEssentials.TurnBased
{

    public interface IAilment
    {
        public string AilmentTag { get; }

        public void Function(BattleEntity target, IBattleBoard board);
    }
}