using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace Combustion.UI
{
    using Battle;
    using System;
    using Utility;

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        public VisualElement root;

        //Game ui
        public VisualElement ButtonBar { get; private set; }

        public VisualElement TextBox { get; private set; }

        private List<Button> buttons;

        private List<VisualElement> menuItems;

        private int menuIndex;

        private int buttonIndex;

        private Label dialogLabel;
        private string previousBoxText;
        private float previousTypeDelay;

        public Action<int> onButtonSelect;
        public Action<int> onMenuItemSelect;

        public enum MenuState {
            Act,
            Dialog,
            None
		}

        public MenuState menuState = MenuState.None;

        private void Start() {
            buttons = new List<Button>();

            menuItems = new List<VisualElement>();

            buttonIndex = 0;

            menuIndex = 0;

            Instance = this;

            root = GetComponent<UIDocument>().rootVisualElement;

            ButtonBar = root.Q("GameButtons");

            buttons = new List<Button>();

            buttons.Add(root.Q<Button>("FIGHT"));
            buttons.Add(root.Q<Button>("MAGIC"));
            buttons.Add(root.Q<Button>("ACT"));
            buttons.Add(root.Q<Button>("ITEM"));
            buttons.Add(root.Q<Button>("DEFEND"));

            for (int i = 0; i < buttons.Count; i++)
            {
                Button button = buttons[i];

                button.RegisterCallback<NavigationMoveEvent>((evt) =>
                {
                    switch (evt.direction)
                    {
                        case NavigationMoveEvent.Direction.Up:
                        case NavigationMoveEvent.Direction.Down: break;
                        case NavigationMoveEvent.Direction.Left: SelectPreviousButton(); break;
                        case NavigationMoveEvent.Direction.Right: SelectNextButton(); break;
                    }

                    evt.PreventDefault();

                    //TODO: find a way to let the child of the BattleManager class store this value
                    //SoundManager.Instance.PlaySound(BattleManager.Instance.buttonSelectAudio);

                    onButtonSelect(i);
                });
            }

            buttons[2].RegisterCallback<NavigationSubmitEvent>((evt) =>
            {
                CreateActMenu();
            });

            TextBox = root.Q("TextBox");
        }

        public void SelectCurrentMenuItem() {
            SelectMenuItem(menuIndex);
        }

        public void SelectMenuItem(int index) {
            if (BattleManager.Instance.turnState == BattleManager.TurnState.Player)
            {
                menuIndex = index;

                menuItems[index].Focus();
            }
        }

        public void SelectNextMenuItem() {
            menuIndex++;

            if (menuIndex % 3 == 0)
                menuIndex -= 3;

            SelectMenuItem(menuIndex);
        }

        public void SelectPreviousMenuItem() {
            menuIndex--;

            if (menuIndex % 3 < 0 || menuIndex % 3 == 2)
                menuIndex += 3;

            SelectMenuItem(menuIndex);
        }

        public void SelectRightMenuItem() {
            menuIndex += 3;

            if (menuIndex > menuItems.Count - 1)
                menuIndex -= menuItems.Count;

            SelectMenuItem(menuIndex);
        }

        public void SelectLeftMenuItem() {
            menuIndex -= 3;

            if (menuIndex < 0)
                menuIndex += menuItems.Count;

            SelectMenuItem(menuIndex);
        }

        public void SelectCurrentButton() {
            SelectButton(buttonIndex);
        }

        public void SelectButton(int index) {
            if (BattleManager.Instance.turnState == BattleManager.TurnState.Player)
            {
                buttonIndex = index;

                buttons[index].Focus();
            }
        }

        public void SelectPreviousButton() {
            buttonIndex--;

            if (buttonIndex < 0)
                buttonIndex = 4;

            SelectButton(buttonIndex);
        }

        public void SelectNextButton() {
            buttonIndex++;

            if (buttonIndex > 4)
                buttonIndex = 0;

            SelectButton(buttonIndex);
        }

        public void ClearTextBox() {
            switch (menuState)
			{
                case MenuState.Act:
                    TextBox.Clear();
                    menuItems = new List<VisualElement>();
                    SelectCurrentButton();
                    break;
                case MenuState.Dialog:
                    StopCoroutine("_ShowBoxText");
                    TextBox.Clear();
                    dialogLabel = null;
                    break;
			}

            menuState = MenuState.None;
		}


        private void CreateActMenu() {
            ClearTextBox();

            menuState = MenuState.Act;

            SerializableDictionary<string, UnityEvent> actChoices = BattleManager.Instance.currentEnemy.actChoices;

            foreach (var (name, evt) in actChoices)
            {
                Button choice = new Button() { text = "* " + name };

                choice.RegisterCallback<NavigationSubmitEvent>((e) =>
                {
                    evt.Invoke();
                });

                choice.RegisterCallback<NavigationCancelEvent>((e) =>
                {
                    ShowBoxText(previousBoxText, previousTypeDelay);
                });

                choice.pickingMode = PickingMode.Ignore;

                choice.AddToClassList("actChoice");

                TextBox.Add(choice);

                menuItems.Add(choice);

                choice.RegisterCallback<NavigationMoveEvent>((e) =>
                {
                    switch (e.direction)
                    {
                        case NavigationMoveEvent.Direction.Left: SelectLeftMenuItem(); break;
                        case NavigationMoveEvent.Direction.Right: SelectRightMenuItem(); break;
                        case NavigationMoveEvent.Direction.Up: SelectPreviousMenuItem(); break;
                        case NavigationMoveEvent.Direction.Down: SelectNextMenuItem(); break;
                    }

                    e.PreventDefault();

                    onMenuItemSelect(menuItems.IndexOf(choice));
                });
            }

            menuIndex = 0;

            SelectCurrentMenuItem();
        }

        public void ShowBoxText(string text, float typeDelay) {
            ClearTextBox();

            menuState = MenuState.Dialog;

            previousBoxText = text;
            previousTypeDelay = typeDelay;

            StartCoroutine(_ShowBoxText(text, typeDelay));
        }

        private IEnumerator _ShowBoxText(string text, float typeDelay) {
            if (dialogLabel == null)
			{
                dialogLabel = new Label();

                dialogLabel.AddToClassList("dialogLabel");

                TextBox.Add(dialogLabel);
			}

            for (int i = 0; i < text.Length; i++)
            {
                if (dialogLabel == null)
				{
                    break;
				}

                if (!ArenaController.Instance.IsMoving)
				{
                    dialogLabel.text += text[i];
                }

                yield return new WaitForSeconds(typeDelay);
            }
        }
    }
}
