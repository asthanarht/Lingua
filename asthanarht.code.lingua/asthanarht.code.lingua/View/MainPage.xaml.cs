using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using asthanarht.code.lingua.ViewModel;

namespace asthanarht.code.lingua.View
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            Title = "ViewProfile";

            BindingContext = vm = new MainViewModel(this);  

        }
    }
}
