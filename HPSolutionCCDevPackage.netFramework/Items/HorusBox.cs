using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    public class HorusBox : ComboBox
    {
        public HorusBox()
        {
            DefaultStyleKey = typeof(HorusBox);
            this.IsTabStop = true;
        }

        #region Public properties

        #region EditTextForeground
        public static readonly DependencyProperty EditTextForegroundProperty =
           DependencyProperty.Register(
                       "EditTextForeground",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultEditTextForeground,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush EditTextForeground
        {
            get { return (Brush)GetValue(EditTextForegroundProperty); }
            set { SetValue(EditTextForegroundProperty, value); }
        }
        #endregion

        #region HorusCornerRadius
        public static readonly DependencyProperty HorusCornerRadiusProperty =
           DependencyProperty.Register(
                       "HorusCornerRadius",
                       typeof(CornerRadius),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultHorusCornerRadius,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public CornerRadius HorusCornerRadius
        {
            get { return (CornerRadius)GetValue(HorusCornerRadiusProperty); }
            set { SetValue(HorusCornerRadiusProperty, value); }
        }
        #endregion

        #region DropDownCornerRadius
        public static readonly DependencyProperty DropDownCornerRadiusProperty =
           DependencyProperty.Register(
                       "DropDownCornerRadius",
                       typeof(CornerRadius),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultDropDownCornerRadius,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public CornerRadius DropDownCornerRadius
        {
            get { return (CornerRadius)GetValue(DropDownCornerRadiusProperty); }
            set { SetValue(DropDownCornerRadiusProperty, value); }
        }
        #endregion

        #region DropDownBorderThickness
        public static readonly DependencyProperty DropDownBorderThicknessProperty =
           DependencyProperty.Register(
                       "DropDownBorderThickness",
                       typeof(Thickness),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultDropDownBorderThickness,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Thickness DropDownBorderThickness
        {
            get { return (Thickness)GetValue(DropDownBorderThicknessProperty); }
            set { SetValue(DropDownBorderThicknessProperty, value); }
        }
        #endregion

        #region DropDownBorderBrush
        public static readonly DependencyProperty DropDownBorderBrushProperty =
           DependencyProperty.Register(
                       "DropDownBorderBrush",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultDropDownBorderBrush,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush DropDownBorderBrush
        {
            get { return (Brush)GetValue(DropDownBorderBrushProperty); }
            set { SetValue(DropDownBorderBrushProperty, value); }
        }
        #endregion

        #region DropDownButtonBackground
        public static readonly DependencyProperty DropDownButtonBackgroundProperty =
           DependencyProperty.Register(
                       "DropDownButtonBackground",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultDropDownButtonBackground,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush DropDownButtonBackground
        {
            get { return (Brush)GetValue(DropDownButtonBackgroundProperty); }
            set { SetValue(DropDownButtonBackgroundProperty, value); }
        }
        #endregion

        #region DropDownIcon
        public static readonly DependencyProperty DropDownIconProperty =
                DependencyProperty.Register(
                        "DropDownIcon",
                        typeof(ImageSource),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultDropDownIcon,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public ImageSource DropDownIcon
        {
            get { return (ImageSource)GetValue(DropDownIconProperty); }
            set { SetValue(DropDownIconProperty, value); }
        }
        #endregion

        #region IconHeight
        public static readonly DependencyProperty IconHeightProperty =
                DependencyProperty.Register(
                        "IconHeight",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIconHeight,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        #endregion

        #region IconWidth
        public static readonly DependencyProperty IconWidthProperty =
                DependencyProperty.Register(
                        "IconWidth",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIconWidth,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        #endregion

        #region MouseOverEffectBackgroud
        public static readonly DependencyProperty MouseOverEffectBackgroudProperty =
            DependencyProperty.Register(
                        "MouseOverEffectBackgroud",
                        typeof(Brush),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultMouseOverEffectBackground,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public Brush MouseOverEffectBackgroud
        {
            get { return (Brush)GetValue(MouseOverEffectBackgroudProperty); }
            set { SetValue(MouseOverEffectBackgroudProperty, value); }
        }
        #endregion

        #region MousePressedEffectBackgroud
        public static readonly DependencyProperty MousePressedEffectBackgroudProperty =
           DependencyProperty.Register(
                       "MousePressedEffectBackgroud",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultMousePressedEffectBackground,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush MousePressedEffectBackgroud
        {
            get { return (Brush)GetValue(MousePressedEffectBackgroudProperty); }
            set { SetValue(MousePressedEffectBackgroudProperty, value); }
        }
        #endregion

        #region IsUsingListFilter
        public static readonly DependencyProperty IsUsingListFilterProperty =
                DependencyProperty.Register(
                        "IsUsingListFilter",
                        typeof(bool),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIsUsingListFilter,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public bool IsUsingListFilter
        {
            get { return (bool)GetValue(IsUsingListFilterProperty); }
            set { SetValue(IsUsingListFilterProperty, value); }
        }
        #endregion

        #region PopupBackground
        public static readonly DependencyProperty PopupBackgroundProperty =
           DependencyProperty.Register(
                       "PopupBackground",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultPopupBackground,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush PopupBackground
        {
            get { return (Brush)GetValue(PopupBackgroundProperty); }
            set { SetValue(PopupBackgroundProperty, value); }
        }
        #endregion

        #region PopupBorderThickness
        public static readonly DependencyProperty PopupBorderThicknessProperty =
           DependencyProperty.Register(
                       "PopupBorderThickness",
                       typeof(Thickness),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultPopupBorderThickness,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Thickness PopupBorderThickness
        {
            get { return (Thickness)GetValue(PopupBorderThicknessProperty); }
            set { SetValue(PopupBorderThicknessProperty, value); }
        }
        #endregion

        #region PopupBorderBrush
        public static readonly DependencyProperty PopupBorderBrushProperty =
           DependencyProperty.Register(
                       "PopupBorderBrush",
                       typeof(Brush),
                       typeof(HorusBox),
                       new FrameworkPropertyMetadata(
                               defaultPopupBorderBrush,
                               FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                               null),
                       null);

        public Brush PopupBorderBrush
        {
            get { return (Brush)GetValue(PopupBorderBrushProperty); }
            set { SetValue(PopupBorderBrushProperty, value); }
        }
        #endregion

        #region IsUsingHorusShadow
        public static readonly DependencyProperty IsUsingHorusShadowProperty =
                DependencyProperty.Register(
                        "IsUsingHorusShadow",
                        typeof(bool),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIsUsingHorusShadow,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public bool IsUsingHorusShadow
        {
            get { return (bool)GetValue(IsUsingHorusShadowProperty); }
            set { SetValue(IsUsingHorusShadowProperty, value); }
        }
        #endregion

        #region IsUsingHorusPopupShadow
        public static readonly DependencyProperty IsUsingHorusPopupShadowProperty =
                DependencyProperty.Register(
                        "IsUsingHorusPopupShadow",
                        typeof(bool),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIsUsingHorusPopupShadow,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public bool IsUsingHorusPopupShadow
        {
            get { return (bool)GetValue(IsUsingHorusPopupShadowProperty); }
            set { SetValue(IsUsingHorusPopupShadowProperty, value); }
        }
        #endregion

        #endregion

        #region Private properties

        private static ImageSource defaultDropDownIcon = default(ImageSource);
        private static double defaultIconHeight = 30d;
        private static double defaultIconWidth = 30d;
        private static Brush defaultMouseOverEffectBackground = new SolidColorBrush(Color.FromArgb(80, 26, 195, 237));
        private static Brush defaultMousePressedEffectBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static Brush defaultDropDownButtonBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static Brush defaultDropDownBorderBrush = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static Brush defaultPopupBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static Brush defaultPopupBorderBrush = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static Brush defaultEditTextForeground = new SolidColorBrush(Colors.Black);
        private static CornerRadius defaultHorusCornerRadius = default(CornerRadius);
        private static CornerRadius defaultDropDownCornerRadius = default(CornerRadius);
        private static Thickness defaultDropDownBorderThickness = default(Thickness);
        private static Thickness defaultPopupBorderThickness = default(Thickness);
        private static bool defaultIsUsingListFilter = false;
        private static bool defaultIsUsingHorusShadow = false;
        private static bool defaultIsUsingHorusPopupShadow = false;

        #endregion

        private TextBox _horusEditTextBoxElement;
        private ToggleButton _horusToggleElement;

        private TextBox HorusEditTextBoxElement
        {
            get
            {
                return _horusEditTextBoxElement;
            }
            set
            {
                if (_horusEditTextBoxElement != null && IsUsingListFilter)
                {
                    _horusEditTextBoxElement.SelectionChanged -=
                          new RoutedEventHandler(HorusTextSelectionChangedEvent);
                    _horusEditTextBoxElement.GotFocus -=
                       new RoutedEventHandler(HorusEditTextGotFocusEvent);
                    _horusEditTextBoxElement.KeyUp -= new KeyEventHandler(HorusEnterKeyUpEvent);
                }

                _horusEditTextBoxElement = value;

                if (_horusEditTextBoxElement != null && IsUsingListFilter)
                {
                    _horusEditTextBoxElement.GotFocus +=
                        new RoutedEventHandler(HorusEditTextGotFocusEvent);
                    _horusEditTextBoxElement.SelectionChanged +=
                          new RoutedEventHandler(HorusTextSelectionChangedEvent);
                    _horusEditTextBoxElement.KeyUp += new KeyEventHandler(HorusEnterKeyUpEvent);
                }
            }
        }
        private ToggleButton HorusToggleButtonElement
        {
            get
            {
                return _horusToggleElement;
            }
            set
            {
                if (_horusToggleElement != null)
                {
                    _horusToggleElement.Checked -= new RoutedEventHandler(HorusCheckedEvent);
                    _horusToggleElement.Unchecked -= new RoutedEventHandler(HorusUncheckedEvent);
                }
                _horusToggleElement = value;
                if (_horusToggleElement != null)
                {
                    _horusToggleElement.Checked += new RoutedEventHandler(HorusCheckedEvent);
                    _horusToggleElement.Unchecked += new RoutedEventHandler(HorusUncheckedEvent);
                }
            }
        }

        private void HorusEnterKeyUpEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Keyboard.ClearFocus();
            }
        }

        private void HorusEditTextGotFocusEvent(object sender, RoutedEventArgs e)
        {
            IsDropDownOpen = true;
        }

        private void HorusCheckedEvent(object sender, RoutedEventArgs e)
        {
            //   IsDropDownOpen = true;
        }

        private void HorusUncheckedEvent(object sender, RoutedEventArgs e)
        {
            //   IsDropDownOpen = false;
        }

        private void HorusTextSelectionChangedEvent(object sender, RoutedEventArgs e)
        {
            if (!IsUsingListFilter) return;
            try
            {
                IsDropDownOpen = true;
                TextBox ctrl = sender as TextBox;
                string filterString = ctrl.Text.Substring(0, ctrl.SelectionStart);
                Items.Filter = new Predicate<object>(o => Filter(o as ComboBoxItem, filterString));
            }
            catch
            {

            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            HorusEditTextBoxElement = GetTemplateChild("PART_EditableTextBox") as TextBox;
            HorusToggleButtonElement = GetTemplateChild("ToggleButton") as ToggleButton;
        }

        private bool Filter(ComboBoxItem cbI, string compareString)
        {
            string v = cbI.Content.ToString();
            return v.IndexOf(compareString) != -1;
        }

    }

}
