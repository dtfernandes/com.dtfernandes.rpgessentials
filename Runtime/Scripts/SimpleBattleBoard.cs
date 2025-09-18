using System.Collections.Generic;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    public class SimpleBattleBoard : BattleBoard
    {
        public IList<BattleEntity> TurnOrder { get; private set; }

        protected override BattleEntity PrepareTurnOrder()
        {
            TurnOrder = new List<BattleEntity> { };

            //Gather all alive entities
            IEnumerable<BattleEntity> aliveEntities = Entities.Where(x => !x.IsDead).Select(x => x);

            //Check if there're still entities than have enough turns
            if (!aliveEntities.Any(x => x.Turn > 0))
            {
                //If there aren't reset the entities and begin a new turn
                foreach (BattleEntity entity in aliveEntities)
                {
                    entity.ResetTurns();
                }
            }

            IList<BattleEntity> duplicateList =
                aliveEntities.Select(x => x.Copy()).ToList();


            int loops = 0;
            do
            {
                //Iterate trough all the entities
                foreach (BattleEntity entity in duplicateList)
                {
                    //Check if entity has enough turn to use
                    if (!entity.IsDead)
                    {
                        //If it has, add copy of entity to turn list
                        BattleEntity copy = entity.Copy();
                        TurnOrder.Add(copy);
                        entity.Turn--;
                    }
                }
                loops++;
                if (loops >= 50)
                {
                    throw new System.Exception("Infinit Loop");
                }
            }
            while (duplicateList.Any(x => x.Turn > 0));

            //Order the list
            TurnOrder =
                TurnOrder.OrderBy(x => x.OrderFunction()).ToList();

            if (TurnOrder.Count == 0)
                throw new System.Exception("Turn list is empty. Check if templates have maxTurn at 0.");

            //return the entity selected to be next
            return Entities.First(x => x.Equals(TurnOrder.FirstOrDefault()));
        }
    }
}



