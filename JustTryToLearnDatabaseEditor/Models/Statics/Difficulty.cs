using System.Collections.Generic;

namespace JustTryToLearnDatabaseEditor.Models.Statics
{
    public static class Difficulty
    {
        private static readonly List<string> DifficultyTypes;
        
        static Difficulty()
        {
            DifficultyTypes = new List<string>()
            {
                "Легко",
                "Непросто",
                "Тяжко",
                "Армагедон"
            };
        }
        
        public static List<string> GetAllTypes()
        {
            return DifficultyTypes;
        }

        public static string GetDefault()
        {
            return DifficultyTypes[0];
        }
    }
}