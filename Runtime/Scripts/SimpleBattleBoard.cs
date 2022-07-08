using System.Collections.Generic;
using System.Linq;

namespace RpgEssentials.TurnBased
{
    public abstract class SimpleBattleBoard<T> : BattleBoard<T> where T : BattleEntity
    {
        public IList<T> TurnOrder { get; private set; }

        protected override T PrepareTurnOrder()
        {
            TurnOrder = new List<T> { };

            //Gather all alive entities
            IEnumerable<T> aliveEntities = 
                Entities.Where(x => !x.IsDead).Select(x => x as T);
         
            //Check if there're still entities than have enough turns
            if(!aliveEntities.Any(x => x.Turn > 0))
            {
                //If there aren't reset the entities and begin a new turn
                foreach(T entity in aliveEntities)
                {
                    entity.ResetTurns();
                }
            }

            IList<T> duplicateList =
                aliveEntities.Select(x => x.Copy() as T).ToList();

                 
            int loops = 0;
            do {
                //Iterate trough all the entities
                foreach (T entity in duplicateList)
                {
                    //Check if entity has enough turn to use
                    if (entity.Turn > 0)
                    {                       
                        //If it has, add copy of entity to turn list
                        T copy =
                            entity.Copy() as T;                        
                        TurnOrder.Add(copy);
                        entity.Turn--;
                    }
                }
                loops++;
                if(loops >= 50)
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
            return entities.First(x => x.Equals(TurnOrder.FirstOrDefault()));
        }
    }
}



