namespace Translation
{
    public class StringFactory
    {
        /// <summary>
        /// The language that the user would like the string translated to.
        /// </summary>
        public static Language Language { get; set; }
        
        public static string getString(GameString gameString)
        {
            // TODO: Add the Google sheets API and query from a google sheet
            return gameString.ToString();
        }

        private string getLanguageString()
        {
            // TODO: Determine the language shorthand notations from the crowd sourced translation tool
            return Language.ToString();
        }
    }
}