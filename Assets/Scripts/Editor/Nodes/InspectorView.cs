using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace Combustion.Editor.Nodes
{
	public class InspectorView : VisualElement
	{
		public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

		UnityEditor.Editor editor;

		public InspectorView() {
			
		}

		public void UpdateSelection(NodeView nodeView) {
			Clear();
			UnityEngine.Object.DestroyImmediate(editor);
			

			editor = UnityEditor.Editor.CreateEditor(nodeView.node);
			IMGUIContainer container = new IMGUIContainer(() =>
			{
				editor.OnInspectorGUI();
			});

			Add(container);
		}
	}
}
