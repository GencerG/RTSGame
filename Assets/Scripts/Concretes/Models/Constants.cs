using UnityEngine;

namespace RTSGame.Concretes.Models
{
    public static class Constants
    {
        public readonly struct SCENE_INDEXES
        {
            public const int INIT_SCENE = 0;
            public const int MAIN_MENU_SCENE = 1;
            public const int BATTLE_SCENE = 2;
        }

        public readonly struct GAME_CONFIGS
        {
            public const int DECK_SIZE = 3;
            public const float HOLD_DURATION = 1.2f;
            public const float ATTACK_ANIMATION_DURATION = .6f;
            public const int PLAY_COUNT_REWARD = 5;
            public const int EXPERIENCE_TO_LEVEL = 5;
            public const int LEVEL_UP_MODIFIER = 10;
        }

        public readonly struct TAGS
        {
            public const string BATTLE_UNIT_TAG = "BattleUnit";
        }
    }
}