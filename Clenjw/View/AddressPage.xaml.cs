using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using lindexi.uwp.Clenjw.ViewModel;
using lindexi.uwp.Framework.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace lindexi.uwp.Clenjw.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    [ViewModel(ViewModel=typeof(AddressModel))]
    public sealed partial class AddressPage : Page
    {
        public AddressPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            View = new AddressModel();
            StorageFile file = e.Parameter as StorageFile;
            if (file != null)
            {
                await AccountGoverment.View.Read();
                await View.OpenAccountCleDisp(file);

            }
            base.OnNavigatedTo(e);
        }

        private AddressModel View { set; get; }

    }
}
