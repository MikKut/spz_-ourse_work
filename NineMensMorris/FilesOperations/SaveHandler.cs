using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NineMensMorris.FilesOperations
{
    internal static class SaveHandler
    {
        private static ushort _quantityOfTheSaves = 0;
        public static ushort QuantityOfTheSaves
        {
            get => _quantityOfTheSaves; 
            private set => _quantityOfTheSaves = value;
        }

        static SaveHandler()
        {
            ushort temp;
            if (File.Exists(Paths.pathOfTheCounter))
            {
                if (!UInt16.TryParse(File.ReadAllText(Paths.pathOfTheCounter), out temp))
                {
                    QuantityOfTheSaves = temp;
                }
            }
            else
            {
                File.WriteAllText(Paths.pathOfTheCounter, "0");
                QuantityOfTheSaves = 0;
            }
        }
        public static string[] GetNamesArray()
        {
            //C:\Users\User\source\repos\spz_-ourse_work\NineMensMorrisKeeper\bin\Debug\net6.0-windows\NineMensMorris\FilesOperations\Saves
            return File.ReadAllLines(Paths.pathOfTheNames);
        }
        public static bool ContainsTheNameInSavedList(string name)
        {
            var names  = GetNamesArray();
            foreach (string arrayName in names)
            {
                if (name == arrayName)
                {
                    return true;
                }
            }
            return false;
        }
        public static void Save(string name)
        {
            MakeRelevantNameThatConsidersRepetitions(ref name);
            AddNewLineSymnolToTheName(ref name);
            ++QuantityOfTheSaves;
            string newFullName = $@"{Paths.pathFromBootDirectoryToStartProjDir}\{Paths.PathFromStartProjFolderToSaves}\{AddJsonEndingToTheName(name).Substring(1)}";
            File.WriteAllText(newFullName, SerializeGameState());
            File.WriteAllText(Paths.pathOfTheCounter, QuantityOfTheSaves.ToString());
            File.AppendAllText(Paths.pathOfTheNames, name);
        }
 
        public static void Load(string name)
        {
            DeserializeGameState
                (
                    File.ReadAllText(@$"{Paths.pathFromBootDirectoryToStartProjDir}\{Paths.PathFromStartProjFolderToSaves}\{name}.json")
                );
        }
        private static string SerializeGameState()
        {
            StringBuilder str = new(System.Text.Json.JsonSerializer.Serialize<Models.PeriodOfTheGame>(Models.GameState.GamePeriod));
            str.Append($"\n{System.Text.Json.JsonSerializer.Serialize<Models.Turn>(Models.GameState.TurnOfThePlayer)}");
            str.Append($"\n{System.Text.Json.JsonSerializer.Serialize<ushort>(Models.GameState.MoveNumber)}");
            str.Append($"\n{System.Text.Json.JsonSerializer.Serialize<byte>(Models.GameState.QuantityOfChipsOfPlayer1)}");
            str.Append($"\n{System.Text.Json.JsonSerializer.Serialize<byte>(Models.GameState.QuantityOfChipsOfPlayer2)}");
            str.Append($"\n{JsonConvert.SerializeObject/*<Dictionary<Models.ButtonPosition, Models.ChipState>>*/(Models.GameState.ButtonStates.Values, Formatting.Indented)}");
            return str.ToString();
        }
        private static void DeserializeGameState(string content)
        {
            var stArray = content.Split('\n');
            string st;
            byte stringsCounter = 0, stringArraySize = (byte)stArray.Length;
            Models.GameState.GamePeriod = System.Text.Json.JsonSerializer.Deserialize<Models.PeriodOfTheGame>(stArray[stringsCounter++]);
            Models.GameState.TurnOfThePlayer = System.Text.Json.JsonSerializer.Deserialize<Models.Turn>(stArray[stringsCounter++]);
            Models.GameState.MoveNumber = System.Text.Json.JsonSerializer.Deserialize<ushort>(stArray[stringsCounter++]);
            Models.GameState.QuantityOfChipsOfPlayer1 = System.Text.Json.JsonSerializer.Deserialize<byte>(stArray[stringsCounter++]);
            Models.GameState.QuantityOfChipsOfPlayer2 = System.Text.Json.JsonSerializer.Deserialize<byte>(stArray[stringsCounter++]);
            stringsCounter++;
            for (Models.ButtonPosition i = Models.ButtonPosition.a1; i<=Models.ButtonPosition.g1; i++)
            {
                st = stArray[stringsCounter][2].ToString();
                Models.GameState.ButtonStates[i] = (Models.ChipState)JsonConvert.DeserializeObject<int>(stArray[stringsCounter++][2].ToString());
            }
        }
        private static void MakeRelevantNameThatConsidersRepetitions(ref string name)
        {
            byte counterOfRepeatedNames = 0;
            string mainPartOfTheName = new(name );
            
            while (ContainsTheNameInSavedList(mainPartOfTheName + $"({counterOfRepeatedNames++})")) ;
            name = mainPartOfTheName + $"({--counterOfRepeatedNames})";
        }
        private static string AddJsonEndingToTheName(string name)
        {
            if (!name.EndsWith(".json"))
            {
                name = name + ".json";
            }
            return name;
        }
        private static void AddNewLineSymnolToTheName(ref string name)
        {
            if (!name.StartsWith('\n'))
            {
                name = '\n' + name;
            }
        }
    }
}
