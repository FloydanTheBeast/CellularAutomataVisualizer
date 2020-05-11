﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using CellularAutomata;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Visualizer
{
    /// <summary>
    /// Логика взаимодействия для RuleSetConstructor.xaml
    /// </summary>
    public partial class RuleSetConstructor : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<Rule> RuleSet { get; set; }

        public RuleSetConstructor()
        {
            RuleSet = new ObservableCollection<Rule>();
            DataContext = this;
            InitializeComponent();
            RuleSet.CollectionChanged += RuleSetCollectionChanged;
        }

 
        public void UpdateViews()
        {
            OnPropertyChanged();

            foreach (var item in RuleListView.Items)
            {
                if (item is ExactRuleConstructor)
                    ((ExactRuleConstructor)item).CreateView();
                
                else if (item is NearbyNeighborsRuleConstructor)
                    ((NearbyNeighborsRuleConstructor)item).UpdateView();
            }
        }

        private void CreateRuleBtnClick(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)RuleTypePicker.SelectedItem;

            try
            {
                switch ((string)selectedItem.Tag)
                {
                    case "ExactPattern":
                        RuleSet.Add(new ExactPatternRule(new Cell(), 9));
                        break;
                    case "NearbyNeighbors":
                        RuleSet.Add(new NearbyNeighborsRule(
                            new Cell(true),
                            "isAlive",
                            true,
                            0,
                            9
                        ));
                        break;
                    default:
                        MessageBox.Show("There is no such rule", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please, select a rule type", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RuleSetCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems[0] is ExactPatternRule)
                    {
                        ExactRuleConstructor ruleConstructor = new ExactRuleConstructor(e.NewStartingIndex);
                        RuleListView.Items.Add(ruleConstructor);
                    }
                    else if (e.NewItems[0] is NearbyNeighborsRule)
                    {
                        NearbyNeighborsRuleConstructor ruleConstructor = new NearbyNeighborsRuleConstructor(e.NewStartingIndex);
                        RuleListView.Items.Add(ruleConstructor);
                    }
                    break;
/*                case NotifyCollectionChangedAction.Remove: // если удаление
                    User oldUser = e.OldItems[0] as User;
                    Console.WriteLine($"Удален объект: {oldUser.Name}");
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                    User replacedUser = e.OldItems[0] as User;
                    User replacingUser = e.NewItems[0] as User;
                    Console.WriteLine($"Объект {replacedUser.Name} заменен объектом {replacingUser.Name}");
                    break;*/
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
