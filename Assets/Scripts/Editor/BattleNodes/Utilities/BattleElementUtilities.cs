using Combustion.Editor.BattleNodes.Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Combustion.Editor.BattleNodes.Utilities
{
	public static class BattleElementUtilities
	{
		public static TextField CreateTextField(string value = null, EventCallback<ChangeEvent<string>> onValueChanged = null) {
			TextField textField = new TextField()
			{
				value = value
			};

			if (onValueChanged != null)
			{
				textField.RegisterValueChangedCallback(onValueChanged);
			}

			return textField;
		}

		public static Button CreateButton(string text, Action onClick = null) {
			Button button = new Button(onClick)
			{
				text = text
			};

			return button;
		}

		public static Port CreatePort(this BattleNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single) {
			Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

			port.portName = portName;

			return port;
		}
	}
}
