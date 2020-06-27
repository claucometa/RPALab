using System;
using System.Windows.Automation;
using static System.Windows.Automation.AutomationElement;

namespace AutomationLibrary
{
    public class AutomationHelper
    {
        public ControlType[] ListTypes()
        {
            return new ControlType[] { ControlType.Window, ControlType.Button };
        }

        public AutomationElement GetRoot(string appRootName)
        {
            var condition = new PropertyCondition(NameProperty, appRootName);

            return RootElement.FindFirst(TreeScope.Children, condition);
        }

        public AutomationElement FindWindow(string className, string windowName)
        {
            var hwnd = Win32.FindWindow(className, windowName);

            if (hwnd.ToInt64() == 0) return null;

            return FromHandle(hwnd);
        }

        public AutomationElement FindWindowByCaption(string windowName)
        {
            var hwnd = Win32.FindWindowByCaption(IntPtr.Zero, windowName);

            if (hwnd.ToInt64() == 0) return null;

            return FromHandle(hwnd);
        }

        //public static void ExecAction(this AutomationElement el, string pattern, string value = "")
        //{
        //    switch (pattern)
        //    {
        //        case "InvokePatternIdentifiers":
        //            el.Click();
        //            break;
        //        case "ValuePatternIdentifiers":
        //            el.ChangeText(value);
        //            break;
        //        case "SelectionPatternIdentifiers":
        //            el.Select();
        //            break;
        //        case "ExpandPatternIdentifiers":
        //            el.Expand();
        //            break;
        //        case "TogglePatternIdentifiers":
        //            el.Toggle();
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
