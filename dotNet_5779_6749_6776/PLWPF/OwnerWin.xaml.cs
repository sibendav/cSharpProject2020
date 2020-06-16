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
        public Host thisHost;
        public HostingUnit thisHO;
        public GuestRequest thisGR;

        public OwnerWin()
        {
            InitializeComponent();
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
            dtgrdAllGRs.ItemsSource = from item in BL.BLFactory.getBL().GroupGuestReqByArea()
                                      where item.Key == thisA
                                      select item;
        }

        private void cmbGrpVac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int thisNum = (int)cmbGrpVac.SelectedItem;
            dtgrdAllGRs.ItemsSource = from item in BL.BLFactory.getBL().GroupGuestReqBynumVacationers()
                                      where item.Key == thisNum
                                      select item;
        }

        private void dtgrdAllGRs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisGR = (GuestRequest)dtgrdAllGRs.SelectedItem;
            this.DataContext = thisGR;
        }

        private void btnClearHU_Click(object sender, RoutedEventArgs e)
        {
            dtgrdAllHU.ItemsSource = BL.BLFactory.getBL().AllHostingUnits();
        }

        private void cmbAreaHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Area thisA = (Area)cmbAreaHU.SelectedItem;
            dtgrdAllHU.ItemsSource = from item in BL.BLFactory.getBL().GroupHostingUnitsByArea()
                                     where item.Key == thisA
                                     select item;
        }

        private void cmbByHost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int thisNumHosting = (int)cmbByHost.SelectedItem;
            dtgrdAllHU.ItemsSource = from item in BL.BLFactory.getBL().GroupHostByNumHostingUnits()
                                     where item.Key == thisNumHosting
                                     select item;
        }

        private void dtgrdAllHU_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisHO = (HostingUnit)dtgrdAllHU.SelectedItem;
            //thisHost = thisHO.Owner;
            this.DataContext = thisHO;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
