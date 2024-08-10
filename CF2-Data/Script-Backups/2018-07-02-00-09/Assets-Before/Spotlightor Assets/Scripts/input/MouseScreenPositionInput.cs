using UnityEngine;
using System.Collections;

public class MouseScreenPositionInput : GenericInputSender
{
    #region Fields
    /// <summary>
    /// ����Ļ���ĵ���Ϊ(0,0)�������������ұ�Ե�ľ���Ϊ1�������ĵ������İ�ȫ��Χ
    /// </summary>
    public float saveRange = 0.6f;
    public float strength = 1;
    #endregion

    #region Properties

    #endregion

    #region Functions

    void Update()
    {
        if (reciever == null) return;

        float mx = ((Input.mousePosition.x / Screen.width) - 0.5f) * 2;
        float my = ((Input.mousePosition.y / Screen.height) - 0.5f) * 2;
        mx = Mathf.Clamp(mx, -1f, 1f);
        my = Mathf.Clamp(my, -1f, 1f);
        float dx = 0;
        float dy = 0;
        float oneMinusRange = 1 - saveRange;
        if (Mathf.Abs(mx) > saveRange) dx = mx > 0 ? (mx - saveRange) / oneMinusRange : (mx + saveRange) / oneMinusRange;
        if (Mathf.Abs(my) > saveRange) dy = my > 0 ? (my - saveRange) / oneMinusRange : (my + saveRange) / oneMinusRange;

        if(dx != 0 || dy != 0) reciever.OnDirectionInput(dx * strength, dy * strength, 0);
    }
    #endregion
}
