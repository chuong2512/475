/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System;

namespace UnityEngine.PostProcessing
{
    [Serializable]
    public class UserLutModel : PostProcessingModel
    {
        [Serializable]
        public struct Settings
        {
            [Tooltip("Custom lookup texture (strip format, e.g. 256x16).")]
            public Texture2D lut;

            [Range(0f, 1f), Tooltip("Blending factor.")]
            public float contribution;

            public static Settings defaultSettings
            {
                get
                {
                    return new Settings
                    {
                        lut = null,
                        contribution = 1f
                    };
                }
            }
        }

        [SerializeField]
        Settings m_Settings = Settings.defaultSettings;
        public Settings settings
        {
            get { return m_Settings; }
            set { m_Settings = value; }
        }

        public override void Reset()
        {
            m_Settings = Settings.defaultSettings;
        }
    }
}
