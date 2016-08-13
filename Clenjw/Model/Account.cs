using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Core;
using Newtonsoft.Json;

namespace lindexi.uwp.Clenjw.Model
{
    public class Account
    {
        public Account()
        {

        }

        public List<FileClen> File { set; get; }=new List<FileClen>();

        //    public async Task Read()
        //    {
        //        if (File != null)
        //        {
        //            return;
        //        }
        //        File=new List<FileClen>();
        //        //读取文件夹 App/file.json
        //        string str = "App";
        //        try
        //        {
        //            var folder = await ApplicationData.Current.
        //                RoamingFolder.GetFolderAsync(str);

        //            str = "file.json";
        //            var file = await FileJson<List<FileClen>>(await folder.GetFileAsync(str));

        //            foreach (var temp in await FileStorageApplicationPermiss())
        //            {
        //                var fileStorage = file.FirstOrDefault(clen => clen.Name == temp.DisplayName);
        //                if (fileStorage != null)
        //                {
        //                    fileStorage.File = temp;
        //                }
        //            }

        //            File.Clear();
        //            foreach (var temp in file)
        //            {
        //                File.Add(temp);
        //            }
        //        }
        //        catch
        //        {

        //        }

        //    }

        //    public void Storage()
        //    {

        //    }
        //    private async Task<T> FileJson<T>(IStorageFile file)
        //    {
        //        using (TextReader stream =
        //               new StreamReader(await
        //                 file.OpenStreamForReadAsync()))
        //        {
        //            var json = JsonSerializer.Create();
        //            return json.Deserialize<T>(new JsonTextReader(stream));
        //        }
        //    }

        //    private async Task<List<StorageFile>> FileStorageApplicationPermiss()
        //    {
        //        var folder = new List<StorageFile>();

        //        foreach (var temp in StorageApplicationPermissions.
        //            FutureAccessList.Entries)
        //        {
        //            folder.Add(await StorageApplicationPermissions.
        //                FutureAccessList.GetFileAsync(temp.Token));
        //        }

        //        for (int i = 0; i < folder.Count; i++)
        //        {

        //            FileInfo temp = new FileInfo(folder[i].Path);
        //            if (!temp.Exists)
        //            {
        //                folder.RemoveAt(i);
        //                i--;
        //            }
        //        }

        //        return folder;
        //    }
    }
}
