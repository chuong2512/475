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
/// 鼠标Enter的时候令target enable，Exit的时候令target disable
/// </summary>
public class MouseOverEnable : MonoBehaviour
{
    #region Fields
    public MonoBehaviour target;
    public bool disableTargetWhenDisable = true;
    #endregion

    #region Properties

    #endregion

    #region Functions
    void OnDisable()
    {
        if (disableTargetWhenDisable && target != null) target.enabled = false;
    }

    void OnMouseEnter()
    {
        if (!enabled) return;
        target.enabled = true;
    }

    void OnMouseExit()
    {
        if (!enabled) return;
        target.enabled = false;
    }
    #endregion
}
