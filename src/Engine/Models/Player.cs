using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Engine.Services;
using Engine.Shared;

namespace Engine.Models
{
    public class Player : LivingEntity
    {
        private int _experiencePoints;

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged();
                SetLevelAndMaximumHitpoints();
            }
        }

        public ObservableCollection<QuestStatus> Quests { get; } = new ObservableCollection<QuestStatus>();

        public ObservableCollection<Recipe> Recipes { get; } = new ObservableCollection<Recipe>();

        public event EventHandler OnLeveledUp;

        public Player(string name, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints, IEnumerable<PlayerAttribute> attributes, int gold) :
            base(name, maximumHitPoints, currentHitPoints, attributes, gold)
        {
            ExperiencePoints = experiencePoints;
        }

        public void AddExperiencePoints(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }

        /// Calculates the amount of experience required to reach a specified level.
        private int ExpRequiredForLevel(int level)
        {
            const double baseExp = 50;
            const double growth = 1.05;
            const double softCap = 20.0;
            const double damping = 0.1;

            double exp = baseExp * Math.Pow(level, growth) * Math.Pow(1 + (level / softCap), damping);
            return (int)Math.Round(exp);
        }

        public void SetLevelAndMaximumHitpoints()
        {
            int originalLevel = Level;

            int calculatedLevel = 1;
            int remainingExp = ExperiencePoints;
            while (remainingExp >= ExpRequiredForLevel(calculatedLevel))
            {
                remainingExp -= ExpRequiredForLevel(calculatedLevel);
                calculatedLevel++;
            }
            Level = calculatedLevel;

            if (Level != originalLevel)
            {
                MaximumHitPoints += 8 + Attributes.FirstOrDefault(p => p.Key == "CON").ModifiedValue/10;

                CompletelyHeal();
                LevelUpRandomAttribute();
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }

        // As the player doesn't use any other attributes, we can simplify the leveling up process to just CON and DEX.
        public void LevelUpRandomAttribute()
        {
            var availableAttributes = new[] { "CON", "DEX" };

            string randomAttribute = availableAttributes[DiceService.Instance.Roll(availableAttributes.Length).Value - 1];

            PlayerAttribute attributeToLevelUp = Attributes.FirstOrDefault(a => a.Key == randomAttribute);

            attributeToLevelUp.ModifiedValue += 1;
        }
    }
}
