using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.Editor.BattleNodes.Utilities
{
	public static class BattleStyleUtility {
		public static VisualElement AddClasses(this VisualElement element, params string[] classNames) {
			foreach (string className in classNames)
			{
				element.AddToClassList(className);
			}

			return element;
		}

		public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheets) {
			foreach (var styleSheet in styleSheets)
			{
				element.styleSheets.Add(EditorGUIUtility.Load(styleSheet) as StyleSheet);
			}

			return element;
		}

	}
}