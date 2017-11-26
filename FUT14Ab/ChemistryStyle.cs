using System.Collections.Generic;
using System.Linq;

namespace FUT14AB
{
    public class ChemistryStyle
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Zone { get; set; }
        public string Abbr { get; set; }
        public Dictionary<string, int> Booster { get; set; }



        public List<ChemistryStyle> GetAll()
        {
            var styles = new List<ChemistryStyle>
            {
                new ChemistryStyle
                {
                    Id = 250,
                    Abbr = "BAS",
                    Name = "Basic",
                    Booster = null,
                    Zone = "All"
                },
                new ChemistryStyle
                {
                    Id = 251,
                    Abbr = "SNI",
                    Name = "Sniper",
                    Booster = null,
                    Zone = "Attacking"
                },
                new ChemistryStyle
                {
                    Id = 252,
                    Abbr = "FIN",
                    Name = "Finisher",
                    Booster = null,
                    Zone = "Attacking"
                },
                new ChemistryStyle
                {
                    Id = 253,
                    Abbr = "EYE",
                    Name = "Deadeye",
                    Booster = null,
                    Zone = "Attacking"
                },
                new ChemistryStyle
                {
                    Id = 254,
                    Abbr = "MRK",
                    Name = "Marksman",
                    Booster = null,
                    Zone = "Attacking"
                },
                new ChemistryStyle
                {
                    Id = 255,
                    Abbr = "HWK",
                    Name = "Hawk",
                    Booster = null,
                    Zone = "Attacking"
                },
                new ChemistryStyle
                {
                    Id = 256,
                    Abbr = "ART",
                    Name = "Artist",
                    Booster = null,
                    Zone = "Midfield"
                },
                new ChemistryStyle
                {
                    Id = 257,
                    Abbr = "ARC",
                    Name = "Architect",
                    Booster = null,
                    Zone = "Midfield"
                },
                new ChemistryStyle
                {
                    Id = 258,
                    Abbr = "PWR",
                    Name = "Powerhouse",
                    Booster = null,
                    Zone = "Midfield"
                },
                new ChemistryStyle
                {
                    Id = 259,
                    Abbr = "MAE",
                    Name = "Maestro",
                    Booster = null,
                    Zone = "Midfield"
                },
                new ChemistryStyle
                {
                    Id = 260,
                    Abbr = "ENG",
                    Name = "Engine",
                    Booster = null,
                    Zone = "Midfield"
                },
                new ChemistryStyle
                {
                    Id = 261,
                    Abbr = "SEN",
                    Name = "Sentinel",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 262,
                    Abbr = "GRD",
                    Name = "Guardian",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 263,
                    Abbr = "GLA",
                    Name = "Gladiator",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 264,
                    Abbr = "BAC",
                    Name = "Backbone",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 265,
                    Abbr = "ANC",
                    Name = "Anchor",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 266,
                    Abbr = "HUN",
                    Name = "Hunter",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 267,
                    Abbr = "CTA",
                    Name = "Catalyst",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 268,
                    Abbr = "SHA",
                    Name = "Shadow",
                    Booster = null,
                    Zone = "Defense"
                },
                new ChemistryStyle
                {
                    Id = 269,
                    Abbr = "WAL",
                    Name = "Wall-GK",
                    Booster = null,
                    Zone = "GK"
                },
                new ChemistryStyle
                {
                    Id = 270,
                    Abbr = "SLD",
                    Name = "Shield-GK",
                    Booster = null,
                    Zone = "GK"
                },
                new ChemistryStyle
                {
                    Id = 271,
                    Abbr = "CAT",
                    Name = "Cat-GK",
                    Booster = null,
                    Zone = "GK"
                },
                new ChemistryStyle
                {
                    Id = 272,
                    Abbr = "GLO",
                    Name = "Glove-GK",
                    Booster = null,
                    Zone = "GK"
                },
                new ChemistryStyle
                {
                    Id = 273,
                    Abbr = "BAS",
                    Name = "Basic-GK",
                    Booster = null,
                    Zone = "GK"
                }
            };
            return styles;
        }


        public ChemistryStyle GetStyleById(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }

        public Dictionary<string, int> GetBooster(int styleId)
        {
            return new Dictionary<string, int>();
        }
    }
}
