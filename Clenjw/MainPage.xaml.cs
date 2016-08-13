using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using lindexi.uwp.Clenjw.ViewModel;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace lindexi.uwp.Clenjw
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            View =AccountGoverment.View;
            this.InitializeComponent();
            View.Frame = frame;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            View.NagitaveAddress();
        }

        private AccountGoverment View { set; get; }
    }
}
