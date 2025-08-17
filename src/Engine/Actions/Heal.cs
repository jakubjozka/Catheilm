using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;

namespace Engine.Actions
{
    internal class Heal : BaseAction, IAction
    {
        private readonly int _hitPointsToHeal;

        public Heal(GameItem itemInUse, int hitPointsToHeal)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a consumable item");
            }
            if (hitPointsToHeal < 0)
            {
                throw new ArgumentException("Hit points to heal cannot be negative");
            }

            _hitPointsToHeal = hitPointsToHeal;
        }

        // Reports the result of the healing action and applies healing to the target.
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal > 0 ? "s" : "")}");
            target.Heal(_hitPointsToHeal);
        }
    }
}
