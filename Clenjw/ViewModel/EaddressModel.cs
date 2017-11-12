using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using lindexi.uwp.Clenjw.Model;
using lindexi.uwp.Framework.ViewModel;

namespace lindexi.uwp.Clenjw.ViewModel
{
    public class EaddressModel : ViewModelMessage
    {
        public EaddressModel()
        {
            Read();
            //Np = 1000;
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

            if (string.IsNullOrEmpty(account.File.Str))
            {
                await account.File.Read();

                if (account.Account.Font == 0)
                {
                    account.Account.Font = 20;
                }

                if (account.Account.Line == 0)
                {
                    account.Account.Line = 10;
                }
            }
            Progress();

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

        //private int Np
        //{
        //    set;
        //    get;
        //}

        public void Up()
        {
            NpClj();

            string str = AccountGoverment.View.File.Up(
                (int) AccountGoverment.View.Account.Font,
                AccountGoverment.View.Account.Line);
            if (str == "")
            {
                return;
            }
            Str = str;
        }


        public void Progress()
        {
            NpClj();

            string str = AccountGoverment.View.File.Progress(
                (int) AccountGoverment.View.Account.Font,
                AccountGoverment.View.Account.Line);
            if (string.IsNullOrEmpty(str))
            {
                return;
            }
            Str = str;
        }

        private void NpClj()
        {
            //计算宽度
            Width = Window.Current.Bounds.Width;
        }

        public override void OnNavigatedFrom(object sender, object obj)
        {
            
        }

        public override void OnNavigatedTo(object sender, object obj)
        {
            
        }
    }
}
