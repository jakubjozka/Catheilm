using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Models;
using Engine.Shared;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.RootFinding;
using Newtonsoft.Json.Linq;

namespace Engine.Services
{
    public static class GameDetailsService
    {
        public static GameDetails ReadGameDetails()
        {
            JObject gameDetailsJson = JObject.Parse(File.ReadAllText(".\\GameData\\GameDetails.json"));

            GameDetails gameDetails = new GameDetails(gameDetailsJson.StringValueOf("Name"),
                                                      gameDetailsJson.StringValueOf("Version"));

            foreach (JToken token in gameDetailsJson["PlayerAttributes"])
            {
                gameDetails.PlayerAttributes.Add(new PlayerAttribute(token["Key"].ToString(),
                                                                     token["DisplayName"].ToString(),
                                                                     token["DiceNotation"].ToString()));
            }

            if (gameDetailsJson["Races"] != null)
            {
                foreach(JToken token in gameDetailsJson["Races"])
                {
                    Race race = new Race
                                {
                                    Key = token.StringValueOf("Key"),
                                    DisplayName = token.StringValueOf("DisplayName"),
                                };

                    if (token["PlayerAttributeModifiers"] != null)
                    {
                        foreach (JToken childToken in token["PlayerAttributeModifiers"])
                        {
                            race.PlayerAtrributeModifiers.Add(new PlayerAttributedModifier
                            {
                                AttributeKey = childToken.StringValueOf("AttributedKey"),
                                Modifier = childToken.IntValueOf("Modifier")
                            });
                        }
                    }
                    gameDetails.Races.Add(race);
                }
            }
            return gameDetails;
        }
    }
}

