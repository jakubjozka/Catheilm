using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Engine.Models;
using Engine.Services;
using Engine.ViewModels;

namespace WPFUI
{
    public partial class CharacterCreation : Window
    {
        private CharacterCreationViewModel VM { get; set; }
        public CharacterCreation()
        {
            InitializeComponent();

            VM = new CharacterCreationViewModel();
            DataContext = VM;
        }
        private void RandomPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            VM.RollNewCharacter();
        }

        private void UseThisPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(VM.GetPlayer(), true);
            mainWindow.Show();
            Close();
        }

        private void Race_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.ApplyAttributeModifiers();
        }
    }
}
