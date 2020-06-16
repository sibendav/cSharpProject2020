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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostWin.xaml
    /// </summary>
    public partial class HostWin : Window
    {
        public BankBranch thisBank = new BankBranch();
        public Host thisHost = new Host();
        public HostingUnit thisHosting = new HostingUnit(), temp;
        public Order thisOrder;
        public GuestRequest thisGR = new GuestRequest();
        public List<BankBranch> listb;

        public string MyState { get; set; }

        public HostWin(Host h, string st)
        {
            InitializeComponent();
            if (h != null)
            {
                thisBank = h.BankBranchDetails;
                thisHost = h;
                thisHosting = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost).First();
            }
            else
            {
                thisHosting.Diary = new bool[12, 31];
                thisHost.BankBranchDetails = thisBank;
            }
            thisHosting.Owner = thisHost;
            this.DataContext = thisHosting;

            listb = BL.BLFactory.getBL().AllBankBranch();

            MyState = st;
            cbBank.ItemsSource = (from item in listb
                                  select item.BankName).Distinct().ToList();
            //cbBank.DisplayMemberPath = "BankName";
            cmbType.ItemsSource = Enum.GetValues(typeof(BE.ResortType));
            cmbArea.ItemsSource = Enum.GetValues(typeof(BE.Area));

            switch (MyState)
            {
                case "add":
                    {
                        tbHU.IsEnabled = false;
                        tbPotOrd.IsEnabled = false;
                        this.DataContext = thisHost;
                        DtgrdAllHU.Visibility = Visibility.Collapsed;
                        btnAddNewHost.Visibility = Visibility.Collapsed;
                        btnUpdate.Visibility = Visibility.Collapsed;
                        btnDelete.Visibility = Visibility.Collapsed;
                        break;
                    }
                case "show":
                    {
                        //thisHost=
                        cmbHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                        cmbHU.DisplayMemberPath = "HostingUnitName";
                        this.DataContext = h;
                        btnAddNew.Visibility = Visibility.Collapsed;
                        grpBHostD.IsEnabled = false;
                        DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                        btnAdd.Visibility = Visibility.Collapsed;
                        break;
                    }
                default:
                    break;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.BLFactory.getBL().DeleteHostingUnit(thisHosting);
                MessageBox.Show("Your Hosting Unit Deleted Successfully!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                cmbHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                cmbHU.DisplayMemberPath = "HostingUnitName";
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbBank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNumberBank_Copy.Text = Convert.ToString((from item in listb
                                                        where item.BankName==Convert.ToString(cbBank.SelectedItem)
                                                        select item.BankNumber).FirstOrDefault());
            cbCity.ItemsSource = (from item in listb
                                  where item.BankName== Convert.ToString(cbBank.SelectedItem)
                                  select item.BranchCity
                                ).Distinct().ToList();
            //cbCity.DisplayMemberPath = "BranchCity";
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            temp = thisHosting;
            thisHosting = new HostingUnit();
            thisHosting.Owner = thisHost;
            this.DataContext = thisHosting;
            this.tbHU.Visibility = Visibility.Visible;
            tbHU.IsSelected = true;
            tbHU.IsEnabled = true;
            tbHost.IsEnabled = false;
            tbPotOrd.IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisHosting.Diary = new bool[12, 31];
                BL.BLFactory.getBL().AddHostingUnit(thisHosting);
                MessageBox.Show("Your Hosting Unit Added Successfully!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                tbHU.IsEnabled = true;
                tbHost.IsEnabled = true;
                tbPotOrd.IsEnabled = true;
                DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                cmbHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
                cmbHU.DisplayMemberPath = "HostingUnitName";
                DtgrdAllHU.Visibility = Visibility.Visible;
                btnAddNewHost.Visibility = Visibility.Visible;
                btnUpdate.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnAdd.Visibility = Visibility.Collapsed;
            }
            catch (MyException ex)
            {

                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tbHU_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnAddNewHost_Click(object sender, RoutedEventArgs e)
        {
            temp = thisHosting;
            thisHosting = new HostingUnit();
            thisHosting.Owner = thisHost;
            this.DataContext = thisHosting;
            DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
        }

        private void DtgrdAllHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DtgrdAllHU.SelectedItem) != null)
            {
                temp = thisHosting;
                thisHosting = (HostingUnit)(DtgrdAllHU.SelectedItem);
                this.DataContext = thisHosting;
                DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);
            }
        }

        private void tbPotOrd_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnFindMatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtgrdMatchingGR.ItemsSource = BL.BLFactory.getBL().FindTheRelevanteGuestRequermentsHostingUnit(thisHosting);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f = new MainWindow();
            f.Show();
            this.Close();
        }

        private void cmbHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnFindMatch.IsEnabled = true;
            thisHosting = (HostingUnit)cmbHU.SelectedItem;
            dtgrdOrders.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseOrdersForHU(thisHosting);
        }

        private void dtgrdMatchingGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreateOr.IsEnabled = true;
            if ((GuestRequest)dtgrdMatchingGR.ItemsSource != null)
                thisGR = (GuestRequest)dtgrdMatchingGR.SelectedItem;
        }

        private void btnCreateOr_Click(object sender, RoutedEventArgs e)
        {
            thisOrder = new Order();
            thisOrder.HostingUnitKey = thisHosting.HostingUnitKey;
            thisOrder.GuestRequestKey = thisGR.GuestRequestKey;
            thisOrder.CreateDate = DateTime.Today;
            thisOrder.Status = OrderStatus.NotAddressed;
            try
            {
                BL.BLFactory.getBL().AddOrder(thisOrder);
                MessageBox.Show("Order Created Successfully!", "SUCCESS!!!", MessageBoxButton.OK, MessageBoxImage.Information);

                dtgrdOrders.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseOrdersForHU(thisHosting);
                //this.DataContext = thisOrder;
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void dtgrdOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if ((Order)dtgrdOrders.SelectedItem != null)
            {
                thisOrder = (Order)dtgrdOrders.SelectedItem;
                if (thisOrder.Status == OrderStatus.NotAddressed)
                    btnSendMail.IsEnabled = true;
                if (thisOrder.Status == OrderStatus.SentEmail)
                    btnUpdate.IsEnabled = true;
            }
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisOrder.Status = OrderStatus.SentEmail;
                thisOrder = BL.BLFactory.getBL().UpdateOrder(thisOrder);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnTryToCloseADeal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisOrder.Status = OrderStatus.ClosedForCustomerResponse;
                thisOrder = BL.BLFactory.getBL().UpdateOrder(thisOrder);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!!!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }


        private void txtBankNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            thisHost.BankBranchDetails = thisBank;
            this.DataContext = thisHost;
        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thisHosting != null && cmbType.SelectedItem != null)
                thisHosting.Type = (ResortType)cmbType.SelectedItem;
        }

        private void cmbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbArea.SelectedItem != null)
                thisHosting.Area = (Area)cmbArea.SelectedItem;
        }

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbAddress.ItemsSource = (from item in listb
                                     where item.BankName==Convert.ToString(cbBank.SelectedValue)
                                            && item.BranchCity == Convert.ToString(cbCity.SelectedValue)
                                     select item.BranchAddress).ToList();
            //cbAddress.DisplayMemberPath = "BranchAddress";
        }

        private void cbAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNumberBank.Text= Convert.ToString((from item in listb
                                 where item.BankName == Convert.ToString(cbBank.SelectedValue)
                                            && item.BranchCity == Convert.ToString(cbCity.SelectedValue)
                                            && item.BranchAddress == Convert.ToString(cbAddress.SelectedValue)
                                 select item.BranchNumber).FirstOrDefault());
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BL.BLFactory.getBL().UpdateHostingUnit(thisHosting);
                MessageBox.Show("Your Hosting Unit Updated Successfully!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                DtgrdAllHU.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseHU(thisHost);

            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
