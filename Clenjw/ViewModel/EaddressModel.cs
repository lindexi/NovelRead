using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using lindexi.uwp.Clenjw.Model;

namespace lindexi.uwp.Clenjw.ViewModel
{
    public class EaddressModel : NotifyProperty
    {
        public EaddressModel()
        {
            Read();
            Np = 1000;
        }

        private string _str;

        public string Str
        {
            set
            {
                _str = value;
                OnPropertyChanged();
            }
            get
            {
                return _str;
            }
        }

        public double Width
        {
            set;
            get;
        }

        public double Height
        {
            set;
            get;
        }

        private async void Read()
        {
            var account = AccountGoverment.View;
            if (account.File?.File == null)
            {
                account.NagitaveAddress();
                return;
            }


            //Stream temp = await account.File.File.OpenStreamForReadAsync();
            if (string.IsNullOrEmpty(account.File.Str))
            {
                //using (StreamReader stream = new StreamReader(
                //  await account.File.File.OpenStreamForReadAsync()))
                //{
                await account.File.Read();
                Progress();
                //}
            }
        }

        public Visibility AccountVisibility
        {
            set
            {
                _accountVisibility = value;
                OnPropertyChanged();
            }
            get
            {
                return _accountVisibility;
            }
        }

        private Visibility _accountVisibility;

        private int Np
        {
            set;
            get;
        }

        public void Up()
        {
            NpClj();
            var account = AccountGoverment.View.File;
            int n = account.Poit - Np;

            if (account.Poit < 0)
            {
                account.Poit = 0;
            }

            int length = account.Str.Length - account.Poit;
            length = length > Np ? Np : length;
            Str = account.Str.Substring(n, length);
            account.Poit = n;
        }


        public void Progress()
        {
            NpClj();
            var account = AccountGoverment.View.File;
            int n = account.Poit + Np;

            int length = account.Str.Length - account.Poit;

            length = length > Np ? Np : length;
            if (length > 0)
            {
                Str = account.Str.Substring(n, length);

                account.Poit = n;
            }
        }

        private void NpClj()
        {
            Width = Window.Current.Bounds.Width;

        }

    }
}
