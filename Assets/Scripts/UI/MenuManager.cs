using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

namespace Combustion.UI
{
    using Battle;
    using Combustion.Dialog;
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
        private DialogContainer currentDialog;
        //private string previousBoxText;
        //private float previousTypeDelay;

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

			buttons = new List<Button>
			{
				root.Q<Button>("FIGHT"),
				root.Q<Button>("MAGIC"),
				root.Q<Button>("ACT"),
				root.Q<Button>("ITEM"),
				root.Q<Button>("DEFEND")
			};

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

                    onButtonSelect(i);
                });
            }

            buttons[0].RegisterCallback<NavigationSubmitEvent>((evt) =>
            {
                Fight();
            });

            buttons[2].RegisterCallback<NavigationSubmitEvent>((evt) =>
            {
                CreateActMenu();
            });

            buttons[4].RegisterCallback<NavigationSubmitEvent>((evt) =>
            {
                Defend();
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
			TextBox.Clear();

			switch (menuState)
			{
                case MenuState.Act:
                    menuItems = new List<VisualElement>();
                    SelectCurrentButton();
                    break;
                case MenuState.Dialog:
                    StopCoroutine("_ShowDialog");
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
                    currentDialog.Reset(true);
                    ShowDialog(currentDialog);
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

        public void ShowDialog(DialogContainer dialog) {
            ClearTextBox();

            menuState = MenuState.Dialog;

            currentDialog = dialog;

            StartCoroutine(_ShowDialog());
		}

        private IEnumerator _ShowDialog() {
            if (dialogLabel == null)
            {
                dialogLabel = new Label();

                dialogLabel.AddToClassList("dialogLabel");

                TextBox.Add(dialogLabel);
            }
            
            while (ArenaController.Instance.IsMoving)
            {
                //This isn't working.
                //Text is still added to the text box while it's moving
                yield return null;
            }

			Coroutine dialog = StartCoroutine(currentDialog.Parse());


			while (!currentDialog.IsDone)
            {
                if (dialogLabel == null)
                {
                    break;
                }

				dialogLabel.text = currentDialog.currentText;

                yield return null;
			}

            yield return dialog;
        }

		private void Defend() {
            //TODO: implement damage reduction logic when HP system is in place

            BattleManager.Instance.AdvanceTurnState();
        }

        private void Fight() {
            //TODO: implement attack logic when HP system is in place

            BattleManager.Instance.AdvanceTurnState();
        }
    }
}
