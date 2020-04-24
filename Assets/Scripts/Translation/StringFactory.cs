using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
        public static Language Language { get; set; }
        
        /// <summary>
        /// A dictionary of strings to display on the screen.
        /// The dictionary is set by default to english if it cannot load.
        /// The strings should be loaded before use.
        /// </summary>
        public static Dictionary<GameString, string> strings { get; set; }

        /// <summary>
        /// Load the strings from our database or values.
        /// If unable to load any strings, the defaults are loaded.
        /// </summary>
        public void loadStrings()
        {
            // TODO: Add the Google sheets API and query from a google sheet
            this.setDefaultStrings();
        }

        /// <summary>
        /// Gets a string representation of the game string.
        /// </summary>
        /// <param name="gameString">The game string to convert to text</param>
        /// <returns>The textual representation of the string</returns>
        public static string getString(GameString gameString)
        {
            if (strings.ContainsKey(gameString))
            {
                return strings[gameString];
            }
            throw new Exception("String does not exist.");
        }

        /// <summary>
        /// Gets the language representation of the string. For example "en" for english.
        /// This is used for a lookup of the string
        /// </summary>
        /// <returns>The language code</returns>
        private string getLanguageString()
        {
            // TODO: Determine the language shorthand notations from the crowd sourced translation tool
            return Language.ToString();
        }

        private void setDefaultStrings()
        {
            strings[GameString.Generic_Button_Back] = "Back";
            strings[GameString.Generic_Button_Cancel] = "Cancel";
            strings[GameString.Generic_Button_Submit] = "Submit";
            strings[GameString.Generic_Info_Loading] = "Loading";
            strings[GameString.Login_Button_Login] = "Login";
            strings[GameString.Login_Error_Unverified] = "Invalid Username or Password";
            strings[GameString.Login_Label_Password] = "Password";
            strings[GameString.Login_Label_Username] = "Username";
            strings[GameString.Register_Button_Register] = "Register";
            strings[GameString.Register_Label_Email] = "Email";
            strings[GameString.CreateGame_Label_Anonymous] = "Anonymous";
            strings[GameString.CreateGame_Label_Players] = "Players";
            strings[GameString.CreateGame_Label_Ranked] = "Ranked";
            strings[GameString.CreateGame_Label_Title] = "Create Game";
            strings[GameString.Game_EventPanel_Title] = "Events";
            strings[GameString.Game_GuiPanel_Chat] = "Chat";
            strings[GameString.Game_GuiPanel_Events] = "Events";
            strings[GameString.Game_GuiPanel_Log] = "Log";
            strings[GameString.Game_GuiPanel_Statistics] = "Stats";
            strings[GameString.GameLobby_Label_Anonymous] = "Anonymous";
            strings[GameString.GameLobby_Label_Title] = "Title";
            strings[GameString.GameSelect_Button_Ended] = "Ended";
            strings[GameString.GameSelect_Button_Ongoing] = "Ongoing";
            strings[GameString.GameSelect_Button_Open] = "Open";
            strings[GameString.GameSelect_Button_Players] = "Players";
            strings[GameString.GameSelect_Label_Title] = "Title";
            strings[GameString.Login_Button_CreateAccount] = "Create Account";
            
            // TODO: Change this string based on username and password requirements
            strings[GameString.Login_Error_InputError] = "TODO: Change this based on input requirements";
            strings[GameString.Login_Error_InvalidCredentials] = "Invalid username or password";
            strings[GameString.Login_Error_NoConnectivity] = "Unable to connect to the internet. Please check your connection.";
            strings[GameString.Login_Info_NoAccount] = "No account?";
            strings[GameString.MainMenu_Button_Account] = "Account";
            strings[GameString.MainMenu_Button_Help] = "Help";
            strings[GameString.MainMenu_Button_Multiplayer] = "Multiplayer";
            strings[GameString.MainMenu_Button_Puzzles] = "Puzzles";
            strings[GameString.MainMenu_Button_Settings] = "Settings";
            // TODO: Update email requirements
            strings[GameString.Register_Error_InvalidEmail] = "The email you have entered is invalid";
            // TODO: Update password requirements
            strings[GameString.Register_Error_InvalidPassword] = "The password does not meet the password requirements.";
            // TODO: Update username requirements
            strings[GameString.Register_Error_InvalidUsername] = "Your username must be between 8-20 characters";
            strings[GameString.CreateGame_Button_CreateGame] = "Create Game";
            strings[GameString.CreateGame_Title_CreateGame] = "Create Game";
            strings[GameString.GameSelect_Button_CreateGame] = "Create Game";
            strings[GameString.Game_ChatPanel_NewChat] = "New Chat";
            strings[GameString.Game_ChatPanelTitle_Chats] = "Chats";
            strings[GameString.Game_LaunchPanelButton_Launch] = "Launch";
            strings[GameString.GameLobby_Button_CancelGame] = "Cancel Game";
            strings[GameString.GameLobby_Button_JoinGame] = "Join Game";
            strings[GameString.GameLobby_Button_LeaveGame] = "Leave Game";
            strings[GameString.GameLobby_Button_StartEarly] = "Start Early";
            strings[GameString.Game_LaunchPanelTitle_LaunchSub] = "Launch Sub";
            strings[GameString.GameLobby_Label_WaitingForPlayers] = "Waiting for players";
        }
    }
}