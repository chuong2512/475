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
using UnityEditor;

public class NamingUtilities : ScriptableWizard
{
	[MenuItem ("Assets/Rename Material by Main Texture", false, 90)]
	public static void NameMaterialByMainTexture ()
	{
		Object[] selectedMaterials = Selection.GetFiltered (typeof(Material), SelectionMode.Assets);
		foreach (Object o in selectedMaterials) {
			Material selectedMaterial = o as Material;
			if (selectedMaterial.mainTexture != null) {
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (selectedMaterial), selectedMaterial.mainTexture.name);
			}
		}
	}

	[MenuItem ("Assets/Format Asset Name", false, 91)]
	public static void FormatAssetName ()
	{
		Object[] selectedMaterials = Selection.objects;
		foreach (Object o in selectedMaterials) {
			string formatedName = o.name;
			formatedName = formatedName.ToLower ();
			formatedName = formatedName.Replace (" ", "_");
			if (formatedName != o.name)
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (o), formatedName);
		}
	}

	public string newNameFormat = "[N]";
	[StepperInt ()]
	public int counterStartNumber = 0;
	[StepperInt ()]
	public int counterDigits = 2;
	public string find = "";
	public string replace = "";

	[MenuItem ("GameObject/Batch Rename %m")]
	static void DisplayWizard ()
	{
		ScriptableWizard.DisplayWizard<NamingUtilities> ("Batch Rename", "Rename");
	}

	private void OnWizardCreate ()
	{
		Object[] objects = Selection.GetFiltered (typeof(Object), SelectionMode.TopLevel);
		
		Undo.RecordObjects (objects, "Batch Reanme");
		for (int i = 0; i < objects.Length; i++) {
			Object obj = objects [i];
			
			string oldName = obj.name;
			string newName = GetNewName (oldName, i);
			
			if (AssetDatabase.IsMainAsset (obj))
				AssetDatabase.RenameAsset (AssetDatabase.GetAssetPath (obj), newName);
			else
				obj.name = newName;
		}
	}

	private void OnWizardUpdate ()
	{
		helpString = "";

		Object[] objects = Selection.GetFiltered (typeof(Object), SelectionMode.TopLevel);
		if (objects.Length > 0) {
			int maxSampleCount = 5;
			for (int i = 0; i < Mathf.Min (maxSampleCount, objects.Length); i++) {
				helpString += string.Format ("{0} => {1}", objects [i].name, GetNewName (objects [i].name, i));
				helpString += "\n";
			}
			helpString += "\n";
			helpString += "Available Codes:\n";
			helpString += "- [N]: Current Name\n";
			helpString += "- [C]: Counter Number\n";
		} else
			helpString = "Select at least 1 object please.";
	}

	private string GetNewName (string oldName, int index)
	{
		if (find != "")
			oldName = oldName.Replace (find, replace);
			
		string newName = newNameFormat.Replace ("[N]", oldName);
		
		string digitString = string.Format (GetDigitFormatString (), index + counterStartNumber);
		newName = newName.Replace ("[C]", digitString);
			
		return newName;
	}

	private string GetDigitFormatString ()
	{
		string digitFormatString = "{0:";
		
		counterDigits = Mathf.Max (1, counterDigits);
		for (int i = 0; i < counterDigits; i++)
			digitFormatString += "0";
		digitFormatString += "}";
		
		return digitFormatString;
	}
}
