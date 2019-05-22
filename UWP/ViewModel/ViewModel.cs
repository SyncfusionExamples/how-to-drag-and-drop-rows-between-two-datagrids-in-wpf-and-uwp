using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfDataGridDemo
{
    public class UserInfoViewModel : INotifyPropertyChanged, IDisposable
    {
        public UserInfoViewModel()
        {
            UserDetails = new UserInfoRepository().GetUserDetails(100);
            UserDetails1 = new UserInfoRepository().GetUserDetails1(100);
        }

        private ObservableCollection<UserInfo> userDetails;
        public ObservableCollection<UserInfo> UserDetails
        {
            get { return userDetails; }
            set { userDetails = value; RaisePropertyChanged("UserDetails"); }
        }

        private ObservableCollection<UserInfo> userDetails1;
        public ObservableCollection<UserInfo> UserDetails1
        {
            get { return userDetails1; }
            set { userDetails1 = value; RaisePropertyChanged("UserDetails1"); }
        }

        public void Dispose()
        {
            if (UserDetails != null)
                UserDetails.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
