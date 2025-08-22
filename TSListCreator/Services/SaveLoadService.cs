using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TSListCreator.Interfaces;

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
                ["Settings"] = settings.GetJsonObject(),
                ["TextBoxes"] = textBoxesArray,
                ["Counters"] = countersArray,
                ["CheckBoxes"] = checkBoxesArray
            };
            using MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(result.ToString()));
            await _filePickerService.SaveToFile(stream);
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}
