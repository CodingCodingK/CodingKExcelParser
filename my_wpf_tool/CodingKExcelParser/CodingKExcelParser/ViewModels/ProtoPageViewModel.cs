using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CodingK_WpfMvvmBase;
using CodingKExcelParser.Models;
using CodingKExcelParser.Utility;
using Microsoft.Win32;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace CodingKExcelParser.ViewModels
{
    public class ProtoPageViewModel : ViewModelBase
    {
        const string batFileName = "CodingK_temp.bat";
        const string toolDicName = "protogen";
        const string toolFileName = "protogen.exe";
        const string protoDicName = "CodingK_protos";

        private string ToolDicPath;
        private string ToolFilePath;
        private string BatFilePath;
        private string ProtoDicPath;

        public ReactiveCollection<FilePathModel> InputFiles { get; set; }
        public ReactiveProperty<string> OutputPath { get; set; }
        public ReactiveCommand SendCommand { get; set; }

        protected override void InitData()
        {
            InputFiles = new ReactiveCollection<FilePathModel>().AddTo(Disposables);
            OutputPath = new ReactiveProperty<string>().AddTo(Disposables);

            // path
            var curtPath = Directory.GetCurrentDirectory();
            var latIndex = curtPath?.LastIndexOf("bin") ?? curtPath.Length;

            ToolDicPath = curtPath.Substring(0, latIndex) + toolDicName;
            ToolFilePath = ToolDicPath + "\\" + toolFileName;
            BatFilePath = ToolDicPath + "\\" + protoDicName + "\\" + batFileName;
            ProtoDicPath = ToolDicPath + "\\" + protoDicName;
        }
        
        protected override void RegisterCommand()
        {
            SendCommand = new ReactiveCommand().WithSubscribe(Send).AddTo(Disposables);
        }

        void Send()
        {
            var unconvertedList = InputFiles.Where(o => o.IsSelected).ToList();
            var outputList = new List<FilePathModel>();
            
            // .cs to .proto
            foreach (var file in unconvertedList)
            {
                var temp = new FilePathModel();
                if (file.Name.Contains(".cs"))
                {
                    // TODO .cs => .proto 暂不开放
                    continue;

                    temp.Path = file.Path.Replace(".cs", ".proto");
                    temp.Name = file.Name.Replace(".cs", ".proto");
                    temp.IsSelected = true;
                    Cs2ProtoUtility.ConvertCsToProto(file.Path, temp.Path);
                }
                else if (file.Name.Contains(".proto"))
                {
                    temp.Path = file.Path;
                    temp.Name = file.Name;
                    temp.IsSelected = true;
                }
                outputList.Add(temp);
            }
            
            // .proto copy
            List<string> batContent = new List<string>();
            foreach (var file in outputList)
            {
                FileUtility.CopyFile(file.Path, ProtoDicPath + "\\" + file.Name);
                batContent.Add(ToolFilePath + @" --csharp_out=" + OutputPath.Value + @" .\" + file.Name);
            }

            // bat
            FileUtility.WriteBATFile(BatFilePath, batContent.ToArray());
            FileUtility.ExistDic(OutputPath.Value);
            FileUtility.ExecuteBATFile(ProtoDicPath, BatFilePath);
        }
    }
}
