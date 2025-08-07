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
            Player player =  new Player("Name", 
                              0, 
                              10, 
                              10, 
                              PlayerAttributes, 
                              10000);

            player.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            player.LearnRecipe(RecipeFactory.RecipeByID(1));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3003));

            return player;
        }
    }
}
