using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace HPSolutionCCDevPackage.netFramework
{
    [TemplatePart(Name = MontuBox.RootBorderName, Type = typeof(Border))]
    [TemplatePart(Name = MontuBox.MontuItemControlName, Type = typeof(ItemsControl))]
    [TemplatePart(Name = MontuBox.MontuPopupName, Type = typeof(Popup))]
    [TemplatePart(Name = MontuBox.MontuPopupBorderName, Type = typeof(Border))]
    [TemplatePart(Name = MontuBox.MontuContentPresenterName, Type = typeof(ContentPresenter))]
    [StyleTypedProperty(Property = "MontuBoxItemStyle", StyleTargetType = typeof(MontuBoxItem))]
    [StyleTypedProperty(Property = "CheckboxStyle", StyleTargetType = typeof(CheckBox))]
    [StyleTypedProperty(Property = "MontuPopupBorderStyleProperty", StyleTargetType = typeof(Border))]
    [ContentProperty("MontuItems")]
    public class MontuBox : Control
    {

        private const string RootBorderName = "RootBorder";
        private const string MontuItemControlName = "MontuItemControl";
        private const string MontuPopupName = "MontuPopup";
        private const string MontuPopupBorderName = "MontuPopupBorder";
        private const string MontuContentPresenterName = "MontuContentPresenter";

        #region IsDropDownOpen
        public static readonly DependencyProperty IsDropDownOpenProperty =
                DependencyProperty.Register(
                        "IsDropDownOpen",
                        typeof(bool),
                        typeof(MontuBox),
                        new FrameworkPropertyMetadata(
                                false,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                new PropertyChangedCallback(IsDropDownOpenChangedCallback)),
                        null);

        private static void IsDropDownOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        #endregion

        #region ItemsSource
        public static readonly DependencyProperty SelectedItemsProperty
            = DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(MontuBox),
                                          new FrameworkPropertyMetadata((IEnumerable)null,
                                                                        new PropertyChangedCallback(SelectedItemsChangedCallback)));

        private static void SelectedItemsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBox ic = (MontuBox)d;
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;

            ic.OnSelectedItemsChanged(oldValue, newValue);
        }

        protected virtual void OnSelectedItemsChanged(IEnumerable oldValue, IEnumerable newValue)
        {

        }


        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set
            {
                if (value == null)
                {
                    ClearValue(SelectedItemsProperty);
                }
                else
                {
                    SetValue(SelectedItemsProperty, value);
                }
            }
        }
        #endregion

        #region ItemsSource
        public static readonly DependencyProperty ItemsSourceProperty
            = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MontuBox),
                                          new FrameworkPropertyMetadata((IEnumerable)null,
                                                                        new PropertyChangedCallback(OnItemsSourceChanged)));

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBox ic = (MontuBox)d;
            IEnumerable oldValue = (IEnumerable)e.OldValue;
            IEnumerable newValue = (IEnumerable)e.NewValue;

            ic.OnItemsSourceChanged(oldValue, newValue);
        }

        protected virtual void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            MontuItems.Clear();
            ClearSelectedItem();

            if(MontuContentPresenter != null)
            {
                MontuContentPresenter.Content = null;
            }

            if (newValue != null)
            {
                foreach (var item in newValue)
                {
                    var mI = new MontuBoxItem()
                    {
                        Content = item,
                        Style = MontuBoxItemStyle,
                        CheckBoxStyle = CheckboxStyle,
                        DataContext = item
                    };

                    mI.IsSelectedChanged -= OnMontuItemIsSelectedChanged;

                    MontuItems.Add(mI);

                    mI.IsSelectedChanged += OnMontuItemIsSelectedChanged;
                }

                var x = newValue as INotifyCollectionChanged;
                if (x != null)
                {
                    x.CollectionChanged -= OnItemCollectionChanged;
                    x.CollectionChanged += OnItemCollectionChanged;
                }
            }

            if (oldValue != null)
            {
                var x = oldValue as INotifyCollectionChanged;
                if (x != null)
                {
                    x.CollectionChanged -= OnItemCollectionChanged;
                }
            }

        }


        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                if (value == null)
                {
                    ClearValue(ItemsSourceProperty);
                }
                else
                {
                    SetValue(ItemsSourceProperty, value);
                }
            }
        }
        #endregion

        #region MontuBoxItemStyle
        public Style MontuBoxItemStyle
        {
            get { return GetValue(MontuBoxItemStyleProperty) as Style; }
            set { SetValue(MontuBoxItemStyleProperty, value); }
        }

        public static readonly DependencyProperty MontuBoxItemStyleProperty =
            DependencyProperty.Register(
                "MontuBoxItemStyle",
                typeof(Style),
                typeof(MontuBox),
                new PropertyMetadata(null,
                    MontuBoxItemStylePropertyChangedCallback));

        private static void MontuBoxItemStylePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBox ic = (MontuBox)d;
            Style oldValue = (Style)e.OldValue;
            Style newValue = (Style)e.NewValue;

            ic.OnMontuBoxItemStyleChanged(oldValue, newValue);
        }

        #endregion MontuBoxItemStyle

        #region CheckboxStyle
        public Style CheckboxStyle
        {
            get { return GetValue(CheckboxStyleProperty) as Style; }
            set { SetValue(CheckboxStyleProperty, value); }
        }

        public static readonly DependencyProperty CheckboxStyleProperty =
            DependencyProperty.Register(
                "CheckboxStyle",
                typeof(Style),
                typeof(MontuBox),
                new PropertyMetadata(null,
                    MontuItemCheckboxStylePropertyChangedCallback));

        private static void MontuItemCheckboxStylePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBox ic = (MontuBox)d;
            Style oldValue = (Style)e.OldValue;
            Style newValue = (Style)e.NewValue;

            ic.OnMontuItemCheckboxStyleChanged(oldValue, newValue);
        }
        #endregion CheckboxStyle

        #region MontuPopupBorderStyle
        public Style MontuPopupBorderStyle
        {
            get { return GetValue(MontuPopupBorderStyleProperty) as Style; }
            set { SetValue(MontuPopupBorderStyleProperty, value); }
        }

        public static readonly DependencyProperty MontuPopupBorderStyleProperty =
            DependencyProperty.Register(
                "MontuPopupBorderStyle",
                typeof(Style),
                typeof(MontuBox),
                new PropertyMetadata(null,
                    MontuPopupBorderStylePropertyChangedCallback));

        private static void MontuPopupBorderStylePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBox ic = (MontuBox)d;
            Style newValue = (Style)e.NewValue;

            if (newValue != null && ic.MontuPopupBorder != null)
            {
                ic.MontuPopupBorder.Style = newValue;
            }
        }
        #endregion CheckboxStyle

        public ObservableCollection<MontuBoxItem> MontuItems { get; set; }
        public ObservableCollection<object> MontuSelectedItems { get; set; }

        private Border RootBorder { get; set; }
        private Border MontuPopupBorder { get; set; }
        private ContentPresenter MontuContentPresenter { get; set; }
        private ItemsControl MontuItemsControl { get; set; }
        private Popup MontuPopup { get; set; }

        public MontuBox()
        {
            DefaultStyleKey = typeof(MontuBox);
            IsTabStop = true;
            MontuItems = new ObservableCollection<MontuBoxItem>();

            MontuSelectedItems = new ObservableCollection<object>();
            MontuSelectedItems.CollectionChanged += OnMontuSelectedItemsCollectionChanged;

            SelectedItems = new ObservableCollection<object>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            MontuItemsControl = GetTemplateChild(MontuItemControlName) as ItemsControl;
            MontuPopupBorder = GetTemplateChild(MontuPopupBorderName) as Border;
            MontuContentPresenter = GetTemplateChild(MontuContentPresenterName) as ContentPresenter;

            // Assign ObservableCollection for source of ItemsControl
            if (MontuItemsControl != null)
            {
                MontuItemsControl.ItemsSource = MontuItems;
            }

            if (MontuPopupBorder != null)
            {
                MontuPopupBorder.Style = MontuPopupBorderStyle;
            }

        }


        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            IsDropDownOpen = false;
        }

        private void OnMontuSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (MontuContentPresenter != null)
            {
                var content = "";
                var count = MontuSelectedItems.Count();
                for (int i = 0; i < count; i++)
                {
                    if (i < count - 1)
                    {
                        content += MontuSelectedItems[i].ToString() + ", ";
                    }
                    else
                    {
                        content += MontuSelectedItems[i].ToString();
                    }
                }
                MontuContentPresenter.Content = content;
            }
        }

        private void OnItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    var mI = new MontuBoxItem()
                    {
                        Content = item,
                        Style = MontuBoxItemStyle,
                        CheckBoxStyle = CheckboxStyle,
                        DataContext = item
                    };

                    mI.IsSelectedChanged -= OnMontuItemIsSelectedChanged;

                    MontuItems.Add(mI);

                    mI.IsSelectedChanged += OnMontuItemIsSelectedChanged;

                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    var removeItem = MontuItems.Where(i => i.Content.Equals(item))
                    .FirstOrDefault();
                    if (removeItem != null)
                    {
                        removeItem.IsSelectedChanged -= OnMontuItemIsSelectedChanged;
                        MontuItems.Remove(removeItem);
                        RemoveSelectedItem(removeItem);
                    }
                }

            }
        }

        private void OnMontuItemIsSelectedChanged(object sender, IsSelectedChangedEventArgs e)
        {
            var ctrl = sender as MontuBoxItem;
            if (ctrl == null)
            {
                return;
            }


            if (e.NewValue && e.NewValue != e.OldValue)
            {
                AddSelectedItem(ctrl);
            }
            else if (!e.NewValue && e.NewValue != e.OldValue)
            {
                RemoveSelectedItem(ctrl);
            }
        }

        private void AddSelectedItem(MontuBoxItem ctrl)
        {
            MontuSelectedItems.Add(ctrl.DataContext);
            var tempCollection = SelectedItems as ObservableCollection<object>;
            if (tempCollection != null)
            {
                tempCollection.Add(ctrl.DataContext);
            }
        }

        private void RemoveSelectedItem(MontuBoxItem ctrl)
        {
            MontuSelectedItems.Remove(ctrl.DataContext);
            var tempCollection = SelectedItems as ObservableCollection<object>;
            if (tempCollection != null)
            {
                tempCollection.Remove(ctrl.DataContext);
            }
        }

        private void ClearSelectedItem()
        {
            MontuSelectedItems?.Clear();
            var tempCollection = SelectedItems as ObservableCollection<object>;
            if (tempCollection != null)
            {
                tempCollection.Clear();
            }
        }

        private void OnMontuBoxItemStyleChanged(Style oldValue, Style newValue)
        {
            if (newValue != null && MontuItems != null)
            {
                foreach (var item in MontuItems)
                {
                    item.Style = newValue;
                }
            }
        }

        private void OnMontuItemCheckboxStyleChanged(Style oldValue, Style newValue)
        {
            if (newValue != null && MontuItems != null)
            {
                foreach (var item in MontuItems)
                {
                    item.CheckBoxStyle = newValue;
                }
            }
        }
    }
}
