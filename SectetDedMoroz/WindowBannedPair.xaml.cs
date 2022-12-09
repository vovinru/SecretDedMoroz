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
    /// Interaction logic for WindowBannedPair.xaml
    /// </summary>
    public partial class WindowBannedPair : Window
    {

        WindowBannedPairViewModel _viewModel;
        BannedPair _pair;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pair">если null значит новая пара</param>
        public WindowBannedPair(List<Player> players, BannedPair pair = null)
        {
            InitializeComponent();

            _pair = pair;

            if (_pair == null)
                _pair = new BannedPair(null, null);

            _viewModel = new WindowBannedPairViewModel(players, _pair.Player1, _pair.Player2);

            DataContext = _viewModel;
        }

        public BannedPair GetBannedPair()
        {
            return _pair;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.IsCorrectData())
            {
                _pair.Player1 = _viewModel.SelectPlayer1;
                _pair.Player2 = _viewModel.SelectPlayer2;

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

    class WindowBannedPairViewModel
    {
        public List<Player> Players
        {
            get;
            set;
        }

        public Player SelectPlayer1
        {
            get;
            set;
        }


        public Player SelectPlayer2
        {
            get;
            set;
        }

        public WindowBannedPairViewModel(List<Player> players, Player selectPlayer1, Player selectPlayer2)
        {
            Players = players;
            SelectPlayer1 = selectPlayer1;
            SelectPlayer2 = selectPlayer2;
        }

        public bool IsCorrectData()
        {
            return SelectPlayer1 != null && SelectPlayer2 != null && SelectPlayer1 != SelectPlayer2;
        }
    }
}
