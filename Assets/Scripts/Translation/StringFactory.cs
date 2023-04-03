using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using System.IO;

namespace Translation
{
    /// <summary>
    /// A factory for creating strings. Before use, ensure that the strings are loaded
    /// before using.
    /// </summary>
    public class StringFactory
    {
        /// <summary>
        /// The language that the user would like the string translated to.
        /// </summary>
        public static LanguageType Language { get; set; } = LanguageType.French;
        
        /// <summary>
        /// A dictionary of strings to display on the screen.
        /// The dictionary is set by default to english if it cannot load.
        /// The strings should be loaded before use.
        /// </summary>
        public static Dictionary<GameString, string> Strings { get; set; } = new Dictionary<GameString, string>();

        /// <summary>
        /// Load the strings from our database or values.
        /// If unable to load any strings, the defaults are loaded.
        /// </summary>
        public static void LoadStrings()
        {
            SetDefaultStrings();

            if (Language != null)
            {
                SetLanguage(Language);
            }

            // Ensure that all game strings have been populated.
            // When loading, if all strings are not populated, the `getString()` method will throw an error.
            foreach(GameString s in Enum.GetValues(typeof(GameString)))
            {
                GetString(s);
            }
        }

        /// <summary>
        /// Method used to change the language.
        /// Pass in the new language to use and the game strings will be updated.
        /// </summary>
        public static void SetLanguage(LanguageType language)
        {
            string fileName = LanguageLookup.languageLookup[language] + "_strings.csv";
            StreamReader reader = new StreamReader($"Assets/lang/{fileName}");
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                {
                    var stringName = line.Split(',')[0];
                    var stringValue = line.Split(',')[2];
                    GameString.TryParse<GameString>(stringName, false, out GameString gameString);
                    if (gameString != null)
                    {
                        Strings[gameString] = stringValue;
                    }
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the game string.
        /// </summary>
        /// <param name="gameString">The game string to convert to text</param>
        /// <returns>The textual representation of the string</returns>
        public static string GetString(GameString gameString)
        {
            if (Strings.ContainsKey(gameString))
            {
                return Strings[gameString];
            }

            return gameString.ToString();
        }

        /// <summary>
        /// Sets the default strings if no translations can be found from the server.
        /// </summary>
        private static void SetDefaultStrings()
        {
            Strings[GameString.Generic_Info_GameName] = "Subterfuge";
            Strings[GameString.Generic_Button_Back] = "Back";
            Strings[GameString.Generic_Button_Cancel] = "Cancel";
            Strings[GameString.Generic_Button_Submit] = "Submit";
            Strings[GameString.Generic_Info_Loading] = "Loading";
            Strings[GameString.Login_Button_Login] = "Login";
            Strings[GameString.Login_Error_Unverified] = "Invalid Username or Password";
            Strings[GameString.Login_Label_Password] = "Password";
            Strings[GameString.Login_Label_Username] = "Username";
            Strings[GameString.Register_Button_Register] = "Register";
            Strings[GameString.Register_Label_Email] = "Email";
            Strings[GameString.CreateGame_Label_Anonymous] = "Anonymous";
            Strings[GameString.CreateGame_Label_Players] = "Players";
            Strings[GameString.CreateGame_Label_Ranked] = "Ranked";
            Strings[GameString.CreateGame_Label_Title] = "Create Game";
            Strings[GameString.Game_EventPanel_Title] = "Events";
            Strings[GameString.Game_GuiPanel_Chat] = "Chat";
            Strings[GameString.Game_GuiPanel_Events] = "Events";
            Strings[GameString.Game_GuiPanel_Log] = "Log";
            Strings[GameString.Game_GuiPanel_Statistics] = "Stats";
            Strings[GameString.GameLobby_Label_Anonymous] = "Anonymous";
            Strings[GameString.GameLobby_Label_Title] = "Title";
            Strings[GameString.GameSelect_Button_Ended] = "Ended";
            Strings[GameString.GameSelect_Button_Ongoing] = "Ongoing";
            Strings[GameString.GameSelect_Button_Open] = "Open";
            Strings[GameString.GameSelect_Button_Players] = "Players";
            Strings[GameString.GameSelect_Label_Title] = "Title";
            Strings[GameString.Login_Button_CreateAccount] = "Create Account";
            
            // TODO: Change this string based on username and password requirements
            Strings[GameString.Login_Error_InputError] = "TODO: Change this based on input requirements";
            Strings[GameString.Login_Error_InvalidCredentials] = "Invalid username or password";
            Strings[GameString.Login_Error_NoConnectivity] = "Unable to connect to the internet. Please check your connection.";
            Strings[GameString.Login_Info_NoAccount] = "No account?";
            Strings[GameString.MainMenu_Button_Singleplayer] = "Singleplayer";
            Strings[GameString.MainMenu_Button_Account] = "Account";
            Strings[GameString.MainMenu_Button_Help] = "Help";
            Strings[GameString.MainMenu_Button_Multiplayer] = "Multiplayer";
            Strings[GameString.MainMenu_Button_Puzzles] = "Puzzles";
            Strings[GameString.MainMenu_Button_Settings] = "Settings";
            // TODO: Update email requirements
            Strings[GameString.Register_Error_InvalidEmail] = "The email you have entered is invalid";
            // TODO: Update password requirements
            Strings[GameString.Register_Error_InvalidPassword] = "The password does not meet the password requirements.";
            // TODO: Update username requirements
            Strings[GameString.Register_Error_InvalidUsername] = "Your username must be between 8-20 characters";
            Strings[GameString.CreateGame_Button_CreateGame] = "Create Game";
            Strings[GameString.CreateGame_Title_CreateGame] = "Create Game";
            Strings[GameString.CreateGame_Label_MinutesPerTick] = "Minutes per tick";
            Strings[GameString.CreateGame_Label_Creator] = "Created By";
            Strings[GameString.GameSelect_Button_CreateGame] = "Create Game";
            Strings[GameString.Game_ChatPanel_NewChat] = "New Chat";
            Strings[GameString.Game_ChatPanelTitle_Chats] = "Chats";
            Strings[GameString.Game_LaunchPanelButton_Launch] = "Launch";
            Strings[GameString.GameLobby_Button_CancelGame] = "Cancel Game";
            Strings[GameString.GameLobby_Button_JoinGame] = "Join Game";
            Strings[GameString.GameLobby_Button_LeaveGame] = "Leave Game";
            Strings[GameString.GameLobby_Button_StartEarly] = "Start Early";
            Strings[GameString.Game_LaunchPanelTitle_LaunchSub] = "Launch Sub";
            Strings[GameString.GameLobby_Label_WaitingForPlayers] = "Waiting for players";
            
            
            // Ensures that all game strings have been populated.
            // When loading, if all strings are not populated, the `getString()` method will throw an error.
            foreach(GameString s in Enum.GetValues(typeof(GameString)))
            {
                GetString(s);
            }
        }
    }
}