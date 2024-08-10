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

public class MouseMovementInput : GenericInputSender
{
    #region Fields
    public enum MouseButtonState { DONT_CARE, L_BUTTON_DOWN, L_BUTTON_NOT_DOWN };
    public MouseButtonState mouseState = MouseButtonState.DONT_CARE;
    public float strength = 1;
    protected float mouseMoveX = 0;
    protected float mouseMoveY = 0;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if(reciever == null)return;
        if (mouseState == MouseButtonState.L_BUTTON_DOWN && !ControlFreak2.CF2Input.GetMouseButton(0)) return; // Mouse drag only
        if (mouseState == MouseButtonState.L_BUTTON_NOT_DOWN && ControlFreak2.CF2Input.GetMouseButton(0)) return;
        mouseMoveX = ControlFreak2.CF2Input.GetAxis("Mouse X");
        mouseMoveY = ControlFreak2.CF2Input.GetAxis("Mouse Y");
        reciever.OnDirectionInput(mouseMoveX * strength, mouseMoveY * strength, 0);
    }
    #endregion
}
