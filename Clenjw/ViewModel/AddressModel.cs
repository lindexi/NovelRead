using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using lindexi.uwp.Clenjw.Model;
using Newtonsoft.Json;

namespace lindexi.uwp.Clenjw.ViewModel
{
    public class AddressModel
    {
        public AddressModel()
        {
            //File.Add(new FileClen()
            //{
            //    Name = "德熙"
            //});
            Read();
        }

        public ObservableCollection<FileClen> File { set; get; }
        = new ObservableCollection<FileClen>();

        private async void Read()
        {
            //await account.Read();
            await AccountGoverment.View.Read();
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    var account = AccountGoverment.View.Account;
                    if (account.File == null)
                    {
                        return;
                    }
                    foreach (var temp in account.File)
                    {
                        File.Add(temp);
                    }
                });
        }

        public void NagitavePreface(object sender, ItemClickEventArgs e)
        {
            AccountGoverment.View.File = e.ClickedItem as FileClen;
            AccountGoverment.View.NagitavePreface();
        }


        public async void Open()
        {
            FileOpenPicker pick = new FileOpenPicker();
            pick.FileTypeFilter.Add(".txt");
            var file = await pick.PickSingleFileAsync();
            if (file != null)
            {
                //不存在
                if (File.All(temp => !temp.Equal(file)))
                {
                    var account = new FileClen(file);
                    AccountGoverment.View.Account.File.Add(account);
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                     () =>
                     {
                         File.Add(account);
                     });
                }
            }
        }

        public void Maddress()
        {

        }
    }
}
