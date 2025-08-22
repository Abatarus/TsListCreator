using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Avalonia.Media;
using TSListCreator.Controls;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class SaveLoadService: ISaveLoadService
    {
        private IFilePickerService _filePickerService;

        public SaveLoadService(IFilePickerService filePickerService)
        {
            _filePickerService = filePickerService;
        }
        public async Task Save(IJsonInput settings, IEnumerable<IJsonInput> textBoxes, IEnumerable<IJsonInput> counters, IEnumerable<IJsonInput> checkBoxes)
        {
            JsonArray textBoxesArray = new JsonArray();
            foreach (var textBox in textBoxes)
            {
                textBoxesArray.Add(textBox.GetJsonObject());
            }
            JsonArray countersArray = new JsonArray();
            foreach (var counter in counters)
            {
                countersArray.Add(counter.GetJsonObject());
            }
            JsonArray checkBoxesArray = new JsonArray();
            foreach (var checkBox in checkBoxes)
            {
                checkBoxesArray.Add(checkBox.GetJsonObject());
            }

            JsonObject result = new JsonObject
            {
                ["settings"] = settings.GetJsonObject(),
                ["textboxes"] = textBoxesArray,
                ["counters"] = countersArray,
                ["checkBoxes"] = checkBoxesArray
            };
            await _filePickerService.SaveJsonToFile(result);
        }

        public async Task<DataHolder> Load()
        {
            DataHolder holder = new DataHolder();
            JsonDocument document = JsonDocument.Parse(await _filePickerService.LoadJsonFile());

            var settingsElem = document.RootElement.GetProperty("settings");
            holder.Settings.BoundWidth = settingsElem.GetProperty("bound_width").GetDouble();
            holder.Settings.BoundHeight = settingsElem.GetProperty("bound_height").GetDouble();
            holder.Settings.Background = Color.FromUInt32(settingsElem.GetProperty("background").GetUInt32());
            holder.Settings.FontColor = Color.FromUInt32(settingsElem.GetProperty("font_color").GetUInt32());

            var textBoxesElem = document.RootElement.GetProperty("textboxes");

            foreach (var textBoxElem in textBoxesElem.EnumerateArray())
            {
                holder.TextBoxes.Add(new TsTextBox()
                {
                    Alignment = (AlignmentId)textBoxElem.GetProperty("alignment").GetInt32(),
                    FontSize = textBoxElem.GetProperty("font_size").GetDouble(),
                    PosX = textBoxElem.GetProperty("pos")[0].GetDouble(),
                    PosY = textBoxElem.GetProperty("pos")[2].GetDouble(),
                    Width = textBoxElem.GetProperty("width").GetDouble(),
                    Value = textBoxElem.GetProperty("value").GetString(),
                    Label = textBoxElem.GetProperty("label").GetString()
                });
            }

            return holder;
        }
    }
}
