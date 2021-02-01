using SubterfugeCore.Core.GameEvents;
using SubterfugeRemakeService;

namespace Rooms.Game
{
    public class GameLoader
    {
        public void loadMultiplayerGame()
        {

            if (!ApplicationState.Client.isConnected)
            {
                // Show info here to try reconnect.
            }
            else
            {
                var gameEvents = ApplicationState.Client.getClient().GetGameRoomEvents(
                    new GetGameRoomEventsRequest()
                    {
                        RoomId = ApplicationState.currentGameRoom.RoomId,
                    });

                if (gameEvents.Status.IsSuccess)
                {
                    // Parse game events here.
                    foreach (GameEventModel gameEvent in gameEvents.GameEvents)
                    {
                        // convert to a game event
                        LaunchEvent launch = LaunchEvent.FromJson(gameEvent.EventData);
                        SubterfugeCore.Core.Game.TimeMachine.AddEvent(launch);
                    }
                } else {
                    // TODO: Tell the user that they are offline or an error occurred.
                }
            }
        }

        public void loadSinglePlayerGame()
        {
            
        }
    }
}