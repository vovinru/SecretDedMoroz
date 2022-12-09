using SecretDedMorozLibrary;
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

namespace SectetDedMoroz
{
    /// <summary>
    /// Interaction logic for WindowPlayer.xaml
    /// </summary>
    public partial class WindowPlayer : Window
    {
        WindowPlayerViewModel _viewModel;
        Player _player;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player">если null значит новый игрок</param>
        public WindowPlayer(Player player = null)
        {
            InitializeComponent();

            _player = player;

            if (_player == null)
                _player = new Player(string.Empty, string.Empty);

            _viewModel = new WindowPlayerViewModel(_player.Name, _player.MailAddress);

            DataContext = _viewModel;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if(_viewModel.IsCorrectData())
            {
                _player.Name = _viewModel.Name;
                _player.MailAddress = _viewModel.MailAddress;

                DialogResult = true;
                this.Close();
            }

            else
            {
                MessageBox.Show("Данные введены не корректно!");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    class WindowPlayerViewModel
    {
        public string Name
        {
            get;
            set;
        }

        public string MailAddress
        {
            get;
            set;
        }

        public WindowPlayerViewModel(string name, string mailAddress)
        {
            Name = name;
            MailAddress = mailAddress;
        }

        public bool IsCorrectData()
        {
            return Name.Length > 0 && MailAddress.Length > 0;
        }
    }
}
