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

public abstract class sGUIBase : MonoBehaviour
{
    #region Fields
    public GUISkin guiSkin;
    public string styleName;
    public Rect pixelInset = new Rect(0, 0, 100, 30);

    #endregion

    #region Properties
	public GUIStyle guiStyle {
		get { return guiSkin != null ? guiSkin.GetStyle(styleName) : null; }
	}
    #endregion

    #region Functions

    void OnGUI()
    {
        GUIStyle guiStyle = null;
        if (guiSkin != null && styleName.Length > 0) guiStyle = guiSkin.GetStyle(styleName);

        if (guiSkin != null) GUI.skin = guiSkin;

        Vector3 localPos = transform.position;
        GUI.depth = Mathf.FloorToInt(localPos.z); // depth by z
        Rect drawRect = new Rect(localPos.x * Screen.width + pixelInset.x, (1 - localPos.y) * Screen.height - pixelInset.y, pixelInset.width, pixelInset.height); // draw rect by x,y and inset

        DrawGUI(drawRect, guiStyle);
    }
	
    protected abstract void DrawGUI(Rect drawRect, GUIStyle guiStyle);
    #endregion
}
