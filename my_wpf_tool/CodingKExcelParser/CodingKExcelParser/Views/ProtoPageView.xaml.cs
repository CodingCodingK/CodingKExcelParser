using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CodingKExcelParser.Models;
using CodingKExcelParser.ViewModels;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CodingKExcelParser.Views
{
    /// <summary>
    /// ProtoPageView.xaml 的交互逻辑
    /// </summary>
    public partial class ProtoPageView : Window
    {
        public ProtoPageView()
        {
            InitializeComponent();
        }

        #region Drop

        private void InputPathView_OnDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            var pathList = files?.ToList();
            if (pathList?.Count > 0 && DataContext is ProtoPageViewModel vm)
            {
                foreach (var path in pathList)
                {
                    if (!path.Contains(".proto") && !path.Contains(".cs"))
                    {
                        continue;
                    }

                    vm.InputFiles.Add(new FilePathModel
                    {
                        Path = path,
                        Name = path.Substring(path.LastIndexOf('\\') + 1),
                        IsSelected = true,
                    });
                }
            }
        }
        #endregion



        #region Click
        private void InputPath_Click(object sender, RoutedEventArgs e)
        {
            var pathList = PathFile_Click("cs文件|*.cs|proto文件|*.proto", true)?.ToList(); //|所有文件|*.*
            if (pathList?.Count > 0 && DataContext is ProtoPageViewModel vm)
            {
                foreach (var path in pathList)
                {
                    vm.InputFiles.Add(new FilePathModel
                    {
                        Path = path,
                        Name = path.Substring(path.LastIndexOf('\\') + 1),
                        IsSelected = true,
                    });
                }
            }
        }

        private void InputPath_Clear(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProtoPageViewModel vm) vm.InputFiles.Clear();
        }

        private void OutputPath_Click(object sender, RoutedEventArgs e)
        {
            var path = PathDic_Click();
            if (!string.IsNullOrEmpty(path) && DataContext is ProtoPageViewModel vm)
            {
                vm.OutputPath.Value = path;
            }
        }

        private string[] PathFile_Click(string filter, bool isMulti)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Title = "选择数据源文件";
            dlg.Multiselect = isMulti;

            if (!string.IsNullOrEmpty(filter))
            {
                dlg.Filter = filter;
                dlg.FilterIndex = 1;
            }
            // openFileDialog.FileName = string.Empty;
            // openFileDialog.RestoreDirectory = true;
            // openFileDialog.DefaultExt = "txt";
            var result = dlg.ShowDialog();
            if (result == true)
            {
                return dlg.FileNames;
            }

            return null;
        }

        private string PathDic_Click()
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.Title = "选择数据源文件";
            dlg.IsFolderPicker = true;

            var result = dlg.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                return dlg.FileName;
            }

            return null;
        }
        #endregion
    }
}
