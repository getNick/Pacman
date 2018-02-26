using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class PluginsService
    {
        public IEnumerable<Type> ListPlugins;
        public static Type SelectedType { get; set; }
        public PluginsService()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var type = typeof(IPursueAlgo);
            ListPlugins = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) & p.IsClass);
        }
       
    }
}
