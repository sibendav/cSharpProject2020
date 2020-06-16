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
using System.Net.Mail;
using System.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string frommail = "simkha555@gmail.com";
        public static string tomail = "";
        public static string pass = "";
        private string UserName = "67766749";
            //"S&E2020";
        public string MyUserName
        {
            get { return UserName; }
            set { UserName = value; }
        }
        private string Password = "1234";
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
                                GuestRequestWin f = new GuestRequestWin(gr, "show", MyLan);
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
                                HostWin f = new HostWin(h, "show", MyLan);
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
                                OwnerWin f = new OwnerWin(MyLan);
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
                        GuestRequestWin f = new GuestRequestWin(null, "add", MyLan);
                        f.Show();
                        this.Close();
                        break;
                    }
                case "Hoster":
                case "מארח":
                    {
                        HostWin f = new HostWin(null, "add", MyLan);
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
                        try
                        {
                            if(txtUserName.Text=="")
                                MessageBox.Show("Please Inter your User Name!!!", "System Request!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                            {
                                GuestRequest gr = BL.BLFactory.getBL().getGRByUserName(txtUserName.Text);
                                if (gr != null)
                                {
                                    tomail= gr.MailAddress;
                                    pass =gr.Password;
                                    Thread t=new Thread(sendPassword);
                                    t.Start();
                                    MessageBox.Show("We are trying to send you an email. We will let you know when it's done!", "Attention!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                    throw new MyException();
                            }
                        }
                        catch (MyException)
                        {
                            MessageBox.Show("The user-name does not!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        break;
                    }
                case "Hoster":
                case "מארח":
                    {
                        try
                        {
                            if (txtUserName.Text == "")
                                MessageBox.Show("Please Inter your User Name!!!", "System Request!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                            else
                            {
                                Host h = BL.BLFactory.getBL().getHostByUserName(txtUserName.Text);
                                if (h != null)
                                {
                                    tomail = h.MailAddress;
                                    pass = h.Password;
                                    Thread t = new Thread(sendPassword);
                                    t.Start();
                                    MessageBox.Show("We are trying to send you an email. We will let you know when it's done!", "Attention!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                else
                                    throw new MyException();
                            }
                        }
                        catch (MyException)
                        {
                            MessageBox.Show("The user-name does not!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        break;
                    }
                case "WebsiteOwner":
                case "מנהל אתר":
                    {
                        try
                        {
                            tomail = frommail;
                            pass = Password;
                            Thread t = new Thread(sendPassword);
                            t.Start();
                            MessageBox.Show("We are trying to send you an email. We will let you know when it's done!", "Attention!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch 
                        {
                            MessageBox.Show("The user-name does not!", "WARNNING!!!", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        private void btnChangeToIL_Click(object sender, RoutedEventArgs e)
        {
            myLan = "hebrow";
            this.FlowDirection = FlowDirection.RightToLeft;
            cmbUserSellection.ItemsSource = new List<string> { "מתארח/נופש", "מארח", "מנהל אתר" };
            lblCMB.Content = "בחר הרשאת גישה:";
            lblForgottenPassword.Content = "שכחת סיסמה";
            lblNewUser.Content = "משתמש חדש";
            lblTitle.Content = "ברוכים הבאים";
            btnEnter.Content = "הכנס";
            lblus.Content = "שם משתמש";
            lblpass.Content = "סיסמה";
        }

        private void btnChangeToUS_Click(object sender, RoutedEventArgs e)
        {
            myLan = "english";
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
        private static void sendPassword()
        {
            int i = 3;
            MailMessage mail = new MailMessage();
            mail.To.Add(tomail);
            mail.From = new MailAddress(frommail);
            mail.Subject = "YOUR PASSWORD!!!";
            mail.Body = "Hi, your password is:" +pass+ " Have a nice day!";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("simkha555", "SBD24300");
            smtp.EnableSsl = true;
            while (i != 0)
            {
                try
                {
                    smtp.Send(mail);
                    MessageBox.Show("Check your inbox mail!!!", "We send your  a mail!!!", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                catch
                {
                    i--;
                    Thread.Sleep(300);
                }
            }
            if (i <= 0)
                MessageBox.Show("Can not Send a mail!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
