﻿using DoAnLTTQ.Backend;
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

namespace DoAnLTTQ.Components
{
    /// <summary>
    /// Interaction logic for GridMessage.xaml
    /// </summary>
    public partial class GridMessage : UserControl
    {
        public GuestProfile guest;
        public List<GuestProfile> userList;
        public event EventHandler<string> ProfileSelected;
        public GridMessage()
        {

            InitializeComponent();
            guest = new GuestProfile();
            List<GuestProfile> friends = new List<GuestProfile>();
            userList = guest.LoadArrayProfile();
            foreach(var u in userList)
            {
                if(u.isLove == true)
                {
                    friends.Add(u);
                }
            }

            ChatList.DataContext = friends;
        }
        private void select_Click(object sender, RoutedEventArgs e)
        {
            string identify = ((sender as Button).Uid).ToString();
            //profileSelecting(identify);
            if (ProfileSelected != null)
            {
                ProfileSelected(this, identify);
            }
        }
        public void reload()
        {
            guest = new GuestProfile();
            List<GuestProfile> friends = new List<GuestProfile>();
            userList = guest.LoadArrayProfile();
            foreach (var u in userList)
            {
                if (u.isLove == true)
                {
                    friends.Add(u);
                }
            }

            ChatList.DataContext = friends;
        }

    }
}
