using SecretDedMorozLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SectetDedMoroz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ADDRESS_SENDER = "vovinru.secretdedmoroz@mail.ru";
        string PASSWORD_SENDER = "ncsx6ygvvTSexYJgFNZe";
        string SMTP_NAME = "smtp.mail.ru";
        int SMTP_PORT = 587;

        Group _group;
        MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _group = new Group();
            _viewModel = new MainWindowViewModel(_group);

            UpdateViewModel();
        }

        void UpdateViewModel()
        {
            DataContext = null;
            DataContext = _viewModel;
        }

        private void ButtonAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            WindowPlayer window = new WindowPlayer();

            if(window.ShowDialog() == true)
            {
                _group.Players.Add(window.GetPlayer());
            }

            UpdateViewModel();
        }

        private void ButtonCreatePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectPlayer != null)
            {
                WindowPlayer window = new WindowPlayer(_viewModel.SelectPlayer);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Участник не выбран");
            }

            UpdateViewModel();
        }

        private void ButtonDeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if(_viewModel.SelectPlayer!=null)
            {
                if(MessageBox.Show("Удалить участника?","",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _group.Players.Remove(_viewModel.SelectPlayer);
                }
            }

            UpdateViewModel();
        }

        private void ButtonAddBannedPair_Click(object sender, RoutedEventArgs e)
        {
            WindowBannedPair window = new WindowBannedPair(_group.Players);

            if (window.ShowDialog() == true)
            {
                _group.BannedPairs.Add(window.GetBannedPair());
            }

            UpdateViewModel();
        }

        private void ButtonCreateBannedPair_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectBannedPair != null)
            {
                WindowBannedPair window = new WindowBannedPair(_group.Players,_viewModel.SelectBannedPair);
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пара не выбрана");
            }

            UpdateViewModel();
        }

        private void ButtonDeleteBannedPair_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectBannedPair != null)
            {
                if (MessageBox.Show("Удалить пару?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _group.BannedPairs.Remove(_viewModel.SelectBannedPair);
                }
            }

            UpdateViewModel();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            List<Tuple<Player,Player>> pairs = _group.GeneratePairs();

            string result = string.Empty;

            foreach(Tuple<Player,Player> p in pairs)
            {
                result += string.Format("\n{0} - {1}", p.Item1, p.Item2);

                string subject = "ТАЙНЫЙ ДЕД МОРОЗ! УРА!)";

                string body = string.Format("Привет {0}! Твой тайный дед мороз: {1}. \n" +
                    "Подготовь подарок, но не дороже 2000 рублей! \n" +
                    "Обменяемся подарками 24 декабря!", p.Item1.Name, p.Item2.Name);

                PostSend.SendMesage(ADDRESS_SENDER, PASSWORD_SENDER, SMTP_NAME, SMTP_PORT, p.Item1.MailAddress, subject, body);
                PostSend.SendMesage(ADDRESS_SENDER, PASSWORD_SENDER, SMTP_NAME, SMTP_PORT, ADDRESS_SENDER, subject, body);
            }

        }
    }

    class MainWindowViewModel
    {
        public Group Group
        {
            get;
            set;
        }

        public Player SelectPlayer
        {
            get;
            set;
        }

        public BannedPair SelectBannedPair
        {
            get;
            set;
        }

        public MainWindowViewModel(Group group)
        {
            Group = group;
        }
    }
}
