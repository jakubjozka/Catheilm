using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.Models;
using Engine.Services;
using Engine.ViewModels;
using Microsoft.Win32;
using WPFUI.Windows;

namespace WPFUI
{
    public partial class MainWindow : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "SchoolRPG";

        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private GameSession _gameSession;

        public MainWindow(Player player, bool IsNew, int xLocation = 0, int yLocation = -1)
        {
            InitializeComponent();

            SetActiveGameSessionTo(new GameSession(player, xLocation, yLocation), IsNew);
        }
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }
        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }
        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }
        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            if (_gameSession.CurrentTrader != null)
            {
                TradeScreen tradeScreen = new TradeScreen();
                tradeScreen.Owner = this;
                tradeScreen.DataContext = _gameSession;
                tradeScreen.ShowDialog();
            }
        }
        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

        private void SetActiveGameSessionTo(GameSession gameSession, bool IsNew)
        {
            _messageBroker.OnMessageRaised -= OnGameMessageRaised;

            _gameSession = gameSession;
            DataContext = _gameSession;

            GameMessages.Document.Blocks.Clear();

            _messageBroker.OnMessageRaised += OnGameMessageRaised;

            if (_gameSession.CurrentLocation == _gameSession.CurrentWorld.LocationAt(0, -1) && IsNew)
            {
                _messageBroker.RaiseMessage("Welcome to the town called Catheilm, your mission is to defeat the evil and help the citizens with their needs.");
                _messageBroker.RaiseMessage("You can move around with buttons in the right bottom corner.");
                _messageBroker.RaiseMessage("You can select a weapon/consumable in the bottom.");
                _messageBroker.RaiseMessage("You can trade in certain locations and craft in the recipe section.");
                _messageBroker.RaiseMessage("You can start a new game, save your current game or exit by clicking on the file button");
                _messageBroker.RaiseMessage("Have fun playing the game!");
            }
        }

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.Show();
            Close();
        }

        private void SaveGame_OnClick(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            YesNoWindow message =
                 new YesNoWindow("Save Game?", "Do you want to save your game?");
            message.Owner = GetWindow(this);
            message.ShowDialog();

            if (message.ClickedYes)
            {
                SaveGame();
            }
        }

        private void SaveGame()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                Filter = $"Saved games (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                SaveGameService.Save(_gameSession, saveFileDialog.FileName);
            }
        }
    }
}