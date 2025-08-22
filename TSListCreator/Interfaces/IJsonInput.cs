using System.Text.Json.Nodes;

namespace TSListCreator.Interfaces;

public interface IJsonInput
{
    JsonObject GetJsonObject();
}
