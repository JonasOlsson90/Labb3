using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3.DefaultData
{
    public static class DefaultData
    {
        // Statisk klass för att hålla defaultinformation. Json-strängen ser bedrövlig ut, men ingen ska in här och ändra manuelt ändå.
        // Vill man ändra standardquiz så får man göra det med applikationen och ta json-strängen från dokumentet och konvertera.
        public static string DefaultQuizJsonString =
            "[{\"Questions\":[{\"Category\":\"Pok\u00e9mon\",\"Statement\":\"What does Oddish evolve to?\",\"Answers\":[\"Vileplume\",\"Gloom\",\"Bulbasaur\"],\"CorrectAnswer\":1,\"ImagePath\":null,\"IsAsked\":false},{\"Category\":\"Science & Nature\",\"Statement\":\"What is the phenomenon of mirrored molecules called in chemistry?\",\"Answers\":[\"Chirality\",\"Diffusion\",\"Speculum\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Arts & Literature\",\"Statement\":\"Who wrote the novel Factotum?\",\"Answers\":[\"Niccol\u00f3 Machiavelli\",\"Ken Kesey\",\"Charles Bukowski\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"History\",\"Statement\":\"Which year was Carl Von Linn\u00e9 born?\",\"Answers\":[\"1607\",\"1707\",\"1807\"],\"CorrectAnswer\":1,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Entertainment\",\"Statement\":\"How many bolts are there in the \u00d6lands bridge according to Nile City?\",\"Answers\":[\"15 473\",\"1 000 000\",\"7 428 954\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Geography\",\"Statement\":\"What is the name of the smallest country by area in Africa?\",\"Answers\":[\"Equatorial Guinea\",\"Djibouti\",\"Seychelles\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Science & Nature\",\"Statement\":\"What is mol a messure of in chemestry?\",\"Answers\":[\"Amount\",\"Wheight\",\"Volume\"],\"CorrectAnswer\":0,\"ImagePath\":null,\"IsAsked\":false},{\"Category\":\"Sports & Leisure\",\"Statement\":\"How much did Ray Williams squat on March 2, 2019 to set the current word record in raw drug-tested squat?\",\"Answers\":[\"290 kg\",\"390 kg\",\"490 kg\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Science & Nature\",\"Statement\":\"What is the scientific name of the plant family that includes potato, tomato and chili peppers amongst others?\",\"Answers\":[\"Fabaceae\",\"Solanaceae\",\"Malvaceae\"],\"CorrectAnswer\":1,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Geography\",\"Statement\":\"How long is the river Nile?\",\"Answers\":[\"6 650 km\",\"2 650 km\",\"650 km\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Geography\",\"Statement\":\"How man countries border Mongolia?\",\"Answers\":[\"2\",\"3\",\"4\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Arts & Literature\",\"Statement\":\"Who wrote the song 'Keep on Rockin in the Freee Word'?\",\"Answers\":[\"Bruce Springsteen\",\"David Bowie\",\"Neil Young\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Arts & Literature\",\"Statement\":\"Who wrote the song \\\"Changing of the Guards\\\"?\",\"Answers\":[\"Neil Young\",\"Bob Dylan\",\"David Bowie\"],\"CorrectAnswer\":1,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"History\",\"Statement\":\"When did Soviet Russia become the Soviet Union?\",\"Answers\":[\"1917\",\"1922\",\"1936\"],\"CorrectAnswer\":1,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Entertainment\",\"Statement\":\"What year did Monty Python's movie \\\"Life of Brian\\\" premier?\",\"Answers\":[\"1985\",\"1979\",\"1972\"],\"CorrectAnswer\":1,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Entertainment\",\"Statement\":\"What is the name of the legendary turn based strategy game series created by Sid Meier?\",\"Answers\":[\"Civilization\",\"Age of Empires\",\"Europa Universalis\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Sports & Leisure\",\"Statement\":\"Which colour indicates the highest grade in judo of the following?\",\"Answers\":[\"Blue\",\"Orange\",\"Green\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Sports & Leisure\",\"Statement\":\"Is it ok to choke an oponent to submition i Judo?\",\"Answers\":[\"Yes\",\"No\",\"\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Pok\u00e9mon\",\"Statement\":\"Which of the following pok\u00e9mon was not an option at the start of the original Game Boy games red and blue?\",\"Answers\":[\"Bulbasaur\",\"Squirtle\",\"Pikachu\"],\"CorrectAnswer\":2,\"ImagePath\":\"\",\"IsAsked\":false},{\"Category\":\"Pok\u00e9mon\",\"Statement\":\"What was the pok\u00e9mon Kangaskhan described as in the original tv-series?\",\"Answers\":[\"The parent pok\u00e9mon\",\"The pouch pok\u00e9mon\",\"The bad ass pok\u00e9mon\"],\"CorrectAnswer\":0,\"ImagePath\":\"\",\"IsAsked\":false}],\"Title\":\"Jonas Super Standard Quiz\"}]";

        public static string[] DefaultCategories =
        {
            "Geography",
            "Entertainment",
            "History",
            "Arts & Literature",
            "Science & Nature",
            "Sports & Leisure",
            "Pokémon"
        };
    }
}
