using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Services;
using Engine.Factories;

namespace Engine.ViewModels
{
    public class CharacterCreationViewModel : BaseNotificationClass
    {
        private Race _selectedRace;
        public GameDetails GameDetails { get; }

        public Race SelectedRace
        {
            get { return _selectedRace; }
            set
            {
                _selectedRace = value;
                OnPropertyChanged();
            }
        }

        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } = new ObservableCollection<PlayerAttribute>();

        public bool HasRaces => GameDetails.Races.Any();

        public bool HasRaceAttributedModifiers => HasRaces && GameDetails.Races.Any(r => r.PlayerAtrributeModifiers.Any());

        public CharacterCreationViewModel()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
            if (HasRaces)
            {
                SelectedRace = GameDetails.Races.First();
            }

            RollNewCharacter();
        }

        // Rerolls all player attributes for the character
        public void RollNewCharacter()
        {
            PlayerAttributes.Clear();
            foreach (PlayerAttribute playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.ReRoll();
                PlayerAttributes.Add(playerAttribute);
            }

            ApplyAttributeModifiers();
        }

        // Applies the race attribute modifiers to the player attributes
        public void ApplyAttributeModifiers()
        {
            foreach (PlayerAttribute playerAttribute in PlayerAttributes)
            {
                var attributeRaceModifier = SelectedRace.PlayerAtrributeModifiers.FirstOrDefault(pam => pam.AttributeKey.Equals(playerAttribute.Key));

                playerAttribute.ModifiedValue = playerAttribute.BaseValue + (attributeRaceModifier?.Modifier ?? 0);
            }
        }

        public Player GetPlayer()
        {
            Player player =  new Player(Name, 
                              0, 
                              10, 
                              10, 
                              PlayerAttributes, 
                              10);

            player.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            player.LearnRecipe(RecipeFactory.RecipeByID(1));
            player.LearnRecipe(RecipeFactory.RecipeByID(2));
            player.LearnRecipe(RecipeFactory.RecipeByID(3));
            player.LearnRecipe(RecipeFactory.RecipeByID(4));
            player.LearnRecipe(RecipeFactory.RecipeByID(5));
            player.LearnRecipe(RecipeFactory.RecipeByID(6));

            return player;
        }
    }
}
