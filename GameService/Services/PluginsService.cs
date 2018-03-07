using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class PluginsService
    {
        private static IEnumerable<Type> _listPlugins;
        public static IEnumerable<Type> ListPlugins
        {
            get
            {
                if (_listPlugins == null)
                {
                    _listPlugins = FindAllPlugins();
                }return _listPlugins;
            }
            set
            {
                _listPlugins = value;
            }
        }
        public static Type SelectedType { get; set; } = null;
        public static IEnumerable<Type> FindAllPlugins()
        {
            var type = typeof(IPursueAlgo);
            ListPlugins = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) & p.IsClass);
            return ListPlugins;
        }
        /// <summary>
         /// Select first finded suitable plugin
         /// </summary>
         /// <returns></returns>
        public static Type GetRandomSelectedType()
        {
            if (ListPlugins == null)
            {
                FindAllPlugins();
            }
            return ListPlugins?.First();
        }
       
    }
}
