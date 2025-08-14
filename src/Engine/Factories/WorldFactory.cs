using System.IO;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Locations.xml";
        internal static World CreateWorld()
        {
            World world = new World();

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath = data.SelectSingleNode("/Locations").AttributesAsString("RootImagePath");

                LoadLocationsFromNodes(world,
                                       rootImagePath,
                                       data.SelectNodes("/Locations/Location"));
            }
            else
            {
                throw new FileNotFoundException($"The game data file '{GAME_DATA_FILENAME}' was not found.");
            }

            return world;
        }

        private static void LoadLocationsFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {

                int? lockedUntilQuestID = null;
                if (node.Attributes["LockedUntilQuestID"] != null)
                {
                    lockedUntilQuestID = node.AttributesAsInt("LockedUntilQuestID");
                }
                Location location = new Location(node.AttributesAsInt("X"),
                                                  node.AttributesAsInt("Y"),
                                                  node.AttributesAsString("Name"),
                                                  node.SelectSingleNode("./Description")?.InnerText ?? "",
                                                  $".{rootImagePath}{node.AttributesAsString("ImageName")}",
                                                  null,
                                                  lockedUntilQuestID);

                AddMonsters(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));

                world.AddLocation(location);
            }
        }

        private static void AddMonsters(Location location, XmlNodeList monsters)
        {
            if (monsters == null)
            {
                return;
            }
            foreach(XmlNode monsterNode in monsters)
            {
                location.AddMonster(monsterNode.AttributesAsInt("ID"),
                                    monsterNode.AttributesAsInt("Percent"));
            }
        }

        private static void AddQuests(Location location, XmlNodeList quests)
        {
            if (quests == null)
            {
                return;
            }
            foreach (XmlNode questNode in quests)
            {
                location.QuestsAvailableHere.Add(QuestFactory.GetQuestByID(questNode.AttributesAsInt("ID")));
            }
        }

        private static void AddTrader(Location location, XmlNode traderHere)
        {
            if (traderHere == null)
            {
                return;
            }
            location.TraderHere = TraderFactory.GetTraderByID(traderHere.AttributesAsInt("ID"));
        }
    }
}
