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

[CustomEditor(typeof(TexturesCompositor))]
public class TexturesCompositorEditor : Editor
{
	private TexturesCompositor Compositor{ get { return target as TexturesCompositor; } }

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		if (GUILayout.Button ("Composite")) {
			Texture2D compositeTexture = Compositor.Composite ();

			string path = AssetDatabase.GetAssetPath (Compositor);
			string folderPath = path.Substring (0, path.LastIndexOf ("/") + 1);
			string texturePath = folderPath + Compositor.name.ToLower () + ".png";
			
			SaveAssetUtility.SaveTexture (compositeTexture, texturePath);
		}
	}
}
