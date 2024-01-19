using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using Models;

namespace RestApi;

/// <summary>
/// The JSON serializer does not know how to serialize and deserialize interfaces by default because it does not
/// know what implementing class to instantiate. This class builds a static mapping of interfaces to 
/// the implementing class that the serializer should use, along with a method that allows the JSON serializer to
/// access these mappings.
/// </summary>
public static class JsonInterfaceHelper
{
    #region Fields

    /// <summary>
    /// Dictionary mapping the model interface types to their implementing record classes
    /// </summary>
    private static readonly Dictionary<Type, Type> interfaceToClassMapping = InterfaceToClassMappingBuilder();

    #endregion

    #region Methods

    /// <summary>
    /// Builds the mapping from interface types to the implementing types that the JSON serializer should 
    /// instantiate. Every interface in the Models assembly has a simple record class implementation that 
    /// provides getters and setters to every property on the model. Use these record classes for the JSON
    /// serializer.
    /// </summary>
    /// <returns>A dictionary mapping the interface type to its implementing record type</returns>
    public static Dictionary<Type, Type> InterfaceToClassMappingBuilder()
    {
        var interfaceToClassMapping = new Dictionary<Type, Type>();
        Assembly modelsAssembly = Assembly.GetAssembly(typeof(IAccount))
            ?? throw new InvalidOperationException("Unable to retrieve Models assembly");
        foreach (Type interfaceType in modelsAssembly.GetTypes().Where(type => type.IsInterface))
        {
            interfaceToClassMapping[interfaceType] = modelsAssembly.GetTypes()
                .Where(type => !type.IsInterface && type.IsAssignableTo(interfaceType))
                .Single();
        }
        return interfaceToClassMapping;
    }

    /// <summary>
    /// If the provided JsonTypeInfo points to an interface type, creates an instance of the
    /// implementing class the JSON serializer should use to serialize that interface.
    /// </summary>
    /// <param name="jsonTypeInfo">JsonTypeInfo the serializer is attempting to serialize</param>
    public static void ResolveTypeInfo(JsonTypeInfo jsonTypeInfo)
    {
        if (interfaceToClassMapping.TryGetValue(jsonTypeInfo.Type, out Type? recordType))
        {
            jsonTypeInfo.CreateObject = () => Activator.CreateInstance(recordType, nonPublic: true)!;
        }
    }

    #endregion
}