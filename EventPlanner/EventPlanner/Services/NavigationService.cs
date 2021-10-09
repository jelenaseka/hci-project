using EventPlanner.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace EventPlanner.Services
{
    class NavigationService
    {
        private static NavigationService singleton = null;
        public static NavigationService Singleton()
        {
            return singleton ??= new NavigationService();
        }

        public event EventHandler<string> PageChanged;
        public event EventHandler<dynamic> PageChangedWithModel;

        public void ChangePage(string page)
        {
            PageChanged?.Invoke(this, page);
        }

        internal void ChangePage(string page, object model)
        {
            PageChangedWithModel?.Invoke(this, new { page = page, model = model });
        }
    }
}
