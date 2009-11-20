using System;
using StructureMap;
using StructureMap.Graph;

namespace InRetail.UserInterface
{
    public class GenericConnectionScanner : ITypeScanner
    {
        // Fields
        private readonly Type _openType;

        // Methods
        public GenericConnectionScanner(Type openType)
        {
            _openType = openType;
            if (!_openType.IsGeneric())
            {
                throw new ApplicationException("This scanning convention can only be used with open generic types");
            }
        }

        public void Process(Type type, PluginGraph graph)
        {
            Type pluginType = type.FindInterfaceThatCloses(_openType);
            if (pluginType != null)
            {
                graph.AddType(pluginType, type);
            }
        }
    }
}