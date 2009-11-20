﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InRetail.CommandHandlers;
using InRetail.Commands;

namespace InRetail.Configuration
{
    public class CommandHandlerHelper
    {
        public static IDictionary<Type, IList<Type>> GetCommandHandlers()
        {
            IDictionary<Type, IList<Type>> commands = new Dictionary<Type, IList<Type>>();
            typeof(ICommandHandler<>)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))
                .ToList()
                .ForEach(x => AddItem(commands, x));
            return commands;
        }

        public static IEnumerable<Type> GetCommands()
        {
            return typeof(Command)
                .Assembly
                .GetExportedTypes()
                .Where(x => x.BaseType == typeof(Command))
                .ToList();
        }

        private static void AddItem(IDictionary<Type, IList<Type>> dictionary, Type type)
        {
            var command = type.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                .First()
                .GetGenericArguments()
                .First();

            if (!dictionary.ContainsKey(command))
                dictionary.Add(command, new List<Type>());

            dictionary[command].Add(type);
        }
    }
}