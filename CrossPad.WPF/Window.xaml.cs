using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace CrossPad.WPF
{
    public partial class Window : FormsApplicationPage
    {
        public Window()
        {
            InitializeComponent();
            Forms.Init();
            // Load the shared Forms from the main project
            LoadApplication(new CrossPad.App());
        }
    }
}
