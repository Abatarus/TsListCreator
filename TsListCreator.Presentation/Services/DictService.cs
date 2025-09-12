using System.Globalization;
using TsListCreator.Presentation.Converters;
using TsListCreator.Shared.Enums;

namespace TsListCreator.Presentation.Services;

public class DictService
{
    private List<string> _alignmentList;


    public DictService()
    {
        AlignmentConverter alignmentConverter = new AlignmentConverter();
        _alignmentList = Enum.GetValues<AlignmentId>()
            .Select(alignment => alignmentConverter.Convert(alignment, null, null, CultureInfo.CurrentCulture))
            .Cast<string>().ToList();
    }

    private static DictService _instance = new DictService();
    public static DictService Instance => _instance;

    public static List<string> AlignmentList => Instance._alignmentList;
}