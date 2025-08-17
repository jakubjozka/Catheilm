using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Shared;

namespace Engine.Services
{
    public static class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

        public static Combatant FirstAttacker(Player player, Monster opponent)
        {
            decimal totalOffset = CalculateDamage(player, opponent);

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset ? Combatant.Player : Combatant.Opponent;
        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            decimal totalOffset = CalculateDamage(attacker, target);

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset;
        }

        // Using a formula, we calculate the damage based on the dexterity of both the attacker and the target
        public static decimal CalculateDamage(LivingEntity attacker, LivingEntity target)
        {
            int playerDexterity = attacker.GetAttribute("DEX").ModifiedValue * attacker.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = target.GetAttribute("DEX").ModifiedValue * target.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return totalOffset;
        }
    }
}
