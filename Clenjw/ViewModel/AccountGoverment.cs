// lindexi
// 21:04

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using lindexi.uwp.Clenjw.Model;
using lindexi.uwp.Clenjw.View;
using Newtonsoft.Json;

namespace lindexi.uwp.Clenjw.ViewModel
{
    public class AccountGoverment
    {
        public AccountGoverment()
        {
        }

        public Account Account
        {
            set;
            get;
        }


        public FileClen File
        {
            set;
            get;
        }

        public static AccountGoverment View
        {
            set
            {
                _accountGoverment = value;
            }
            get
            {
                return _accountGoverment ?? (_accountGoverment = new AccountGoverment());
            }
        }

        public Frame Frame
        {
            set;
            get;
        }

        public void NagitaveAddress()
        {
            Frame?.Navigate(typeof(AddressPage));
        }

        public void NagitavePreface()
        {
            Frame?.Navigate(typeof(Preface));
        }

        public async Task Read()
        {
            string str = "App";
            if (Account != null)
            {
                return;
            }
            try
            {
                var folder = await ApplicationData.Current.
                    RoamingFolder.GetFolderAsync(str);
                str = "file.json";
                Account = await FileJson<Account>(await folder.GetFileAsync(str));
            }
            catch
            {
            }

            if (Account == null)
            {
                Account = new Account();
            }
            //if (Account.File != null)
            //{
            //    return;
            //}
            //Account.File = new List<FileClen>();
            ////读取文件夹 App/file.json
            //string str = "App";
            //try
            //{
            //    var folder = await ApplicationData.Current.
            //        RoamingFolder.GetFolderAsync(str);

            //    str = "file.json";
            //    var file = await FileJson<List<FileClen>>(await folder.GetFileAsync(str));

            foreach (var temp in await FileStorageApplicationPermiss())
            {
                var fileStorage = Account.File.FirstOrDefault(clen => clen.Name == temp.DisplayName);
                if (fileStorage != null)
                {
                    fileStorage.File = temp;
                }
            }
            Account.File.RemoveAll(temp => temp.File == null);


            //   Account.File.Clear();
            //    foreach (var temp in file)
            //    {
            //        Account.File.Add(temp);
            //    }
            //}
            //    catch
            //    {

            //    }
        }


        public async Task Storage()
        {
            try
            {
                StorageApplicationPermissions.FutureAccessList.Clear();
                foreach (var temp in Account.File)
                {
                    StorageApplicationPermissions.FutureAccessList.Add(temp.File);
                }
                string str = "App";

                StorageFolder folder;
                try
                {
                    folder = await ApplicationData.Current.
                        RoamingFolder.GetFolderAsync(str);
                }
                catch
                {
                    folder = await ApplicationData.Current.RoamingFolder.
                        CreateFolderAsync(str);
                }

                str = "file.json";
                var file = await folder.CreateFileAsync(str, CreationCollisionOption.ReplaceExisting);
                using (TextWriter stream = new StreamWriter(
                    await file.OpenStreamForWriteAsync()))
                {
                    var json = JsonSerializer.Create();
                    json.Serialize(stream, Account);
                }
            }
            catch
            {
            }
        }

        private async Task<List<StorageFile>> FileStorageApplicationPermiss()
        {
            var folder = new List<StorageFile>();

            foreach (var temp in StorageApplicationPermissions.
                FutureAccessList.Entries)
            {
                try
                {
                    folder.Add(await StorageApplicationPermissions.
                        FutureAccessList.GetFileAsync(temp.Token));
                }
                catch 
                {
                    
                }
            }

            for (int i = 0; i < folder.Count; i++)
            {
                //FileInfo temp = new FileInfo(folder[i].Path);
                //if (!temp.Exists)
                //{
                //    folder.RemoveAt(i);
                //    i--;
                //}
            }

            return folder;
        }

        private async Task<T> FileJson<T>(IStorageFile file)
        {
            using (TextReader stream =
                new StreamReader(await
                    file.OpenStreamForReadAsync()))
            {
                var json = JsonSerializer.Create();
                //string str =await stream.ReadToEndAsync();
                return json.Deserialize<T>(new JsonTextReader(stream));
            }
        }

        private static AccountGoverment _accountGoverment;
    }
}