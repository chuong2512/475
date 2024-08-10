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
using System.Collections;

public class TransformRandomizer : ScriptableWizard
{
	public float rotateAngle = 0;
	public Vector3 rotateAxis = Vector3.up;
	public Vector3 offset = Vector3.zero;

	[MenuItem("GameObject/Random Transform")]
	static void DisplayWizard ()
	{
		ScriptableWizard.DisplayWizard<TransformRandomizer> ("Random Transform", "Randomizer");
	}
	
	private void OnWizardCreate ()
	{
		Object[] objects = Selection.GetFiltered (typeof(Transform), SelectionMode.TopLevel);
		
		Undo.RecordObjects (objects, "Random Transform");
		for (int i = 0; i < objects.Length; i++) {
			Transform transform = objects [i] as Transform;
			
			if (rotateAngle != 0)
				transform.Rotate (rotateAxis, Random.Range (0, rotateAngle), Space.Self);

			if (offset != Vector3.zero)
				transform.position += new Vector3 (Random.Range (0, offset.x), Random.Range (0, offset.y), Random.Range (0, offset.z));
		}
	}
}
