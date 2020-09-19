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
        Generic_Info_Loading,
        Generic_Button_Back,
        Generic_Button_Cancel,
        Generic_Button_Submit,
        
        // Login Scene Strings
        Login_Label_Username,
        Login_Label_Password,
        Login_Button_Login,
        Login_Error_InvalidCredentials,
        Login_Error_NoConnectivity,
        Login_Error_InputError,
        Login_Error_Unverified,
        Login_Info_NoAccount,
        Login_Button_CreateAccount,
        
        // Registration Scene strings
        Register_Label_Email,
        Register_Button_Register,
        Register_Error_InvalidUsername,
        Register_Error_InvalidPassword,
        Register_Error_InvalidEmail,
        
        // Main Menu
        MainMenu_Button_Multiplayer,
        MainMenu_Button_Settings,
        MainMenu_Button_Help,
        MainMenu_Button_Puzzles,
        MainMenu_Button_Account,
        
        // Game Select Scene
        GameSelect_Label_Title,
        GameSelect_Button_Ongoing,
        GameSelect_Button_Open,
        GameSelect_Button_Ended,
        GameSelect_Button_Players,
        GameSelect_Button_CreateGame,
        
        // Create Game
        CreateGame_Title_CreateGame,
        CreateGame_Label_Title,
        CreateGame_Label_Players,
        CreateGame_Label_Ranked,
        CreateGame_Label_Anonymous,
        CreateGame_Button_CreateGame,
        
        // Game Lobby
        GameLobby_Label_Title,
        GameLobby_Label_Anonymous,
        GameLobby_Button_StartEarly,
        GameLobby_Button_LeaveGame,
        GameLobby_Label_WaitingForPlayers,
        GameLobby_Button_CancelGame,
        GameLobby_Button_JoinGame,
        
        // In game
        Game_GuiPanel_Chat,
        Game_GuiPanel_Events,
        Game_GuiPanel_Statistics,
        Game_GuiPanel_Log,
        Game_LaunchPanelTitle_LaunchSub,
        Game_LaunchPanelButton_Launch,
        Game_ChatPanelTitle_Chats,
        Game_ChatPanel_NewChat,
        Game_EventPanel_Title,
    }
}