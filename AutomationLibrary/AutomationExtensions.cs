using System;
using System.Drawing;
using System.Windows.Automation;
using System.Windows.Forms;
using static System.Windows.Automation.AutomationElement;

namespace AutomationLibrary
{
    public static class AutomationExtensions
    {
        public static AutomationElement GetById(this AutomationElement el, string id)
        {
            var condition = new PropertyCondition(AutomationIdProperty, id, PropertyConditionFlags.IgnoreCase);

            return el.FindFirst(TreeScope.Descendants | TreeScope.Children, condition);
        }

        public static AutomationElementCollection GetByType(this AutomationElement el, object controlType)
        {
            if (!(controlType is ControlType)) return null;

            var condition = new PropertyCondition(ControlTypeProperty, controlType);

            return el.FindAll(TreeScope.Descendants | TreeScope.Children, condition);
        }        

        public static AutomationElement GetByType(this AutomationElement el, object controlType, int index)
        {            
            if (!(controlType is ControlType)) return null;

            var items = el.GetByType(controlType);

            if (items == null) return null;

            if (index > items.Count - 1) return null;

            return items[index];
        }

        public static AutomationElement GetByClass(this AutomationElement el, string className, int index)
        {
            var items = el.GetByClass(className);

            if (index > items.Count - 1) return null;

            return items[index];
        }

        public static AutomationElementCollection GetByClass(this AutomationElement el, string className)
        {
            var condition = new PropertyCondition(ClassNameProperty, className);

            return el.FindAll(TreeScope.Descendants | TreeScope.Children, condition);
        }

        public static void ChangeText(this AutomationElement el, string text)
        {
            if (el.TryGetCurrentPattern(ValuePattern.Pattern, out var p))
                (p as ValuePattern).SetValue(text);
        }

        public static void Click(this AutomationElement el) =>
            (el.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();

        public static void Toggle(this AutomationElement el) =>
            (el.GetCurrentPattern(TogglePattern.Pattern) as TogglePattern).Toggle();

        public static void Select(this AutomationElement el) =>
            (el.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern).Select();

        public static void Expand(this AutomationElement el) =>
            (el.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern).Expand();

        public static void Collapse(this AutomationElement el) =>
            (el.GetCurrentPattern(ExpandCollapsePattern.Pattern) as ExpandCollapsePattern).Collapse();

        public static bool MouseClickFromThis(this AutomationElement el, int X, int Y)
        {
            try
            {
                var current = el.Current;
                Cursor.Position = new Point(
                    Convert.ToInt32(current.BoundingRectangle.Right + X), 
                    Convert.ToInt32(current.BoundingRectangle.Y + Y));
                Win32.MouseEvent(6u, Convert.ToUInt32(Cursor.Position.X), Convert.ToUInt32(Cursor.Position.Y), 0u, 0u);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
