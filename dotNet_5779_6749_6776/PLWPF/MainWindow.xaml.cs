using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using BL;
using BE;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    public enum WinLanguegh { Hebrew, English };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string UserName = "S&E2020";
        public string MyUserName
        {
            get { return UserName; }
            set { UserName = value; }
        }
        private string Password = "67766749";
        public string MyPassword
        {
            get { return Password; }
            set { Password = value; }
        }
        private string winState;
        public string MyWinState
        {
            get { return winState; }
            set { winState = value; }
        }
        private string myLan = "english";
        public string MyLan
        {
            get { return myLan; }
            set { myLan = value; }
        }


        public MainWindow()
        {
            //IBL myBL;
            InitializeComponent();
            cmbUserSellection.ItemsSource = new List<string> { "GuestRequest", "Hoster", "WebsiteOwner" };
        }

        private void cmbUserSellection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblCMB.Visibility = Visibility.Collapsed;
            this.MyWinState = Convert.ToString(cmbUserSellection.SelectedValue);
            txtUserName.IsEnabled = true;
            txtUserName.Text = "";
            passwordBox.IsEnabled = true;
            passwordBox.Password = "";
            btnEnter.IsEnabled = true;
            lblForgottenPassword.IsEnabled = true;
            lblNewUser.IsEnabled = true;
            btnEnter.IsEnabled = true;
        }


        private void passwordBox_MouseLeave(object sender, MouseEventArgs e)
        {

        }


        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MyWinState)
                {
                    case "GuestRequest":
                    case "מתארח/נופש":
                        {
                            GuestRequest gr = BL.BLFactory.getBL().getGRByUserName(txtUserName.Text);
                            if (gr == null)
                                MessageBox.Show("The Guest Does Not Exist!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
                            if (passwordBox.Password == gr.Password)
                            {
                                GuestRequestWin f = new GuestRequestWin(gr, "show");
                                f.Show();
                                this.Close();
                            }
                            else
                                MessageBox.Show("The password or the user-name is not corrent!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);

                            break;
                        }
                    case "Hoster":
                    case "מארח":
                        {
                            Host h = BL.BLFactory.getBL().getHostByUserName(txtUserName.Text);
                            if (h != null && passwordBox.Password == h.Password)
                            {
                                //HostingUnit hu = new HostingUnit();
                                //hu.Owner = h;
                                HostWin f = new HostWin(h, "show");
                                f.Show();
                                this.Close();
                            }
                            else
                                MessageBox.Show("The password or the user-name is not corrent!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);

                            break;
                        }
                    case "WebsiteOwner":
                    case "מנהל אתר":
                        {
                            if (txtUserName.Text == MyUserName && passwordBox.Password == MyPassword)
                            {
                                OwnerWin f = new OwnerWin();
                                f.Show();
                                this.Close();
                            }
                            else
                                MessageBox.Show("The password or the user-name is not corrent!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }



        private void lblNewUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (MyWinState)
            {
                case "GuestRequest":
                case "מתארח/נופש":
                    {
                        GuestRequestWin f = new GuestRequestWin(null, "add");
                        f.Show();
                        this.Close();
                        break;
                    }
                case "Hoster":
                case "מארח":
                    {
                        HostWin f = new HostWin(null, "add");
                        f.Show();
                        this.Close();
                        break;
                    }
                case "WebsiteOwner":
                case "מנהל אתר":
                    {
                        MessageBox.Show("You can't add new website owner!!!", "Pay Attention!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                default:
                    break;
            }
        }

        private void lblForgottenPassword_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (MyWinState)
            {
                case "GuestRequest":
                case "מתארח/נופש":
                    {
                        MessageBox.Show("Check your inbox mail!!!", "We send your  a mail!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                        //send a mail
                        break;
                    }
                case "Hoster":
                case "מארח":
                    {
                        MessageBox.Show("Check your inbox mail!!!", "We send your  a mail!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                        //send a mail
                        break;
                    }
                case "WebsiteOwner":
                case "מנהל אתר":
                    {
                        MessageBox.Show("Check The inside code!!!", "We've got you!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                default:
                    break;
            }
        }

        private void btnChangeToIL_Click(object sender, RoutedEventArgs e)
        {
            this.FlowDirection = FlowDirection.RightToLeft;
            cmbUserSellection.ItemsSource = new List<string> { "מתארח/נופש", "מארח", "מנהל אתר" };
            lblCMB.Content = "בחר הרשאת גישה:";
            lblForgottenPassword.Content = "שכחת סיסמה";
            lblNewUser.Content = "משתמש חדש";
            lblTitle.Content = "ברוכים הבאים ל'פיינ-קון'";
            btnEnter.Content = "הכנס";
            lblus.Content = "שם משתמש";
            lblpass.Content = "סיסמה";
        }

        private void btnChangeToUS_Click(object sender, RoutedEventArgs e)
        {
            this.FlowDirection = FlowDirection.LeftToRight;
            cmbUserSellection.ItemsSource = new List<string> { "GuestRequest", "Hoster", "WebsiteOwner" };
            lblCMB.Content = "Please select your visiter type:";
            lblForgottenPassword.Content = "Forgotten password";
            lblNewUser.Content = "New User";
            lblTitle.Content = "Welcome To PineCone";
            btnEnter.Content = "Enter";
            lblus.Content = "UserName";
            lblpass.Content = "Password";
        }

        private void btnChangeToIL_MouseEnter(object sender, MouseEventArgs e)
        {
        }
    }
}
