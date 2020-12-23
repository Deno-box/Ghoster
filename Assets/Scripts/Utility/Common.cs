using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Scene
    {
        public enum SceneNum
        {
            TITLE_SCENE,
            STAGE_SELECT_SCENE,
            PLAY_SCENE,
            RESULT_SCENE
        }
        public const int TITLE_SCENE        = 0;
        public const int STAGE_SELECT_SCENE = 1;
        public const int PLAY_SCENE         = 2;
        public const int RESULT_SCENE       = 3;
    }
}
