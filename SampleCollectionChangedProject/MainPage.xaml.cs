using Windows.UI.Xaml.Controls;

namespace SampleCollectionChangedProject
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = new MainPageViewModel(); // Set up the VM in a quick and dirty way
        }
    }
}
