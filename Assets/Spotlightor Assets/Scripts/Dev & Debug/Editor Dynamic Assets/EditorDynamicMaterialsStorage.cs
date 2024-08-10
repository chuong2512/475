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
using System.Collections.Generic;

public static class EditorDynamicMaterialsStorage
{
	#if UNITY_EDITOR
	private static Dictionary<Material, Material> dynamicMaterialDictionary = new Dictionary<Material, Material> ();
	#endif

	public static Material GetDynamicMaterial (Material material)
	{
		#if UNITY_EDITOR
		Material replacementMaterial = null;
		if (dynamicMaterialDictionary.ContainsValue (material))
			replacementMaterial = material;
		else {
			if (dynamicMaterialDictionary.TryGetValue (material, out replacementMaterial) == false) {
				replacementMaterial = new Material (material);
				replacementMaterial.name = "RUNTIME " + material.name;

				dynamicMaterialDictionary [material] = replacementMaterial;
			}
		}
		return replacementMaterial;
		#endif
		#if !UNITY_EDITOR
		return material;
		#endif
	}
}
