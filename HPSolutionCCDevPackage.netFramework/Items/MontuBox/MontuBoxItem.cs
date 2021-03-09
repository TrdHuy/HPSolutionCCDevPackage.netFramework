using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace HPSolutionCCDevPackage.netFramework
{
    [TemplatePart(Name = MontuBoxItem.RootBorderName, Type = typeof(Border))]
    [TemplatePart(Name = MontuBoxItem.MontuCheckBoxName, Type = typeof(CheckBox))]
    [TemplatePart(Name = MontuBoxItem.MontuContentPresenterName, Type = typeof(ContentPresenter))]
    [StyleTypedProperty(Property = "CheckBoxStyle", StyleTargetType = typeof(CheckBox))]
    [ContentProperty("Content")]
    public class MontuBoxItem : ContentControl
    {
        private const string RootBorderName = "RootBorder";
        private const string MontuCheckBoxName = "MontuICheckBox";
        private const string MontuContentPresenterName = "MontuIContentPrsenter";

        #region IsSelected
        public static readonly DependencyProperty IsSelectedProperty =
           DependencyProperty.Register(
                       "IsSelected",
                       typeof(bool),
                       typeof(MontuBoxItem),
                       new PropertyMetadata(
                               false,
                               new PropertyChangedCallback(IsSelectedChangeCallback)),
                       null);

        private static void IsSelectedChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MontuBoxItem ctrl = d as MontuBoxItem;
            bool oldVal = (bool)e.OldValue;
            bool newVal = (bool)e.NewValue;
            ctrl.OnSelectedChanged(oldVal, newVal);
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        #endregion

        #region GapWidth
        public static readonly DependencyProperty GapWidthProperty =
           DependencyProperty.Register(
                       "GapWidth",
                       typeof(double),
                       typeof(MontuBoxItem),
                       new PropertyMetadata(
                               10d,
                               new PropertyChangedCallback(GapWidthChangeCallback)),
                       null);

        private static void GapWidthChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public double GapWidth
        {
            get { return (double)GetValue(GapWidthProperty); }
            set { SetValue(GapWidthProperty, value); }
        }
        #endregion

        #region CheckBoxStyle
        public static readonly DependencyProperty CheckBoxStyleProperty =
           DependencyProperty.Register(
                       "CheckBoxStyle",
                       typeof(Style),
                       typeof(MontuBoxItem),
                       null);

        public Style CheckBoxStyle
        {
            get { return (Style)GetValue(CheckBoxStyleProperty); }
            set { SetValue(CheckBoxStyleProperty, value); }
        }
        #endregion

        #region IsSelectedChangedEvent
        public static readonly RoutedEvent IsSelectedChangedEvent =
            EventManager.RegisterRoutedEvent("IsSelectedChanged", RoutingStrategy.Direct,
                          typeof(IsSelectedChangedEventHandler), typeof(MontuBoxItem));

        public event IsSelectedChangedEventHandler IsSelectedChanged
        {
            add { AddHandler(IsSelectedChangedEvent, value); }
            remove { RemoveHandler(IsSelectedChangedEvent, value); }
        }
        #endregion

        private Border RootBorder { get; set; }
        private CheckBox MontuCheckBox { get; set; }
        private ContentPresenter MontuContentPresenter { get; set; }

        public MontuBoxItem()
        {
            DefaultStyleKey = typeof(MontuBoxItem);
            IsTabStop = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RootBorder = GetTemplateChild(RootBorderName) as Border;
            MontuCheckBox = GetTemplateChild(MontuCheckBoxName) as CheckBox;
            MontuContentPresenter = GetTemplateChild(MontuContentPresenterName) as ContentPresenter;

        }

        protected virtual void OnSelectedChanged(bool oldVal, bool newVal)
        {
            RaiseEvent(new IsSelectedChangedEventArgs(MontuBoxItem.IsSelectedChangedEvent, oldVal, newVal));
        }
    }

    public delegate void IsSelectedChangedEventHandler(object sender, IsSelectedChangedEventArgs e);
    public class IsSelectedChangedEventArgs : RoutedEventArgs
    {
        private bool oldValue;
        private bool newValue;

        public IsSelectedChangedEventArgs(RoutedEvent id, bool oldVal, bool newVal)
        {
            oldValue = oldVal;
            newValue = newVal;
            RoutedEvent = id;
        }

        public bool OldValue
        {
            get { return oldValue; }
        }

        public bool NewValue
        {
            get { return newValue; }
        }
    }
}
