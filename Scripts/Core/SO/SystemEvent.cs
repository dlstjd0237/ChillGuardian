using static BIS.Core.Define;

namespace BIS.Core
{

    public static class SystemEvent
    {
        public static readonly GameOverEvent GameOverEvent = new GameOverEvent();
        public static readonly GameClearEvent GameClearEvent = new GameClearEvent();
        public static readonly AddChillEvent AddChillEvent = new AddChillEvent();
    }

    public class GameOverEvent : GameEvent
    {

    }

    public class GameClearEvent : GameEvent
    {

    }

    public class AddChillEvent : GameEvent
    {
        public EGuyType addType;
    }

}
