/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using SgLib.Editor;
using System.Collections;

namespace EasyMobile.Editor
{
    public class EM_About : EditorWindow
    {
        const string MAIN_IMAGE_PATH = EM_Constants.SkinTextureFolder + "/about-image.psd";
        const int WINDOW_WIDTH = 400;
        const int WINDOW_HEIGHT = 200;
        const int IMAGE_WIDTH = 400;
        const int IMAGE_HEIGHT = 160;

        Texture2D mainImage;

        void OnEnable()
        {
            // Set the window title
            #if UNITY_PRE_5_1
            title = "About";
            #else
            titleContent = new GUIContent("About");            
            #endif

            // Load the main image
            mainImage = AssetDatabase.LoadAssetAtPath(MAIN_IMAGE_PATH, typeof(Texture2D)) as Texture2D;

            // Set sizes
            Vector2 fixedSizes = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
            maxSize = fixedSizes;
            minSize = fixedSizes;
        }

        void OnGUI()
        {
            GUILayout.BeginVertical();
            if (mainImage != null)
                GUI.DrawTexture(new Rect(0f, 0f, IMAGE_WIDTH, IMAGE_HEIGHT), mainImage, ScaleMode.ScaleAndCrop);

            GUILayout.FlexibleSpace();
            GUILayout.Label("Version " + EM_Constants.versionString);
            GUILayout.Label(EM_Constants.Copyright);

            GUILayout.EndVertical();
        }
    }
}
