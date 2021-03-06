﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using MinimapGen.MapGenerator;

namespace MinimapGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Core core;
        private static int style;
        private static bool expandEdge;
        private static String version="1.2.3";

        public static bool ExpandEdge => expandEdge;

        public static int Style1 => style;

        public MainWindow()
        {
            InitializeComponent();  
            core = new Core();
            style = 0;
            ProcessUpdate(IOUtility.checkUpdate(version));
        }

        private void ProcessUpdate(string downloadUrl)
        {
            if (downloadUrl!=null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("发现新版本，是否前往下载", "更新", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Process.Start(downloadUrl);
                    Application.Current.Shutdown(-1);
                }
            }
        }

        private void onPreview(object sender, RoutedEventArgs e)
        {
            if (style==0)
            {
                MessageBox.Show("请选择风格");
                return;
            }
            if (core.main())
            {
                this.minimap.Source = IOUtility.BitmapToImageSource(core.Minimap);
            }
            else
            {
                MessageBox.Show("请先选择地图");
            }
        }

        private void onChooseMap(object sender, RoutedEventArgs e)
        {
            ChooseMapDialog chooseMapDialog = new ChooseMapDialog(core.MapNames);
            if (chooseMapDialog.ShowDialog() == true)
            {
                this.mapName.Content=core.SavePath=core.Filepath = chooseMapDialog.selectMap;
            }
        }

        private void onChooseColor(object sender, RoutedEventArgs e)
        {
            colorInputDialog colorInputDialog=new colorInputDialog(MapHelper.customColor);
            if (colorInputDialog.ShowDialog()==true)
            {
                
            }
        }

        private void onChooseStyle(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            style = radioButton.Name[5]-'0';
            if (style==4)
            {
                this.ChooseColorBtn.IsEnabled = true;
            }
            else
            {
                this.ChooseColorBtn.IsEnabled = false;
            }
        }

        private void onSave(object sender, RoutedEventArgs e)
        {
            if (File.Exists(core.SavePath))
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("要删除原来的小地图吗?", "保存小地图", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    File.Delete(core.SavePath);
                    IOUtility.SaveTGA(core.Minimap,core.SavePath);
                    MessageBox.Show("保存成功");
                }
            }
            else
            {
                IOUtility.SaveTGA(core.Minimap,core.SavePath);
                MessageBox.Show("保存成功");
            }
        }

        private void onCheckExpandEdge(object sender, RoutedEventArgs e)
        {
            expandEdge = (bool) expandEdgeCheck.IsChecked;
        }

        private void onAbout(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitee.com/wu162/Ra3MinimapGenerator");
        }
    }
}