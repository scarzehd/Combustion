using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Combustion.BattleNodes
{
	using Elements;

	public class BattleNodeSearchWindow : ScriptableObject, ISearchWindowProvider
	{
		private BattleNodeGraphView graphView;

		private Texture2D indentationIcon;

		public void Initialize(BattleNodeGraphView graphView) {
			this.graphView = graphView;

			indentationIcon = new Texture2D(1, 1);
			indentationIcon.SetPixel(0, 0, Color.clear);
			indentationIcon.Apply();
		}

		public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context) {
			List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>();

			searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Add Node")));

			{
				searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Action Nodes"), 1));

				var types = TypeCache.GetTypesDerivedFrom(typeof(ActionNode));

				foreach (Type type in types)
				{
					searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", indentationIcon))
					{
						level = 2,
						userData = type
					});
				}
			}
			{
				searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Modify Nodes"), 1));

				var types = TypeCache.GetTypesDerivedFrom(typeof(ModifyNode));

				foreach (Type type in types)
				{
					searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", indentationIcon))
					{
						level = 2,
						userData = type
					});
				}
			}
			{
				searchTreeEntries.Add(new SearchTreeGroupEntry(new GUIContent("Composite Nodes"), 1));

				var types = TypeCache.GetTypesDerivedFrom(typeof(CompositeNode));

				foreach (Type type in types)
				{
					searchTreeEntries.Add(new SearchTreeEntry(new GUIContent($"{type.Name}", indentationIcon))
					{
						level = 2,
						userData = type
					});
				}
			}

			searchTreeEntries.Add(new SearchTreeEntry(new GUIContent("Node Group"))
			{
				level = 1,
				userData = typeof(BattleGroup)
			});

			return searchTreeEntries;
		}

		public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context) {
			if (searchTreeEntry.userData as Type == typeof(BattleGroup))
			{
				graphView.CreateGroup("Group", graphView.GetLocalMousePosition(context.screenMousePosition, true));

				return true;
			}
			
			BattleNode battleNode = graphView.CreateNode(searchTreeEntry.userData as Type, graphView.GetLocalMousePosition(context.screenMousePosition, true));

			graphView.AddElement(battleNode);

			return true;
		}
	}
}
