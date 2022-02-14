using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;
using CheckBox_Searcher.Helpers;

namespace CheckBox_Searcher
{  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Node> Nodes { get; private set; }
        public MainWindow()
        {
            Nodes = new ObservableCollection<Node>();
            InitializeComponent();
            FillingTree();
        }
      
        #region UI methods
        /// <summary>
        /// Take Id from CheckBox Uid and transfer value to CheckBoxId struct
        /// </summary>
        /// <param name="sender">The CheckBox clicked.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckBox currentCheckBox = (CheckBox)sender;
            CheckBoxId.checkBoxId = currentCheckBox.Uid;
        }

        /// <summary>
        /// Take Id from CheckBox Uid and transfer value to CheckBoxId struct
        /// </summary>
        /// <param name="sender">The CheckBox check with space button.</param>
        /// <param name="e">Parameters associated to the mouse event.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                CheckBox currentCheckBox = (CheckBox)sender;
                CheckBoxId.checkBoxId = currentCheckBox.Uid;
            }
        }
        private void restartBtn_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (HasSelectedNodes())
            {
                MessageWindow myWin = new MessageWindow();
                myWin.ShowDialog();
                if (myWin.DialogResult == true)
                {
                    DeleteSelectedNodes();
                }
            }
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!HasSelectedNodes())
            {
                AddAndEditWindow myWin = new AddAndEditWindow();
                myWin.ShowDialog();
                Restart();
            }
        }
         private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Node firstNode in Nodes.First().Children)
            {
                if (firstNode.IsChecked == true)
                {
                    AddAndEditWindow myWin = new AddAndEditWindow(firstNode.Text);
                    myWin.ShowDialog();
                    break;
                }
                foreach (Node n in firstNode.Children)
                {
                    if (n.IsChecked == true)
                    {
                        int charLocation = n.Text.IndexOf(":", StringComparison.Ordinal);

                        if (charLocation > 0)
                        {
                            AddAndEditWindow myWin = new AddAndEditWindow(firstNode.Text, n.Text.Substring(0, charLocation));
                            myWin.ShowDialog();
                        }    
                    }
                }
            }
            Restart();
        }
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (SearchBox.Text == "")
                Restart();
            else
                Search();
        }
        #endregion

        #region Privte methods
        /// <summary>
        /// Filling tree different values
        /// </summary>
        private void FillingTree()
        {
            Nodes.Clear();
            XElement xelement = XElement.Load(SettingsHelper.xmlConncation);
            var level_1_items = new Node() { Text = " users ", IsChecked = false, IsVisible=Visibility.Visible };
            IEnumerable<XElement> users = xelement.Elements();
            foreach (XElement user in users)
            {
                string myId = user.Attribute("id").Value;
                var level_2_items = new Node() { Text = "Id " + myId, IsChecked = false, IsVisible = Visibility.Visible };
                level_2_items.Parent.Add(level_1_items);
                level_1_items.Children.Add(level_2_items);
                IEnumerable<XElement> userInfo = user.Elements();
                foreach (XElement info in userInfo)
                {
                    var level_3_items = new Node() { Text = info.Name + ": " + info.Value, IsChecked = false, IsVisible = Visibility.Visible };
                    level_3_items.Parent.Add(level_2_items);
                    level_2_items.Children.Add(level_3_items);
                }
            }
            Nodes.Add(level_1_items);
            treeView.ItemsSource = Nodes;
            ExpandTree(Nodes,true);
        }
        private bool HasSelectedNodes()
        {
            foreach (Node firstNode in Nodes.First().Children)
            {
                if (firstNode.IsChecked == true)
                {
                    return true;
                }
                foreach (Node n in firstNode.Children)
                {
                    if (n.IsChecked == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void Restart()
        {
            FillingTree();
        }
        private void DeleteSelectedNodes()
        {
            foreach (Node firstNode in Nodes.First().Children)
            {
                if (firstNode.IsChecked == true)
                {
                    XmlHelper.DeleteUser(firstNode.Text);
                    break;
                }
                foreach (Node n in firstNode.Children)
                {
                    if (n.IsChecked == true)
                    {
                        XmlHelper.DeleteItem(firstNode.Text, n.Text);
                    }
                }
            }
            Restart();
        }
       public void Search()
       {
            foreach (Node firstNode in Nodes.First().Children)
            {
                SearchResult(firstNode);
            }
            SearchResultTree();
        }

        public Visibility SearchResult(Node myNode)
        {
            Visibility visibility = Visibility.Hidden;
            bool HasVisibleChild = false;
            if (myNode.Children == null)
            {
                if (myNode.Text.Contains(SearchBox.Text))
                {
                    myNode.IsVisible = Visibility.Visible;
                    return Visibility.Visible;
                }
                else
                {
                    myNode.IsVisible = Visibility.Hidden;
                    return Visibility.Hidden;
                }
            }
            else
            {
                foreach (Node node in myNode.Children)
                {                   
                    visibility = SearchResult(node);
                    if (visibility == Visibility.Visible)
                    {
                        HasVisibleChild = true;
                    }
                }
            }
            if (HasVisibleChild)
            {
                myNode.IsVisible = Visibility.Visible;
                return Visibility.Visible;
            }
            else
            {
                if (myNode.Text.Contains(SearchBox.Text))
                {
                    myNode.IsVisible = Visibility.Visible;
                    return Visibility.Visible;
                }
                else
                {
                    myNode.IsVisible = Visibility.Hidden;
                    return Visibility.Hidden;
                }
            }

        }

        public void SearchResultTree()
        {
            ObservableCollection<Node> SearchNode = new ObservableCollection<Node>();
            var level_1_items = new Node() { Text = " users ", IsChecked = false, IsVisible = Visibility.Visible };
            foreach (Node firstNode in Nodes.First().Children)
            {
                if (firstNode.IsVisible == Visibility.Visible)
                {
                    var level_2_items = new Node() { Text = firstNode.Text, IsChecked = firstNode.IsChecked, IsVisible = Visibility.Visible };
                    level_2_items.Parent.Add(level_1_items);
                    level_1_items.Children.Add(level_2_items);
                    foreach (Node n in firstNode.Children)
                    {
                        if (n.IsVisible == Visibility.Visible)
                        {
                            var level_3_items = new Node() { Text = n.Text, IsChecked = n.IsChecked, IsVisible = Visibility.Visible };
                            level_3_items.Parent.Add(level_2_items);
                            level_2_items.Children.Add(level_3_items);
                        }
                    }
                }    
            }
            SearchNode.Add(level_1_items);
            Nodes = SearchNode;
            treeView.ItemsSource = Nodes;
        }
        #endregion

        #region Check, Uncheck and Invert all checkboxes

        private void buttonCheckAll_Click(object sender, RoutedEventArgs e)
        {
            CheckedTree(Nodes, true);
        }

        private void buttonUncheckAll_Click(object sender, RoutedEventArgs e)
        {
            CheckedTree(Nodes, false);
        }

        private void CheckedTree(ObservableCollection<Node> items, bool isChecked)
        {
            foreach (Node item in items)
            {
                item.IsChecked = isChecked;
                if (item.Children.Count != 0) CheckedTree(item.Children, isChecked);
            }
        }

        private void buttonInvert_Click(object sender, RoutedEventArgs e)
        {
            CheckBoxId.checkBoxId = "";
            InvertTree(Nodes);
        }

        private void InvertTree(ObservableCollection<Node> items)
        {
            foreach (Node item in items)
            {
                if (item.IsChecked != null)
                {
                    if (item.IsChecked == true) item.IsChecked = false;
                    else item.IsChecked = true;
                    if (item.Parent.Count != 0 && item.Parent[0].IsChecked == true) item.IsChecked = true;
                    if (item.Parent.Count != 0 && item.Parent[0].IsChecked == false) item.IsChecked = false;
                }
                if (item.Children.Count != 0) InvertTree(item.Children);
            }
        }

        #endregion

        #region Expand and Collapse items

        private void buttonExpand_Click(object sender, RoutedEventArgs e)
        {
            ExpandTree(Nodes, true);
        }

        private void buttonCollapse_Click(object sender, RoutedEventArgs e)
        {
            ExpandTree(Nodes, false);
        }

        private void ExpandTree(ObservableCollection<Node> items, bool isExpanded)
        {
            foreach (Node item in items)
            {
                item.IsExpanded = isExpanded;
                if (item.Children.Count != 0) ExpandTree(item.Children, isExpanded);
            }
        }


        #endregion
    }
}
