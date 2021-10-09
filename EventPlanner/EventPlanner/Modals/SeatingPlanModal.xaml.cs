using EventPlanner.Services;
using EventPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventPlanner.Modals
{
    /// <summary>
    /// Interaction logic for SeatingPlanModal.xaml
    /// </summary>
    public partial class SeatingPlanModal : Window
    {
        Point startPoint = new Point();
        object referenceDataContext;
        bool isDraggingElement1 = false;
        public SeatingPlanModal()
        {
            InitializeComponent();
        }
        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if(((ListView)e.Source).DataContext is SeatingPlanViewModel)
            {
                referenceDataContext = (SeatingPlanViewModel)((ListView)e.Source).DataContext;
            } else
            {
                referenceDataContext = (TableViewModel)((ListView)e.Source).DataContext;
            }

            if (e.LeftButton == MouseButtonState.Pressed && isDraggingElement1 &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = 
                    FindAncestor<ListView>((DependencyObject)e.OriginalSource);
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                // Find the data behind the ListViewItem

                if (listViewItem is ListViewItem)
                {
                    string invitation = listViewItem.Content as string;
                    if (invitation == null)
                    {
                        return;
                    }
                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("invitationFormat", invitation);
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
            if (!e.Data.GetDataPresent("invitationFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            isDraggingElement1 = false;
            if (e.Data.GetDataPresent("invitationFormat"))
            {
                string invitation = e.Data.GetData("invitationFormat") as string;
                
                if(referenceDataContext is SeatingPlanViewModel)
                {
                    ((SeatingPlanViewModel)referenceDataContext).Invitations.Remove(invitation);
                    TableViewModel viewModel = (TableViewModel)((ListView)e.Source).DataContext;
                    viewModel.Invites.Add(invitation);
                } else
                {
                    ((TableViewModel)referenceDataContext).Invites.Remove(invitation);
                    if(((ListView)e.Source).DataContext is SeatingPlanViewModel)
                    {
                        SeatingPlanViewModel viewModel = (SeatingPlanViewModel)((ListView)e.Source).DataContext;
                        viewModel.Invitations.Add(invitation);
                    } else
                    {
                        TableViewModel viewModel = (TableViewModel)((ListView)e.Source).DataContext;
                        viewModel.Invites.Add(invitation);
                    }
                    
                }
                
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDraggingElement1 = false;
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            isDraggingElement1 = true;
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            mainScrollViewer.ScrollToVerticalOffset(mainScrollViewer.VerticalOffset - e.Delta);
        }

        private void addTableTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SeatingPlanViewModel viewModel = (SeatingPlanViewModel)((TextBox)e.Source).DataContext;
                if (addTableTextBox.Text == "") return;
                viewModel.AddTable();
            }
        }

        private void addInvitationTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SeatingPlanViewModel viewModel = (SeatingPlanViewModel)((TextBox)e.Source).DataContext;
                if (addInvitationTextBox.Text == "") return;
                viewModel.AddInvitation();
            }
        }
    }
}
