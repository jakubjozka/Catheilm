using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        public void SetLevelAndMaximumHitpoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints += Level * 10;

                CompletelyHeal();

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
