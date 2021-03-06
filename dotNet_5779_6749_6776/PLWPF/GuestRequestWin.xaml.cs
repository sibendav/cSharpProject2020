﻿using System;
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
    /// Interaction logic for GuestRequestWin.xaml
    /// </summary>
    public partial class GuestRequestWin : Window
    {
        public GuestRequest thisGR, temp;
        public string thisState;

        public GuestRequestWin(GuestRequest gr, string state)
        {
            thisState = state;
            if (gr != null)
                thisGR = gr;
            else
                thisGR = new GuestRequest();
            InitializeComponent();
            this.DataContext = thisGR;

            cmbResortType.ItemsSource = Enum.GetValues(typeof(BE.ResortType));
            cmbResortType_Copy.ItemsSource = Enum.GetValues(typeof(BE.ResortType));
            cmbArea.ItemsSource = Enum.GetValues(typeof(BE.Area));
            cmbArea_Copy.ItemsSource = Enum.GetValues(typeof(BE.Area));
            cbmChildernAtrac.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmChildernAtrac_Copy.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmGarden.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmGarden_Copy.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmJacuzzi.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmJacuzzi_Copy.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmPool.ItemsSource = Enum.GetValues(typeof(BE.Extra));
            cbmPool_Copy.ItemsSource = Enum.GetValues(typeof(BE.Extra));

            switch (thisState)
            {
                case "add":
                    {
                        btnUpdate.Visibility = Visibility.Collapsed;
                        txtAdults.Text = "";
                        txtChildrens.Text = "";
                        tbCrlShow.Visibility = Visibility.Collapsed;
                        break;
                    }
                case "show":
                    {
                        grpGuestRequest.Visibility = Visibility.Collapsed;
                        btnAdd.Visibility = Visibility.Collapsed;
                        dgrAllGR.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseGR(thisGR);
                        break;
                    }
                default:
                    break;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (validation.IsNum(Convert.ToInt32(txtAdults.Text)) && validation.IsNum(Convert.ToInt32(txtChildrens.Text)))
                {
                    thisGR.RegistrationDate = DateTime.Today;
                    BL.BLFactory.getBL().AddCustomerReq(thisGR);
                    MessageBox.Show("Your Vacation request added to our program!", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnAdd.Visibility = Visibility.Collapsed;
                    grpGuestRequest.Visibility = Visibility.Collapsed;
                    tbCrlShow.Visibility = Visibility.Visible;
                    dgrAllGR.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseGR(thisGR);
                }
                //else
                //    throw new MyException("Please Enter Details Correctly!");

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

        private void txtFName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cmbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            thisGR.Area = (Area)cmbArea.SelectedItem;
        }

        private void cbmPool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void datePicEntery_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thisGR != null)
                thisGR.EntryDate = Convert.ToDateTime(datePicEntery.SelectedDate);
        }

        private void datePicRelease_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thisGR != null)
                thisGR.ReleaseDate = Convert.ToDateTime(datePicRelease.SelectedDate);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisGR = BL.BLFactory.getBL().UpdateCustomerReq(thisGR);
                MessageBox.Show("Your Vacation request Updated Successfully!", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);
                dgrAllGR.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseGR(thisGR);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            lblPass_Copy.Visibility = Visibility.Visible;
            txtPass_Copy.Visibility = Visibility.Visible;
            btnChangePasswordUpdate.Visibility = Visibility.Visible;
        }

        private void btnChangePasswordUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                thisGR = BL.BLFactory.getBL().UpdateAllPasswordCustomerReq(thisGR);
                MessageBox.Show("Your Paasword changed Successfully!", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgrAllGR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            temp = thisGR;
            thisGR = (GuestRequest)dgrAllGR.SelectedItem;
            btnUpdate.Visibility = Visibility.Visible;
            btnAddNew.Visibility = Visibility.Collapsed;
        }

        private void btnWantToAddNew_Click(object sender, RoutedEventArgs e)
        {
            thisGR = temp;
            datePicEntery_Copy.SelectedDate = DateTime.Today;
            datePicRelease_Copy.SelectedDate = DateTime.Today;
            btnAddNew.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Collapsed;
        }


        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((GuestRequest)(this.DataContext)).RegistrationDate = DateTime.Today;
                thisGR = BL.BLFactory.getBL().AddTheSameGuestRequest((GuestRequest)this.DataContext);
                MessageBox.Show("Your Vacation request Added Successfully!", "SUCCESS!", MessageBoxButton.OK, MessageBoxImage.Information);
                dgrAllGR.ItemsSource = BL.BLFactory.getBL().FindAllThePriviaseGR(thisGR);
            }
            catch (MyException ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void datePicEntery_Copy_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thisGR != null)
                thisGR.EntryDate = Convert.ToDateTime(datePicEntery_Copy.SelectedDate);
        }

        private void datePicRelease_Copy_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (thisGR != null)
                thisGR.ReleaseDate = Convert.ToDateTime(datePicRelease_Copy.SelectedDate);
        }

        private void grdAllGR_Loaded(object sender, RoutedEventArgs e)
        {
            btnUpdate.Visibility = Visibility.Visible;
            btnAddNew.Visibility = Visibility.Collapsed;
            datePicEntery_Copy.SelectedDate = thisGR.EntryDate;
            datePicRelease_Copy.SelectedDate = thisGR.ReleaseDate;
        }

        private void tbCrlShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void ChangeL()
        {
            lblPas.Name = "שם פרטי";
            lbfn.Name = "שם משפחה";
            lbemail.Name = "אי-מייל";
            btnChangePassword.Name = "שינוי הסיסמה";
            lblPass_Copy.Name = "בחר/י סיסמה";
            btnChangePasswordUpdate.Name = "אישור הסיסמה";
            btnAdd.Name = "הוספה";
        }

    }
}
