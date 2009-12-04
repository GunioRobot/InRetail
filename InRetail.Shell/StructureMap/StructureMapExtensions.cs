using StructureMap;

namespace InRetail.Shell.StructureMap
{
    public static class StructureMapExtensions
    {
        public static bool Implements<T>(this PluginTypeConfiguration configuration)
        {
            return typeof(T).IsAssignableFrom(configuration.PluginType);
        }
        public static T To<T>(this PluginTypeConfiguration configuration, IContainer container)
        {
            return (T)container.GetInstance(configuration.PluginType);
        }
    }
}