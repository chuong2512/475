/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;
using System.IO;

[RequireComponent(typeof(Camera))]
public class HiResScreenShots : MonoBehaviour
{
    public KeyCode key = KeyCode.K;
    public int resWidth = 4096;
    public int resHeight = 2232;
    public bool transparent = true;

    void Update()
    {
        if (ControlFreak2.CF2Input.GetKeyDown(key))
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;

            TextureFormat tf = transparent ? TextureFormat.ARGB32 : TextureFormat.RGB24;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, tf, false);

            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt);
			#if UNITY_STANDALONE_WIN
            byte[] bytes = screenShot.EncodeToPNG();
            string dir = Application.dataPath + "/screenshots/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string filename = dir + "screen" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
			
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
			#endif
			#if UNITY_STANDALONE_OSX
            byte[] bytes = screenShot.EncodeToPNG();
            string dir = Application.dataPath + "/screenshots/";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string filename = dir + "screen" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
			
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
			#endif
        }
    }
}
