using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Engine.Models;
using Engine.Shared;

namespace Engine.Factories
{
    public static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Monsters.xml";
        private static readonly List<Monster> _standardMonsters = new List<Monster>();
        
        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath = data.SelectSingleNode("/Monsters").AttributesAsString("RootImagePath");

                LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Game data file not found: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadMonstersFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }

            foreach(XmlNode node in nodes)
            {
                Monster monster = new Monster(node.AttributesAsInt("ID"),
                                              node.AttributesAsString("Name"),
                                              $".{rootImagePath}{node.AttributesAsString("ImageName")}",
                                              node.AttributesAsInt("MaximumHitPoints"),
                                              Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText),
                                              ItemFactory.CreateGameItem(node.AttributesAsInt("WeaponID")),
                                              node.AttributesAsInt("RewardXP"),
                                              node.AttributesAsInt("Gold"));


                XmlNodeList lootItemNodes = node.SelectNodes("./LootItems/LootItem");
                if (lootItemNodes != null)
                {
                    foreach (XmlNode lootItemNode in lootItemNodes)
                    {
                        monster.AddItemToLootTable(lootItemNode.AttributesAsInt("ID"),
                                                   lootItemNode.AttributesAsInt("Percentage"));
                    }
                }

                _standardMonsters.Add(monster);
            }
        }

        public static Monster GetMonster(int id)
        {
            return _standardMonsters.FirstOrDefault(m => m.ID == id)?.GetNewInstance();
        }
    }
}
