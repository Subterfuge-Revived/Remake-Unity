namespace Translation
{
    /// <summary>
    /// An enumeration of all of the in-game strings. This class is used to determine which string the user would like
    /// to be displayed to the screen. The enumeration is structured as follows:
    ///
    /// SCENE_CATEGORY_STRING
    /// </summary>
    public enum GameString
    {
        
        // Generic strings that repeat through the game
        Generic_Info_GameName = 1,
        Generic_Info_Loading = 2,
        Generic_Button_Back = 3,
        Generic_Button_Cancel = 4,
        Generic_Button_Submit = 5,
        
        // Login Scene Strings
        Login_Label_Username = 6,
        Login_Label_Password = 7,
        Login_Button_Login = 8,
        Login_Error_InvalidCredentials = 9,
        Login_Error_NoConnectivity = 10,
        Login_Error_InputError = 11,
        Login_Error_Unverified = 12,
        Login_Info_NoAccount = 13,
        Login_Button_CreateAccount = 14,
        
        // Registration Scene strings
        Register_Label_Email = 15,
        Register_Button_Register = 16,
        Register_Error_InvalidUsername = 17,
        Register_Error_InvalidPassword = 18,
        Register_Error_InvalidEmail = 19,
        
        // Main Menu
        MainMenu_Button_Singleplayer = 20,
        MainMenu_Button_Multiplayer = 21,
        MainMenu_Button_Settings = 22,
        MainMenu_Button_Help = 23,
        MainMenu_Button_Puzzles = 24,
        MainMenu_Button_Account = 25,
        
        // Game Select Scene
        GameSelect_Label_Title = 26,
        GameSelect_Button_Ongoing = 27,
        GameSelect_Button_Open = 28,
        GameSelect_Button_Ended = 29,
        GameSelect_Button_Players = 30,
        GameSelect_Button_CreateGame = 31,
        
        // Create Game
        CreateGame_Title_CreateGame = 32,
        CreateGame_Label_Title = 33,
        CreateGame_Label_Players = 34,
        CreateGame_Label_Ranked = 35,
        CreateGame_Label_Anonymous = 36,
        CreateGame_Button_CreateGame = 37,
        CreateGame_Label_MinutesPerTick = 38,
        
        // Game Lobby
        GameLobby_Label_Title = 39,
        GameLobby_Label_Anonymous = 40,
        GameLobby_Button_StartEarly = 41,
        GameLobby_Button_LeaveGame = 42,
        GameLobby_Label_WaitingForPlayers = 43,
        GameLobby_Button_CancelGame = 44,
        GameLobby_Button_JoinGame = 45,
        
        // In game
        Game_GuiPanel_Chat = 46,
        Game_GuiPanel_Events = 47,
        Game_GuiPanel_Statistics = 48,
        Game_GuiPanel_Log = 49,
        Game_LaunchPanelTitle_LaunchSub = 50,
        Game_LaunchPanelButton_Launch = 51,
        Game_ChatPanelTitle_Chats = 52,
        Game_ChatPanel_NewChat = 53,
        Game_EventPanel_Title = 54,
        
        CreateGame_Label_Creator = 55,
    }
}