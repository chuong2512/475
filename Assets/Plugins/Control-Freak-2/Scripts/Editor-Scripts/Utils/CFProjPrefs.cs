/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using ControlFreak2Editor.Internal;


namespace  ControlFreak2Editor
{

[System.Serializable]
public class CFProjPrefs 
	{
	private const string SETTINGS_PATH_REL_TO_PROJ = "CF2-Data/Settings/CF2.config";

	public string	
		projectPath;
	public bool		
		isIsntalled,
		wasShown;
	public int		
		installedVer;



	// -------------------
	void OnEnable()
		{
//Debug.Log("Prefs OnEnable");
		}	


	// --------------------
	static private CFProjPrefs mInst;
	
	static public CFProjPrefs Inst 
		{
		get {
			if (mInst == null)
				{
				mInst = Load(); 
				}
			
			return mInst;
			}
		}

	
	// ------------------
	static public string GetFullPath()
		{
		return CFEditorUtils.GetProjectPath() + SETTINGS_PATH_REL_TO_PROJ;
		}
	
	// -----------------
	public static CFProjPrefs Load() 
		{
		string fullPath = GetFullPath(); 

		CFEditorUtils.EnsureDirectoryExists(fullPath);

		CFProjPrefs o = CFEditorUtils.LoadObjectFromXml(fullPath, typeof(CFProjPrefs)) as CFProjPrefs; //LoadXml<CFProjPrefs>(fullPath);
		return ((o == null) ? (new CFProjPrefs()) : o);
		}


	// -----------------
	public void Save()
		{
		CFEditorUtils.SaveObjectAsXml(GetFullPath(), this, this.GetType());
		}

	}
}

#endif
