﻿using DoAnLTTQ.Views;
using DoAnLTTQ.Backend;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for NavBarMain.xaml
    /// </summary>
    public partial class NavBarMain : UserControl, INotifyPropertyChanged
    {
        public UserControl _TabChange;


        //public GridProfile gridProfile = new GridProfile();
        //public GridMessage gridMessage = new GridMessage(); 
        public event ClickOnButtonHandler ButtonSwitchViewOnClick;
        public UserControl TabChange
        {
            get { return this._TabChange; }
            set
            {
                _TabChange = value;
                OnPropertyChanged("TabChange");
            }
        }
        public User _myUser;
        public User myUser
        {
            get { return this._myUser; }
            set
            {
                _myUser = value;
                OnPropertyChanged("myUser");
            }
        }
        public NavBarMain()
        {
            InitializeComponent();

            //this.TabChange = gridProfile; 
            //this.DataContext = this; 
            gridProfile.Visibility = Visibility.Visible;
            gridMessage.Visibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
     
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void buttonQuanhDay_Click(object sender, RoutedEventArgs e)
        {
            gridProfile.Visibility = Visibility.Visible;
            gridMessage.Visibility = Visibility.Collapsed;
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.QuanhDayView);
        }

        private void buttonTinNhan_Click(object sender, RoutedEventArgs e)
        {
            gridProfile.Visibility = Visibility.Collapsed;
            gridMessage.Visibility = Visibility.Visible;
            gridMessage.reload();
            if (ButtonSwitchViewOnClick != null) { }
                ButtonSwitchViewOnClick(ViewEnum.MessageView);



        }

        private void ToSettingViewButton_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonSwitchViewOnClick != null)
                ButtonSwitchViewOnClick(ViewEnum.SettingView);
           
        }
        public void Reload_myProfile()
        {
            //MessageBox.Show("reload");
            myUser = new User();
            this.profile.DataContext = myUser.myProfile;
        }
    }
}
