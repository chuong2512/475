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

public class MouseWheelInput : GenericInputSender
{
    #region Fields
    public Vector3 inputAxis = Vector3.forward;
    public float strength = 1;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if(reciever == null)return;
        float scroll = ControlFreak2.CF2Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 input = inputAxis * scroll * strength;
            reciever.OnDirectionInput(input.x, input.y, input.z);
        }
    }
    #endregion
}
