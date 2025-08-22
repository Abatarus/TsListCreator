using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using TSListCreator.Controls;
using TSListCreator.Interfaces;
using TSListCreator.Utils;

namespace TSListCreator.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private ITopLevelService _topLevelService;

        public SaveLoadService(ITopLevelService topLevelService)
        {
            _topLevelService = topLevelService;
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
                ["checkboxes"] = checkBoxesArray
            };
            await _topLevelService.SaveJsonToFile(result);
        }

        public async Task<DataHolder> Load(ISettingsService settingsService)
        {
            DataHolder holder = new DataHolder();
            var jsonString = await _topLevelService.LoadJsonFile();
            if (jsonString == null)
            {
                throw new Exception("File not loaded");
            }
            JsonDocument document = JsonDocument.Parse(jsonString);

            var settingsElem = document.RootElement.GetProperty("settings");
            holder.Settings.BoundWidth = settingsElem.GetProperty("bound_width").GetDouble();
            settingsService.BoundWidth = holder.Settings.BoundWidth;
            holder.Settings.BoundHeight = settingsElem.GetProperty("bound_height").GetDouble();
            settingsService.BoundHeight = holder.Settings.BoundHeight;
            holder.Settings.Background = Color.FromUInt32(settingsElem.GetProperty("background").GetUInt32());
            holder.Settings.FontColor = Color.FromUInt32(settingsElem.GetProperty("font_color").GetUInt32());

            var textBoxesElem = document.RootElement.GetProperty("textboxes");

            foreach (var elem in textBoxesElem.EnumerateArray())
            {
                holder.TextBoxes.Add(new TsTextBox()
                {
                    Alignment = (AlignmentId)elem.GetProperty("alignment").GetInt32(),
                    FontSize = elem.GetProperty("font_size").GetDouble(),
                    PosX = elem.GetProperty("pos")[0].GetDouble(),
                    PosY = elem.GetProperty("pos")[2].GetDouble(),
                    Width = elem.GetProperty("width").GetDouble(),
                    Value = elem.GetProperty("value").GetString(),
                    Label = elem.GetProperty("label").GetString(),
                    Name = elem.GetProperty("name").GetString()!
                });
            }

            var checkBoxesElem = document.RootElement.GetProperty("checkboxes");

            foreach (var elem in checkBoxesElem.EnumerateArray())
            {
                holder.CheckBoxes.Add(new TsCheckBox()
                {
                    PosX = elem.GetProperty("pos")[0].GetDouble(),
                    PosY = elem.GetProperty("pos")[2].GetDouble(),
                    Size = elem.GetProperty("size").GetDouble(),
                    State = elem.GetProperty("state").GetBoolean(),
                    Name = elem.GetProperty("name").GetString()!
                });
            }

            return holder;
        }
        public async void SaveToClipBoard(ILuaInput settings,
            IEnumerable<ILuaInput> textBoxes,
            IEnumerable<ILuaInput> counters,
            IEnumerable<ILuaInput> checkBoxes)
        {
            StringBuilder builder = new StringBuilder("" +
                                                         "--[[    Character Sheet Template    by: MrStump\r\n" +
                                                         "\r\n    Begin editing below:    ]]\r\n" +
                                                         "\r\n--Set this to true while editing and false when you have finished" +
                                                         "\r\ndisableSave = true\r\n" +
                                                         "--Remember to set this to false once you are done making changes" +
                                                         "\r\n--Then, after you save & apply it, save your game too\r\n" +
                                                         "\r\n--Color information for button text (r,g,b, values of 0-1)" +
                                                         "\r\nbuttonFontColor = {1,1,0.9}" +
                                                         "\r\n--Color information for button background" +
                                                         "\r\nbuttonColor = {0.04,0,0.06}" +
                                                         "\r\n--Change scale of button (Avoid changing if possible)" +
                                                         "\r\nbuttonScale = {0.1,0.1,0.1}");
            builder.Append(@"
defaultButtonData = {
    --Add checkboxes
    checkbox = {
        --[[
            pos   = the position (pasted from the helper tool)
            size  = height/width/font_size for checkbox
            state = default starting value for checkbox (true=checked, false=not)
            ]]");
            foreach (var checkBox in checkBoxes)
            {
                builder.Append(checkBox.GetLuaString());
            }
            builder.Append(@"--End of checkboxes
    },
    --Add counters that have a + and - button
    counter = {
        --[[
            pos    = the position (pasted from the helper tool)
            size   = height/width/font_size for counter
            value  = default starting value for counter
            hideBG = if background of counter is hidden (true=hidden, false=not)
            ]]");
            foreach (var counter in counters)
            {
                builder.Append(counter.GetLuaString());
            }
            builder.Append(@"--End of counters
    },
    --Add editable text boxes
    textbox = {
        --[[
            pos       = the position (pasted from the helper tool)
            rows      = how many lines of text you want for this box
            width     = how wide the text box is
            font_size = size of text. This and ""rows"" effect overall height
            label     = what is shown when there is no text. """" = nothing
            value     = text entered into box. """" = nothing
            alignment = Number to indicate how you want text aligned
                        (1=Automatic, 2=Left, 3=Center, 4=Right, 5=Justified)
            ]]");
            foreach (var textBox in textBoxes)
            {
                builder.Append(textBox.GetLuaString());
            }

            builder.Append(@"--End of textboxes
    }
}");

            builder.Append(@"--Save function
function updateSave()
    saved_data = JSON.encode(ref_buttonData)
    if disableSave==true then saved_data="""" end
    self.script_state = saved_data
end

--Startup procedure
function onload(saved_data)
    if disableSave==true then saved_data="""" end
    if saved_data ~= """" then
        local loaded_data = JSON.decode(saved_data)
        ref_buttonData = loaded_data
    else
        ref_buttonData = defaultButtonData
    end

    spawnedButtonCount = 0
    createCheckbox()
    createCounter()
    createTextbox()
end



--Click functions for buttons



--Checks or unchecks the given box
function click_checkbox(tableIndex, buttonIndex)
    if ref_buttonData.checkbox[tableIndex].state == true then
        ref_buttonData.checkbox[tableIndex].state = false
        self.editButton({index=buttonIndex, label=""""})
    else
        ref_buttonData.checkbox[tableIndex].state = true
        self.editButton({index=buttonIndex, label=string.char(10010)})
    end
    updateSave()
end

--Applies value to given counter display
function click_counter(tableIndex, buttonIndex, amount)
    ref_buttonData.counter[tableIndex].value = ref_buttonData.counter[tableIndex].value + amount
    self.editButton({index=buttonIndex, label=ref_buttonData.counter[tableIndex].value})
    updateSave()
end

--Updates saved value for given text box
function click_textbox(i, value, selected)
    if selected == false then
        ref_buttonData.textbox[i].value = value
        updateSave()
    end
end

--Dud function for if you have a background on a counter
function click_none() end



--Button creation



--Makes checkboxes
function createCheckbox()
    for i, data in ipairs(ref_buttonData.checkbox) do
        --Sets up reference function
        local buttonNumber = spawnedButtonCount
        local funcName = ""checkbox""..i
        local func = function() click_checkbox(i, buttonNumber) end
        self.setVar(funcName, func)
        --Sets up label
        local label = """"
        if data.state==true then label=string.char(10010) end
        --Creates button and counts it
        self.createButton({
            label=label, click_function=funcName, function_owner=self,
            position=data.pos, height=data.size, width=data.size,
            font_size=data.size, scale=buttonScale,
            color=buttonColor, font_color=buttonFontColor
        })
        spawnedButtonCount = spawnedButtonCount + 1
    end
end

--Makes counters
function createCounter()
    for i, data in ipairs(ref_buttonData.counter) do
        --Sets up display
        local displayNumber = spawnedButtonCount
        --Sets up label
        local label = data.value
        --Sets height/width for display
        local size = data.size
        if data.hideBG == true then size = 0 end
        --Creates button and counts it
        self.createButton({
            label=label, click_function=""click_none"", function_owner=self,
            position=data.pos, height=size, width=size,
            font_size=data.size, scale=buttonScale,
            color=buttonColor, font_color=buttonFontColor
        })
        spawnedButtonCount = spawnedButtonCount + 1

        --Sets up add 1
        local funcName = ""counterAdd""..i
        local func = function() click_counter(i, displayNumber, 1) end
        self.setVar(funcName, func)
        --Sets up label
        local label = ""+""
        --Sets up position
        local offsetDistance = (data.size/2 + data.size/4) * (buttonScale[1] * 0.002)
        local pos = {data.pos[1] + offsetDistance, data.pos[2], data.pos[3]}
        --Sets up size
        local size = data.size / 2
        --Creates button and counts it
        self.createButton({
            label=label, click_function=funcName, function_owner=self,
            position=pos, height=size, width=size,
            font_size=size, scale=buttonScale,
            color=buttonColor, font_color=buttonFontColor
        })
        spawnedButtonCount = spawnedButtonCount + 1

        --Sets up subtract 1
        local funcName = ""counterSub""..i
        local func = function() click_counter(i, displayNumber, -1) end
        self.setVar(funcName, func)
        --Sets up label
        local label = ""-""
        --Set up position
        local pos = {data.pos[1] - offsetDistance, data.pos[2], data.pos[3]}
        --Creates button and counts it
        self.createButton({
            label=label, click_function=funcName, function_owner=self,
            position=pos, height=size, width=size,
            font_size=size, scale=buttonScale,
            color=buttonColor, font_color=buttonFontColor
        })
        spawnedButtonCount = spawnedButtonCount + 1
    end
end

function createTextbox()
    for i, data in ipairs(ref_buttonData.textbox) do
        --Sets up reference function
        local funcName = ""textbox""..i
        local func = function(_,_,val,sel) click_textbox(i,val,sel) end
        self.setVar(funcName, func)

        self.createInput({
            input_function = funcName,
            function_owner = self,
            label          = data.label,
            alignment      = data.alignment,
            position       = data.pos,
            scale          = buttonScale,
            width          = data.width,
            height         = (data.font_size*data.rows)+24,
            font_size      = data.font_size,
            color          = buttonColor,
            font_color     = buttonFontColor,
            value          = data.value,
        })
    end
end");
            _topLevelService.SetClipboardText(builder.ToString());
        }
    }
}
