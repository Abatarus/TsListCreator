using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSListCreator.Utils;

namespace TSListCreator.Interfaces
{
    public interface ISaveLoadService
    {
        Task Save(IJsonInput settings, IEnumerable<IJsonInput> textBoxes, IEnumerable<IJsonInput> counters, IEnumerable<IJsonInput> checkBoxes);

        Task<DataHolder> Load();

        void SaveToClipBoard(ILuaInput settings,
            IEnumerable<ILuaInput> textBoxes,
            IEnumerable<ILuaInput> counters,
            IEnumerable<ILuaInput> checkBoxes);
    }
}
