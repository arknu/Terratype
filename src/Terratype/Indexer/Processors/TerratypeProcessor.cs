﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terratype.Indexer.ProcessorService;

namespace Terratype.Indexer.Processors
{
	public class TerratypeProcessor : PropertyBase
	{
		public TerratypeProcessor(IList<Entry> results, Stack<Task> tasks) : base(results, tasks)
		{
		}

		public override bool Process(Task task)
		{
			if (string.Compare(task.PropertyEditorAlias, Terratype.TerratypePropertyEditor.PropertyEditorAlias, true) != 0 || 
				task.Json.Type != JTokenType.Object || ((int?) task.DataTypeId) == null)
			{
				return false;
			}

			var obj = task.Json as JObject;
			obj.Add(new JProperty("datatypeId", (int) task.DataTypeId));
			this.Results.Add(new Entry(task.Keys, task.Ancestors, new Models.Model(obj)));
			return true;
		}
	}
}
