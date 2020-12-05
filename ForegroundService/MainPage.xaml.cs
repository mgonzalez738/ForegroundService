using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForegroundService
{
    public partial class MainPage : ContentPage
    {
        public static event Action NotificationEvent;

        public MainPage()
        {
            InitializeComponent();
        }

        private void startservicebutton_Clicked(object sender, EventArgs e)
        {
            NotificationEvent();
        }

        

        
    }
}
