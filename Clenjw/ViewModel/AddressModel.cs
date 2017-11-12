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
using lindexi.uwp.Framework.ViewModel;
using Newtonsoft.Json;

namespace lindexi.uwp.Clenjw.ViewModel
{
    public class AddressModel:ViewModelMessage
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
                    await OpenAccountCleDisp(file);
                }
            }
        }

        public async Task OpenAccountCleDisp(StorageFile file)
        {
            var account = new FileClen(file);
          
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
             () =>
             {
                 if (File.All(temp => !temp.Equal(file)))
                 {
                     AccountGoverment.View.Account.File.Add(account);
                     File.Add(account);
                 }
                 AccountGoverment.View.File = account;
                 AccountGoverment.View.NagitavePreface();
             });
           

        }

        public void Maddress()
        {
            for (int i = 0; i < AccountGoverment.View.Account.File.Count; i++)
            {
                if (AccountGoverment.View.Account.File[i].Check)
                {
                    AccountGoverment.View.Account.File.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].Check)
                {
                    File.RemoveAt(i);
                    i--;
                }
            }
            //AccountGoverment.View.Account.File.RemoveAll(temp => temp.Check);
        }

        public override void OnNavigatedFrom(object sender, object obj)
        {
            
        }

        public override void OnNavigatedTo(object sender, object obj)
        {
          
        }
    }
}
