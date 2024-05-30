using CommonServiceLocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiP_Tool.Shared;
using System.IO;
using PiP_Tool.MachineLearning;

namespace PiP_Tool.Services
{
    public class WindowDataService
    {
        private readonly string _filePath;

        public static WindowDataService Instance => _instance ?? (_instance = new WindowDataService());
        private static WindowDataService _instance;
        public WindowDataService()
        {
            _filePath = Shared.Constants.PositionPath;
        }

        public void SaveData(WindowData windowData)
        {
            var dataList = LoadAllData();

            var existingData = dataList.FirstOrDefault(x => x.Title == windowData.Title);
            if (existingData != null)
            {
                existingData.Top = windowData.Top;
                existingData.Left = windowData.Left;
            }
            else
            {
                dataList.Add(windowData);
            }

            WriteToFile(dataList);
        }

        public WindowData LoadData(string title)
        {
            var dataList = LoadAllData();
            return dataList.FirstOrDefault(x => x.Title == title);
        }

        public List<WindowData> LoadAllData()
        {
            if (!File.Exists(_filePath))
            {
                return new List<WindowData>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<WindowData>>(json) ?? new List<WindowData>();
        }

        private void WriteToFile(List<WindowData> dataList)
        {
            var json = JsonConvert.SerializeObject(dataList, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
