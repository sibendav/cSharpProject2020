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


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for OwnerWin.xaml
    /// </summary>
    public partial class OwnerWin : Window
    {
        public Order thisOR;
        public HostingUnit thisHO;
        public GuestRequest thisGR;
        double huFee;

        public OwnerWin(string langu)
        {
            InitializeComponent();
            if (langu == "hebrow")
                ChangeL();
            dtgrdAllGRs.ItemsSource = BL.BLFactory.getBL().AllGuestRequests();
            cmbGrpArea.ItemsSource = Enum.GetValues(typeof(BE.Area));
            cmbGrpVac.ItemsSource = AllNumVacationers();

            dtgrdAllHU.ItemsSource = BL.BLFactory.getBL().AllHostingUnits();
            cmbAreaHU.ItemsSource = Enum.GetValues(typeof(BE.Area));
            cmbByHost.ItemsSource = NumAllHosting();

            cmbResortType.ItemsSource = Enum.GetValues(typeof(BE.ResortType));
            cmbArea.ItemsSource = Enum.GetValues(typeof(BE.Area));
            cbmChildernAtrac.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmGarden.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmJacuzzi.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmPool.ItemsSource = Enum.GetValues(typeof(BE.Extra));
        }

        public List<int> NumAllHosting()
        {
            return (from item in BL.BLFactory.getBL().AllHostingUnits()
                    let host1 = item.Owner.MailAddress
                    select (from item1 in BL.BLFactory.getBL().AllHostingUnits()
                            where item1.Owner.MailAddress == host1
                            select item1).Count()).Distinct().ToList();

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dtgrdAllGRs.ItemsSource = BL.BLFactory.getBL().AllGuestRequests();

        }
        public List<int> AllNumVacationers()
        {
            return (from item in BL.BLFactory.getBL().AllGuestRequests()
                    select item.Adults + item.Children).Distinct().ToList();
        }

        private void cmbGrpArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area thisA = (Area)cmbGrpArea.SelectedItem;
            var list = from item in BL.BLFactory.getBL().GroupGuestReqByArea()
                                      where item.Key == thisA
                                      select item;
            dtgrdAllGRs.ItemsSource = list.SelectMany(group => group).ToList();
        }

        private void cmbGrpVac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int thisNum = (int)cmbGrpVac.SelectedItem;
            var list = from item in BL.BLFactory.getBL().GroupGuestReqBynumVacationers()
                                      where item.Key == thisNum
                                      select item;
            dtgrdAllGRs.ItemsSource = list.SelectMany(group => group).ToList();
            
        }

        private void dtgrdAllGRs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((GuestRequest)dtgrdAllGRs.SelectedValue != null)
            {
                thisGR = (GuestRequest)dtgrdAllGRs.SelectedValue;
                this.DataContext = thisGR;
            }
        }

        private void btnClearHU_Click(object sender, RoutedEventArgs e)
        {
            dtgrdAllHU.ItemsSource = BL.BLFactory.getBL().AllHostingUnits();
        }

        private void cmbAreaHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area thisA = (Area)cmbAreaHU.SelectedItem;
            var list = from item in BL.BLFactory.getBL().GroupHostingUnitsByArea()
                                     where item.Key == thisA
                                     select item;
            dtgrdAllHU.ItemsSource = list.SelectMany(group => group).ToList();
            
        }

        private void cmbByHost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int thisNumHosting = (int)cmbByHost.SelectedItem;
            var list = from item in BL.BLFactory.getBL().GroupHostByNumHostingUnits()
                                     where item.Key == thisNumHosting
                                     select item;
            dtgrdAllHU.ItemsSource = list.SelectMany(group => group).ToList();
            
        }

        private void dtgrdAllHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((HostingUnit)dtgrdAllHU.SelectedItem != null)
            {
                thisHO = (HostingUnit)dtgrdAllHU.SelectedItem;
                this.DataContext = thisHO;
                btnShowDtl.Visibility = Visibility.Visible;
                btnOrderDet.Visibility = Visibility.Visible;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ChangeL()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f = new MainWindow();
            f.Show();
            this.Close();
        }

        private void btnShowDtl_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Host Details:\n" + thisHO.Owner.PrivateName + thisHO.Owner.FamilyName + "\n" + thisHO.Owner.MailAddress + "\n0" + thisHO.Owner.PhoneNumber,"Hoster Details!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnOrderDet_Click(object sender, RoutedEventArgs e)
        {
            string t = AllOrders();
            if (t == "")
            {
                MessageBox.Show("There is no Orders!", "Sorry!", MessageBoxButton.OK, MessageBoxImage.Information);
                lblFee.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show(t, "All Orders!", MessageBoxButton.OK, MessageBoxImage.Information);
                lblFee.Content += Convert.ToString(huFee);
            }
        }
        private string AllOrders()
        {
            string t = "";
            var list = BL.BLFactory.getBL().FindAllThePriviaseOrdersForHU(thisHO);
            if(list!=null)
            {
                foreach(Order item in list)
                {
                    if (item.Status == OrderStatus.ClosedForCustomerResponse)
                    {
                        t += ("Date:" + item.OrderDate + " Sum:" + item.OrderFee + "$\n");
                        huFee += item.OrderFee;
                    }
                }
            }
            return t;
        }
    }
}
