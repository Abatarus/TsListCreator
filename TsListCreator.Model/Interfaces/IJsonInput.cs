using System.Text.Json.Nodes;

namespace TsListCreator.Model.Interfaces;

public interface IJsonInput
{
    JsonObject GetJsonObject();
}
