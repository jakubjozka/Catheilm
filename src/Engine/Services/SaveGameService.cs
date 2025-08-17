using System;
using System.IO;
using Engine.Factories;
using Engine.Models;
using Engine.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Engine.Services
{
    // Service to save and load game sessions from JSON files
    public static class SaveGameService
    {
        public static void Save(GameSession gameSession, string filename)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(gameSession, Formatting.Indented));
        }
        public static GameSession LoadLastSaveOrCreateNew(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File not found: {filename}");
            }

            try
            {
                JObject data = JObject.Parse(File.ReadAllText(filename));
                Player player = CreatePlayer(data);
                int x = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.XCoordinate)];
                int y = (int)data[nameof(GameSession.CurrentLocation)][nameof(Location.YCoordinate)];

                return new GameSession(player, x, y);
            }
            catch
            {
               throw new FormatException($"Failed to load game session from {filename}.");
            }
        }
        private static Player CreatePlayer(JObject data)
        {
            Player player = new Player((string)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Name)],
                                       (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.ExperiencePoints)],
                                       (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.MaximumHitPoints)],
                                       (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.CurrentHitPoints)],
                                       GetPlayerAttributes(data),
                                       (int)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Gold)]);

            PopulatePlayerInventory(data, player);

            PopulatePlayerQuests(data, player);

            PopulatePlayerRecipes(data, player);

            return player;
        }

        private static IEnumerable<PlayerAttribute> GetPlayerAttributes(JObject data)
        {
            List<PlayerAttribute> attributes = new List<PlayerAttribute>();

            foreach (JToken itemToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Attributes)])
            {
                attributes.Add(new PlayerAttribute(
                                   (string)itemToken[nameof(PlayerAttribute.Key)],
                                   (string)itemToken[nameof(PlayerAttribute.DisplayName)],
                                   (string)itemToken[nameof(PlayerAttribute.DiceNotation)],
                                   (int)itemToken[nameof(PlayerAttribute.BaseValue)],
                                   (int)itemToken[nameof(PlayerAttribute.ModifiedValue)]));
            }

            return attributes;
        }
        private static void PopulatePlayerInventory(JObject data, Player player)
        {
            foreach (JToken itemToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Inventory)])
            {
                int itemId = (int)itemToken[nameof(GameItem.ItemTypeID)];

                player.AddItemToInventory(ItemFactory.CreateGameItem(itemId));
            }
        }
        private static void PopulatePlayerQuests(JObject data, Player player)
        {
            foreach (JToken questToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Quests)])
            {
                int questId = (int)questToken[nameof(QuestStatus.PlayerQuest)][nameof(QuestStatus.PlayerQuest.ID)];

                Quest quest = QuestFactory.GetQuestByID(questId);
                QuestStatus questStatus = new QuestStatus(quest);
                questStatus.IsCompleted = (bool)questToken[nameof(QuestStatus.IsCompleted)];

                player.Quests.Add(questStatus);
            }
        }
        private static void PopulatePlayerRecipes(JObject data, Player player)
        {
            foreach (JToken recipeToken in (JArray)data[nameof(GameSession.CurrentPlayer)][nameof(Player.Recipes)])
            {
                int recipeId = (int)recipeToken[nameof(Recipe.ID)];

                Recipe recipe = RecipeFactory.RecipeByID(recipeId);

                player.Recipes.Add(recipe);
            }
        }
    }
}