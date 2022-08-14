using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Combustion.Projectile;

namespace Combustion.Battle.Nodes
{
	[CreateAssetMenu(menuName = "Combustion/New Battle Tree")]
	public class BattleTree : ScriptableObject
	{

		public List<BattleNode> nodes = new List<BattleNode>();

		public Pattern ChoosePattern() {
			return rootNode.Evaluate();
		}

		public BattleNode rootNode;

		public BattleNode CreateNode(System.Type type) {
			BattleNode node = ScriptableObject.CreateInstance(type) as BattleNode;
			node.name = type.Name;
			node.guid = GUID.Generate().ToString();

			nodes.Add(node);

			AssetDatabase.AddObjectToAsset(node, this);
			AssetDatabase.SaveAssets();

			return node;
		}

		public void DeleteNode(BattleNode node) {
			nodes.Remove(node);

			AssetDatabase.RemoveObjectFromAsset(node);
			AssetDatabase.SaveAssets();
		}

		public void AddChild(BattleNode parent, BattleNode child) {
			switch (parent)
			{
				case CompositeNode compositeNode:
					compositeNode.children.Add(child);
					break;
				case ModifyNode modifyNode:
					modifyNode.child = child;
					break;
				case RootNode rootNode:
					rootNode.child = child;
					break;
			}
		}

		public void RemoveChild(BattleNode parent, BattleNode child) {
			switch (parent)
			{
				case CompositeNode node:
					node.children.Remove(child);
					break;
				case ModifyNode node:
					node.child = null;
					break;
				case RootNode node:
					node.child = null;
					break;
			}
		}

		public List<BattleNode> GetChildren(BattleNode parent) {
			switch (parent)
			{
				case CompositeNode node:
					if (node.children.Count > 0)
						return node.children;
					else
						return new List<BattleNode>();
				case ModifyNode node:
					if (node.child != null)
						return new List<BattleNode>() { node.child };
					else
						return new List<BattleNode>();
				case RootNode node:
					if (node.child != null)
						return new List<BattleNode>() { node.child };
					else
						return new List<BattleNode>();
				case ActionNode node:
					return new List<BattleNode>();
			}

			return new List<BattleNode>();
		}
	}
}