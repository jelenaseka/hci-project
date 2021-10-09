using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventPlanner.Pages
{
    /// <summary>
    /// Interaction logic for EventBoardPage.xaml
    /// </summary>
    public partial class EventBoardPage : Page
    {
        Point startPoint = new Point();
        string referenceListName;
        bool isDraggingElement = false;
        public EventBoardPage()
        {
            InitializeComponent();
        }
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            referenceListName = ((ListView)sender).Name;

            if (e.LeftButton == MouseButtonState.Pressed && isDraggingElement &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                Console.WriteLine(e.OriginalSource.ToString());
                // Find the data behind the ListViewItem

                if(listViewItem != null)
                {
                    Task task = (Task)listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);
                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("taskFormat", task);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
                
            }
        }
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            if (!e.Data.GetDataPresent("taskFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            isDraggingElement = false;
            if (referenceListName == ((ListView)sender).Name) return;
            TaskLevel listFromLevel;
            TaskLevel listToLevel;
            if(referenceListName == "ToDoTaskListView")
            {
                listFromLevel = TaskLevel.TO_DO;
            } else if (referenceListName == "InProgressTaskListView")
            {
                listFromLevel = TaskLevel.IN_PROGRESS;
            } else
            {
                listFromLevel = TaskLevel.DONE;
            }
            if(((ListView)sender).Name == "ToDoTaskListView") {
                listToLevel = TaskLevel.TO_DO;
            } else if (((ListView)sender).Name == "InProgressTaskListView")
            {
                listToLevel = TaskLevel.IN_PROGRESS;
            } else
            {
                listToLevel = TaskLevel.DONE;
            }
            if (e.Data.GetDataPresent("taskFormat"))
            {
                Task task = e.Data.GetData("taskFormat") as Task;
                EventBoardViewModel viewModel = (EventBoardViewModel)this.DataContext;
                viewModel.MoveTaskCmd.Execute(new { Task = task, ListFromLevel = listFromLevel, ListToLevel = listToLevel });
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            isDraggingElement = true;
        }

        private void Page_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(UserService.Singleton().CurrentUser is EventPlanner.Models.Organizer)) return;
            isDraggingElement = false;
        }
    }
}
