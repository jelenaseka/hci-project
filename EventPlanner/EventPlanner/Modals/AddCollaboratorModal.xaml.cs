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
    /// Interaction logic for AddCollaboratorModal.xaml
    /// </summary>
    public partial class AddCollaboratorModal : Window
    {
        public AddCollaboratorModal()
        {
            InitializeComponent();
            collaboratorsTypeComboBox.ItemsSource = new List<String> { "Restaurant", "Drink store", "Balloons" };
        }
    }
}
