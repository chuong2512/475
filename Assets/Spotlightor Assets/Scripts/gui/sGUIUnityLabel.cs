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

/// <summary>
/// ʹ��������GUIText�ķ��������Unity GUI.Label����ô�����ԵĹ��ܣ���Ҫ��Ϊ����Label��Word Wrap�������ֲ�GUIText�Ĳ���
/// </summary>
[ExecuteInEditMode()]
public class sGUIUnityLabel : sGUIBase
{
    #region Fields
    public string text;
    #endregion

    #region Properties

    #endregion

    #region Functions
    protected override void DrawGUI(Rect drawRect, GUIStyle guiStyle)
    {
        if (guiStyle != null)
        {
            GUI.Label(drawRect, text, guiStyle);
        }
        else GUI.Label(drawRect, text);
    }
    #endregion
}
