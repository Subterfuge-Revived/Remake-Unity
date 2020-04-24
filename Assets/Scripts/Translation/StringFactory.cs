using System.Runtime.CompilerServices;

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
            return getDefaultString(gameString);
        }

        private string getLanguageString()
        {
            // TODO: Determine the language shorthand notations from the crowd sourced translation tool
            return Language.ToString();
        }

        private static string getDefaultString(GameString gameString)
        {
            switch (gameString)
            {
                case GameString.Generic_Button_Back:
                    return "Back";
                case GameString.Generic_Button_Cancel:
                    return "Cancel";
                case GameString.Generic_Button_Submit:
                    return "Submit";
                case GameString.Generic_Info_Loading:
                    return "Loading";
                case GameString.Login_Button_Login:
                    return "Login";
                case GameString.Login_Error_Unverified:
                    return "Invalid Username or Password";
                case GameString.Login_Label_Password:
                    return "Password";
                case GameString.Login_Label_Username:
                    return "Username";
                case GameString.Register_Button_Register:
                    return "Register";
                case GameString.Register_Label_Email:
                    return "Email";
                case GameString.CreateGame_Label_Anonymous:
                    return "Anonymous";
                case GameString.CreateGame_Label_Players:
                    return "Players";
                case GameString.CreateGame_Label_Ranked:
                    return "Ranked";
                case GameString.CreateGame_Label_Title:
                    return "Create Game";
                case GameString.Game_EventPanel_Title:
                    return "Events";
                case GameString.Game_GuiPanel_Chat:
                    return "Chat";
                case GameString.Game_GuiPanel_Events:
                    return "Events";
                case GameString.Game_GuiPanel_Log:
                    return "Log";
                case GameString.Game_GuiPanel_Statistics:
                    return "Stats";
                case GameString.GameLobby_Label_Anonymous:
                    return "Anonymous";
                case GameString.GameLobby_Label_Title:
                    return "Title";
                case GameString.GameSelect_Button_Ended:
                    return "Ended";
                case GameString.GameSelect_Button_Ongoing:
                    return "Ongoing";
                case GameString.GameSelect_Button_Open:
                    return "Open";
                case GameString.GameSelect_Button_Players:
                    return "Players";
                case GameString.GameSelect_Label_Title:
                    return "Title";
                case GameString.Login_Button_CreateAccount:
                    return "Create Account";
                case GameString.Login_Error_InputError:
                    // TODO: Change this string based on username and password requirements
                    return "TODO: Change this based on input requirements";
                case GameString.Login_Error_InvalidCredentials:
                    return "Invalid username or password";
                case GameString.Login_Error_NoConnectivity:
                    return "Unable to connect to the internet. Please check your connection.";
                case GameString.Login_Info_NoAccount:
                    return "No account?";
                case GameString.MainMenu_Button_Account:
                    return "Account";
                case GameString.MainMenu_Button_Help:
                    return "Help";
                case GameString.MainMenu_Button_Multiplayer:
                    return "Multiplayer";
                case GameString.MainMenu_Button_Puzzles:
                    return "Puzzles";
                case GameString.MainMenu_Button_Settings:
                    return "Settings";
                case GameString.Register_Error_InvalidEmail:
                    // TODO: That is not a valid email
                    return "The email you have entered is invalid";
                case GameString.Register_Error_InvalidPassword:
                    // TODO: Update password requirements
                    return "The password does not meet the password requirements.";
                case GameString.Register_Error_InvalidUsername:
                    // TODO: Update username requirements
                    return "Your username must be between 8-20 characters";
                case GameString.CreateGame_Button_CreateGame:
                case GameString.CreateGame_Title_CreateGame:
                case GameString.GameSelect_Button_CreateGame:
                    return "Create Game";
                case GameString.Game_ChatPanel_NewChat:
                    return "New Chat";
                case GameString.Game_ChatPanelTitle_Chats:
                    return "Chats";
                case GameString.Game_LaunchPanelButton_Launch:
                    return "Launch";
                case GameString.GameLobby_Button_CancelGame:
                    return "Cancel Game";
                case GameString.GameLobby_Button_JoinGame:
                    return "Join Game";
                case GameString.GameLobby_Button_LeaveGame:
                    return "Leave Game";
                case GameString.GameLobby_Button_StartEarly:
                    return "Start Early";
                case GameString.Game_LaunchPanelTitle_LaunchSub:
                    return "Launch Sub";
                case GameString.GameLobby_Label_WaitingForPlayers:
                    return "Waiting for players";

            }
        }
    }
}