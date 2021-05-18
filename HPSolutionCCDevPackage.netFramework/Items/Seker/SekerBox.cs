using HPSolutionCCDevPackage.netFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    [TemplatePart(Name = SekerBox.CalendarToggleButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SekerBox.SekerPopupName, Type = typeof(Popup))]
    public class SekerBox : TextBox
    {
        /// <summary>
        /// Specifies the name of the SekerPopupName.
        /// </summary>
        protected const string SekerPopupName = "PART_sekerPopup";

        /// <summary>
        /// Specifies the name of the CalendarToggleButtonName.
        /// </summary>
        protected const string CalendarToggleButtonName = "calendarToggle";

        public SekerBox()
        {
            DefaultStyleKey = typeof(SekerBox);
            this.IsTabStop = true;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F3F3F3"));
            this.SuggestTextForegroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA000000"));
            this.CornerRadius = new CornerRadius(6);
        }

        #region CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(SekerBox),
                new FrameworkPropertyMetadata(default(CornerRadius),
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender, null), null);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region CalendarButtonStyle
        public static readonly DependencyProperty CalendarButtonStyleProperty =
         DependencyProperty.Register("CalendarButtonStyle",
             typeof(Style),
             typeof(SekerBox),
             new FrameworkPropertyMetadata(default(Style),
                                     FrameworkPropertyMetadataOptions.AffectsMeasure |
                                     FrameworkPropertyMetadataOptions.AffectsRender, null), null);

        public Style CalendarButtonStyle
        {
            get { return (Style)GetValue(CalendarButtonStyleProperty); }
            set { SetValue(CalendarButtonStyleProperty, value); }
        }
        #endregion

        #region CalendarColor
        public static readonly DependencyProperty CalendarColorProperty =
           DependencyProperty.Register(
                       "CalendarColor",
                       typeof(Brush),
                       typeof(SekerBox),
                       new FrameworkPropertyMetadata(
                               default(Brush),
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public Brush CalendarColor
        {
            get { return (Brush)GetValue(CalendarColorProperty); }
            set { SetValue(CalendarColorProperty, value); }
        }
        #endregion

        #region SuggestTextForegroundColor
        public static readonly DependencyProperty SuggestTextForegroundColorProperty =
           DependencyProperty.Register(
                       "SuggestTextForegroundColor",
                       typeof(Brush),
                       typeof(SekerBox),
                       new FrameworkPropertyMetadata(
                               default(Brush),
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public Brush SuggestTextForegroundColor
        {
            get { return (Brush)GetValue(SuggestTextForegroundColorProperty); }
            set { SetValue(SuggestTextForegroundColorProperty, value); }
        }
        #endregion

        #region DateTimeSekerStyle
        public static readonly DependencyProperty DateTimeSekerStyleProperty =
         DependencyProperty.Register("DateTimeSekerStyle",
             typeof(Style),
             typeof(SekerBox),
             new FrameworkPropertyMetadata(default(Style),
                                     FrameworkPropertyMetadataOptions.AffectsMeasure |
                                     FrameworkPropertyMetadataOptions.AffectsRender, null), null);

        public Style DateTimeSekerStyle
        {
            get { return (Style)GetValue(DateTimeSekerStyleProperty); }
            set { SetValue(DateTimeSekerStyleProperty, value); }
        }
        #endregion

        internal ICommand SekerDoneButtonCommand { get; private set; }
        private ToggleButton CalendarToggle { get; set; }
        private Popup SekerPopup { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CalendarToggle = GetTemplateChild(CalendarToggleButtonName) as ToggleButton;
            SekerPopup = GetTemplateChild(SekerPopupName) as Popup;



            var dateSeker = new DateTimeSeker()
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8B99B9")),
                CornerRadius = new CornerRadius(5),
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
                FontSize = 12
            };

            SekerPopup.Child = dateSeker;

            SekerDoneButtonCommand = new InputCommand((paramaters) =>
            {
                CalendarToggle.IsChecked = false;
                Text = dateSeker.SelectedDateTime.ToString();
            });

            dateSeker.DoneButtonCommand = SekerDoneButtonCommand;
        }
    }
}
