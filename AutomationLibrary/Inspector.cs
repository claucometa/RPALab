using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;

namespace AutomationLibrary
{
    public class Inspector
    {
        public List<string> InspectElement(AutomationElement el, string separator = "\t")
        {
            var list = new List<string>();

            var items = new List<string> { "Index", "AutomationId", "Name", "ControlType", "SupportedPatterns", "ClassName" };
            list.Add(string.Join(separator, items));

            var col = el.FindAll(TreeScope.Descendants, Condition.TrueCondition);

            for (int i = 0; i < col.Count; i++)
            {
                AutomationElement item = col[i];

                try
                {
                    items.Clear();
                    items.Add(i.ToString());
                    items.Add(item.Current.AutomationId);
                    items.Add(item.Current.Name);
                    items.Add(item.Current.ControlType.ProgrammaticName);
                    items.Add(string.Join(" | ", item.GetSupportedPatterns().Select(p => p.ProgrammaticName).ToList()));
                    items.Add(item.Current.ClassName);
                    list.Add(string.Join(separator, items));
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return list;
        }
    }
}
