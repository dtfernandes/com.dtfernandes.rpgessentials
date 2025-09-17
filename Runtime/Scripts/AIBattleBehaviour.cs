using System.Linq;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public class AIBattleBehaviour : IBattleBehaviour
    {
        protected BattleEntity entity;

        public virtual void StartBehaviour(BattleBoard board)
        {
            entity = board.TurnEntity;

            //Select move
            IBattleMove selectedMove = entity.Mold.Moves[0];

            //Select all possible entities
            IEnumerable<BattleEntity> entities =
                board.Entities.Where(x => x.Team != entity.Team && !x.IsDead);

            //Resolve Attack
            entity.UseMove(selectedMove, entities);

            board.NextTurn();
        }

        public virtual bool UpdateBehaviour()
        {
            return true;
        }

        public virtual void EndBehaviour()
        {

        }

    }

}
