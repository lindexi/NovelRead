﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using lindexi.uwp.Clenjw.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace lindexi.uwp.Clenjw.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Preface : Page
    {
        public Preface()
        {
            View=new EaddressModel();
            this.InitializeComponent();
            DataContext = View;
        }

        private EaddressModel View { set; get; }

        private void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            var temp = sender as FrameworkElement;
            if (temp == null)
            {
                //var grid = sender as Button;
                
                //foreach (var g in grid.Children)
                //{
                    
                //}
                return;
            }
            var n = e.GetCurrentPoint(temp).RawPosition;
            double length = temp.ActualWidth - temp.ActualWidth*0.3;
            if (n.X > length)
            {
                View.Progress();
            }
            else if (n.X < temp.ActualWidth*0.3)
            {
                View.Up();
            }

            
        }
    }
}
