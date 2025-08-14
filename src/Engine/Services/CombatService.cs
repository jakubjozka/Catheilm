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
            int playerDexterity = player.GetAttribute("DEX").ModifiedValue * player.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = opponent.GetAttribute("DEX").ModifiedValue * opponent.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset ? Combatant.Player : Combatant.Opponent;
        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            int playerDexterity = attacker.GetAttribute("DEX").ModifiedValue * attacker.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = target.GetAttribute("DEX").ModifiedValue * target.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            decimal finalChance = 50 + totalOffset;
            int roll = DiceService.Instance.Roll(100).Value;
            bool hit = roll <= finalChance;

            // DEBUG - remove after testing
            System.Diagnostics.Debug.WriteLine($"Combat: {attacker.Name}(DEX:{attacker.GetAttribute("DEX").ModifiedValue}) vs {target.Name}(DEX:{target.GetAttribute("DEX").ModifiedValue})");
            System.Diagnostics.Debug.WriteLine($"DEX²: {playerDexterity} vs {opponentDexterity}, Offset: {dexterityOffset}");
            System.Diagnostics.Debug.WriteLine($"Random: {randomOffset}, Total: {totalOffset}, Chance: {finalChance}%");
            System.Diagnostics.Debug.WriteLine($"Roll: {roll}, Hit: {hit}");
            System.Diagnostics.Debug.WriteLine("---");

            return hit;
        }
    }
}
