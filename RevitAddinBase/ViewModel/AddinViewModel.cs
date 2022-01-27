using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddinBase.ViewModel
{
    public class AddinViewModel : DependencyObject, INotifyPropertyChanged
    {
        public static DependencyProperty IsVisibleProperty;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool visibility;

        public bool Visibility
        {
            get => visibility; 
            set 
            { 
                visibility = value;
                OnPropertyChanged();
            }
        }


        public bool IsVisible
        {
            get { return (bool)base.GetValue(IsVisibleProperty); }
            set 
            { 
                base.SetValue(IsVisibleProperty, value);
                OnPropertyChanged();
            }
        }

        static AddinViewModel()
        {
            IsVisibleProperty = DependencyProperty.Register(nameof(IsVisible), typeof(bool), typeof(AddinViewModel));
        }

        public AddinViewModel()
        {
            IsVisible = true;
            Visibility = true;
        }

        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
