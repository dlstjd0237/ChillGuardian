using UnityEngine;
namespace BIS.Core
{
    public static class Define
    {
        public const float RecoredTime = 3.0f;

        public enum ESceneType
        {
            Title,
            GameMenu,
            InGame,
            Unknown
        }

        public static class MLayerMask
        {
            public static readonly LayerMask WhatIsGround = LayerMask.GetMask("Ground");
            public static readonly LayerMask WhatIsPlayer = LayerMask.GetMask("Player");
            public static readonly LayerMask WhatIsEnemy = LayerMask.GetMask("Enemy");
        }

        public static class SceneName
        {
            public static readonly string TitleScene = "Title";
            public static readonly string ChoiceScene = "ChoiceScene";
            public static readonly string GameScene = "GameScene";

        }

        public enum EUIEventType
        {
            DOWN,
            MOVE,
            ENTER,
            EXIT,
            CLICK
        }

        public enum EObjectTag
        {
            Player,
            Enemy,
            All
        }
        public enum EItemRarity
        {
            General,
            Rare,
            Epic,
            Legend
        }

        public enum EUIEvent
        {
            Click,
            PointerDown,
            PointerUp,
            Drag,
            PointerEnter,
            PointerExit
        }
        public enum EGuyType
        {
            ChillGuy,
            Chill,
            Winston_S_Churchill,
            CaChill,
            Chill_Mk_1
        }
    }
}
