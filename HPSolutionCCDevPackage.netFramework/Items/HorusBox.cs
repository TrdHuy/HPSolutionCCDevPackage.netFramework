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
using HPSolutionCCDevPackage.netFramework.Utils;
using System.Windows.Data;

namespace HPSolutionCCDevPackage.netFramework
{
    public class HorusBox : ComboBox
    {
        private static Logger logger = new Logger("HorusBox");

        public HorusBox()
        {
            DefaultStyleKey = typeof(HorusBox);
            this.IsTabStop = true;
            //this.MinHeight = 30;
            //this.MinWidth = 100;
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
                                FrameworkPropertyMetadataOptions.AffectsRender, null),
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

        #region IsUsingIgnoreLowerUpperCase
        public static readonly DependencyProperty IsUsingIgnoreLowerUpperCaseProperty =
                DependencyProperty.Register(
                        "IsUsingIgnoreLowerUpperCase",
                        typeof(bool),
                        typeof(HorusBox),
                        new PropertyMetadata(false));

        public bool IsUsingIgnoreLowerUpperCase
        {
            get { return (bool)GetValue(IsUsingIgnoreLowerUpperCaseProperty); }
            set { SetValue(IsUsingIgnoreLowerUpperCaseProperty, value); }
        }
        #endregion

        #region FilterPathWhileUsingItemTemplate
        public static readonly DependencyProperty FilterPathWhileUsingItemTemplateProperty =
                DependencyProperty.Register(
                        "FilterPathWhileUsingItemTemplate",
                        typeof(string),
                        typeof(HorusBox),
                        new PropertyMetadata(""));

        public string FilterPathWhileUsingItemTemplate
        {
            get { return (string)GetValue(FilterPathWhileUsingItemTemplateProperty); }
            set { SetValue(FilterPathWhileUsingItemTemplateProperty, value); }
        }
        #endregion

        #region FilterPathItems
        public static readonly DependencyProperty FilterPathItemsProperty =
                DependencyProperty.Register(
                        "FilterPathItems",
                        typeof(string[]),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                default(string[]),
                                new PropertyChangedCallback(FilterPathItemsValueChangedCallBack)));

        private static void FilterPathItemsValueChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HorusBox ctrl = d as HorusBox;
            if (!String.IsNullOrEmpty(ctrl.FilterPathWhileUsingItemTemplate))
            {
                throw new InvalidOperationException("Cannot use both FilterPathWhileUsingItemTemplate and FilterPathItems");
            }
        }

        public string[] FilterPathItems
        {
            get { return (string[])GetValue(FilterPathItemsProperty); }
            set { SetValue(FilterPathItemsProperty, value); }
        }
        #endregion

        #region SelectionHorusBoxItem
        private static readonly DependencyPropertyKey SelectionHorusBoxItemPropertyKey =
           DependencyProperty.RegisterReadOnly("SelectionHorusBoxItem", typeof(object), typeof(HorusBox),
                                               new FrameworkPropertyMetadata(String.Empty));

        public static readonly DependencyProperty SelectionHorusBoxItemProperty = SelectionHorusBoxItemPropertyKey.DependencyProperty;
        /// <summary>
        /// Used to display the selected item
        /// </summary>
        public object SelectionHorusBoxItem
        {
            get { return GetValue(SelectionHorusBoxItemProperty); }
            private set { SetValue(SelectionHorusBoxItemPropertyKey, value); }
        }
        #endregion

        #region SelectionHorusBoxItemTemplate
        private static readonly DependencyPropertyKey SelectionHorusBoxItemTemplatePropertyKey =
            DependencyProperty.RegisterReadOnly("SelectionHorusBoxItemTemplate", typeof(DataTemplate), typeof(HorusBox),
                                                new FrameworkPropertyMetadata((DataTemplate)null));

        /// <summary>
        /// The DependencyProperty for the SelectionBoxItemProperty
        /// </summary>
        public static readonly DependencyProperty SelectionHorusBoxItemTemplateProperty = SelectionHorusBoxItemTemplatePropertyKey.DependencyProperty;

        /// <summary>
        /// Used to set the item DataTemplate
        /// </summary>
        public DataTemplate SelectionHorusBoxItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectionHorusBoxItemTemplateProperty); }
            private set { SetValue(SelectionHorusBoxItemTemplatePropertyKey, value); }
        }
        #endregion

        #region SelectionHorusBoxItemStringFormat
        private static readonly DependencyPropertyKey SelectionHorusBoxItemStringFormatPropertyKey =
            DependencyProperty.RegisterReadOnly("SelectionHorusBoxItemStringFormat", typeof(String), typeof(HorusBox),
                                                new FrameworkPropertyMetadata((String)null));

        /// <summary>
        /// The DependencyProperty for the SelectionBoxItemProperty
        /// </summary>
        public static readonly DependencyProperty SelectionHorusBoxItemStringFormatProperty = SelectionHorusBoxItemStringFormatPropertyKey.DependencyProperty;

        /// <summary>
        /// Used to set the item DataStringFormat
        /// </summary>
        public String SelectionHorusBoxItemStringFormat
        {
            get { return (String)GetValue(SelectionHorusBoxItemStringFormatProperty); }
            private set { SetValue(SelectionHorusBoxItemStringFormatPropertyKey, value); }
        }
        #endregion

        #region ContentAreaWidth
        public static readonly DependencyProperty ContentAreaWidthProperty =
                DependencyProperty.Register(
                        "ContentAreaWidth",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultContentAreaWidth,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(ContentAreaSizeChanegCallBack)),
                        null);

        public double ContentAreaWidth
        {
            get { return (double)GetValue(ContentAreaWidthProperty); }
            set { SetValue(ContentAreaWidthProperty, value); }
        }
        #endregion

        #region ContentAreaHeight
        public static readonly DependencyProperty ContentAreaHeightProperty =
                DependencyProperty.Register(
                        "ContentAreaHeight",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultContentAreaHeight,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(ContentAreaSizeChanegCallBack)),
                        null);

        public double ContentAreaHeight
        {
            get { return (double)GetValue(ContentAreaHeightProperty); }
            set { SetValue(ContentAreaHeightProperty, value); }
        }
        #endregion

        #region DropDownAreaWidth
        public static readonly DependencyProperty DropDownAreaWidthProperty =
                DependencyProperty.Register(
                        "DropDownAreaWidth",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultDropDownAreaWidth,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(ContentAreaSizeChanegCallBack)),
                        null);

        public double DropDownAreaWidth
        {
            get { return (double)GetValue(DropDownAreaWidthProperty); }
            set { SetValue(DropDownAreaWidthProperty, value); }
        }
        #endregion

        #region DropDownAreaHeight
        public static readonly DependencyProperty DropDownAreaHeightProperty =
                DependencyProperty.Register(
                        "DropDownAreaHeight",
                        typeof(double),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultDropDownAreaHeight,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(ContentAreaSizeChanegCallBack)),
                        null);

        private static void ContentAreaSizeChanegCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HorusBox ctrl = d as HorusBox;
        }

        public double DropDownAreaHeight
        {
            get { return (double)GetValue(DropDownAreaHeightProperty); }
            set { SetValue(DropDownAreaHeightProperty, value); }
        }
        #endregion

        #region RemoveDropDownIcon
        public static readonly DependencyProperty RemoveDropDownIconProperty =
                DependencyProperty.Register(
                        "RemoveDropDownIcon",
                        typeof(bool),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultRemoveDropDownIcon,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender, null),
                        null);

        public bool RemoveDropDownIcon
        {
            get { return (bool)GetValue(RemoveDropDownIconProperty); }
            set { SetValue(RemoveDropDownIconProperty, value); }
        }
        #endregion

        #region FilterText
        public static readonly DependencyProperty FilterTextProperty =
           DependencyProperty.Register(
                       "FilterText",
                       typeof(string),
                       typeof(HorusBox),
                       new PropertyMetadata(defaultFilterText),
                       null);

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }
        #endregion

        #region DropDownChangedEvent
        public static readonly RoutedEvent DropDownChangedEvent =
            EventManager.RegisterRoutedEvent("DropDownChanged", RoutingStrategy.Direct,
                          typeof(DropDownOpenEventHandler), typeof(HorusBox));

        public event DropDownOpenEventHandler DropDownChanged
        {
            add { AddHandler(DropDownChangedEvent, value); }
            remove { RemoveHandler(DropDownChangedEvent, value); }
        }
        #endregion

        #region IsUsingLoadingPopupAsync
        public static readonly DependencyProperty IsUsingLoadingPopupAsyncProperty =
                DependencyProperty.Register(
                        "IsUsingLoadingPopupAsync",
                        typeof(bool),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                defaultIsUsingLoadingPopupAsync,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public bool IsUsingLoadingPopupAsync
        {
            get { return (bool)GetValue(IsUsingLoadingPopupAsyncProperty); }
            set { SetValue(IsUsingLoadingPopupAsyncProperty, value); }
        }
        #endregion

        #region LoadingPopupDelayTime
        public static readonly DependencyProperty LoadingPopupDelayTimeProperty =
           DependencyProperty.Register("LoadingPopupDelayTime", typeof(long), typeof(HorusBox),
             new PropertyMetadata(defaultLoadingPopupDelayTime));

        /// <summary>
        /// Delay a period of specifictime when open popup
        /// </summary>
        public long LoadingPopupDelayTime
        {
            get { return (long)GetValue(LoadingPopupDelayTimeProperty); }
            set { SetValue(LoadingPopupDelayTimeProperty, value); }
        }
        #endregion

        #region SelectedItemTemplate
        public static readonly DependencyProperty SelectedItemTemplateProperty =
                DependencyProperty.Register(
                        "SelectedItemTemplate",
                        typeof(DataTemplate),
                        typeof(HorusBox),
                        new FrameworkPropertyMetadata(
                                default(DataTemplate),
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(SelectedItemTemplateChangedCallBack)),
                        null);

        private static void SelectedItemTemplateChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            HorusBox ctrl = d as HorusBox;
            ctrl.UpdateBindingSelectedItemTemplate();
        }

        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }
        #endregion

        #endregion



        //------------------------------------------



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
        private static bool defaultRemoveDropDownIcon = false;
        private static bool defaultIsUsingLoadingPopupAsync = false;
        private static double defaultContentAreaWidth = 100d;
        private static double defaultContentAreaHeight = 30d;
        private static double defaultDropDownAreaHeight = 30d;
        private static double defaultDropDownAreaWidth = 30d;
        private static string defaultFilterText = "";
        private static long defaultLoadingPopupDelayTime = 100;

        private TextBox _horusEditTextBoxElement;
        private TextBox _horusFilterEditTextBoxElement;
        private Popup _horusPopupElement;
        private ToggleButton _horusToggleElement;
        private ScrollViewer _contentPresenterElement;
        private ScrollViewer _customHorusContentPresenterElement;

        private bool IsSelectionChangeEventUpdatingText { get; set; } = false;
        private bool IsFilterUpdatingSelectIndex { get; set; } = false;
        private bool IsPreviewKeyUpAndDown { get; set; } = false;
        private bool IsHorusFilterTextBoxLostFocusEventCallTextChangedEvent { get; set; } = false;
        private Popup HorusPopupElement
        {
            get
            {
                return _horusPopupElement;
            }
            set
            {
                if (_horusPopupElement != null)
                {
                    _horusPopupElement.Opened -= new EventHandler(HorusPopup_Opened);
                }
                _horusPopupElement = value;
                if (_horusPopupElement != null)
                {
                    _horusPopupElement.Opened += new EventHandler(HorusPopup_Opened);
                }
            }
        }
        private ScrollViewer ContentPresenterElement
        {
            get
            {
                return _contentPresenterElement;
            }
            set
            {
                _contentPresenterElement = value;
            }
        }
        private ScrollViewer CustomHorusContentPresenterElement
        {
            get
            {
                return _customHorusContentPresenterElement;
            }
            set
            {
                _customHorusContentPresenterElement = value;
            }
        }
        private TextBox HorusEditTextBoxElement
        {
            get
            {
                return _horusEditTextBoxElement;
            }
            set
            {
                _horusEditTextBoxElement = value;
            }
        }
        private TextBox HorusFilterEditTextBoxElement
        {
            get
            {
                return _horusFilterEditTextBoxElement;
            }
            set
            {
                _horusFilterEditTextBoxElement = value;
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
                    _horusToggleElement.Checked -= new RoutedEventHandler(HorusToggle_Checked);
                    _horusToggleElement.Unchecked -= new RoutedEventHandler(HorusToggle_Unchecked);

                }
                _horusToggleElement = value;
                if (_horusToggleElement != null)
                {
                    _horusToggleElement.Checked += new RoutedEventHandler(HorusToggle_Checked);
                    _horusToggleElement.Unchecked += new RoutedEventHandler(HorusToggle_Unchecked);

                }
            }
        }

        #endregion



        //------------------------------------------



        #region Override methods
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ValidateUsingListFilterPossibility();
            this.SizeChanged += HorusBoxSizeChangedEvent;

            HorusPopupElement = GetTemplateChild("PART_Popup") as Popup;
            HorusFilterEditTextBoxElement = GetTemplateChild("FilterEditTextBox") as TextBox;
            HorusEditTextBoxElement = GetTemplateChild("PART_EditableTextBox") as TextBox;

            HorusToggleButtonElement = GetTemplateChild("ToggleButton") as ToggleButton;
            ContentPresenterElement = GetTemplateChild("contentPresenter") as ScrollViewer;
            CustomHorusContentPresenterElement = GetTemplateChild("CustomPresenterWhenUseEditable") as ScrollViewer;

            if (IsUsingListFilter)
            {
                HorusEditTextBoxElement.Visibility = Visibility.Collapsed;
                HorusFilterEditTextBoxElement.Visibility = IsEditable ? Visibility.Visible : Visibility.Collapsed;
                if (IsEditable)
                {
                    HorusFilterEditTextBoxElement.GotFocus += new RoutedEventHandler(HorusFilterEditTextGotFocusEvent);
                    HorusFilterEditTextBoxElement.LostFocus += new RoutedEventHandler(HorusFilterEditTextLostFocusEvent);
                    HorusFilterEditTextBoxElement.TextChanged += new TextChangedEventHandler(HorusFilterEditTexChangedEvent);
                    HorusFilterEditTextBoxElement.PreviewMouseDown += new MouseButtonEventHandler(HorusFilterEditTexPreviewMouseDownEvent);
                }
            }
            else
            {
                HorusEditTextBoxElement.Visibility = IsEditable ? Visibility.Visible : Visibility.Collapsed;
                HorusFilterEditTextBoxElement.Visibility = Visibility.Collapsed;
            }

            if (IsEditable && ItemTemplate != null)
            {
                ContentPresenterElement.Visibility = Visibility.Collapsed;
                CustomHorusContentPresenterElement.Visibility = IsUsingListFilter ? Visibility.Visible : Visibility.Collapsed;
                UpdateBindingSelectedItemTemplate();
            }
            else if (!IsEditable && ItemTemplate != null)
            {
                ContentPresenterElement.Visibility = Visibility.Visible;
                CustomHorusContentPresenterElement.Visibility = Visibility.Collapsed;
                UpdateBindingSelectedItemTemplate();
            }
            else if (!IsEditable && ItemTemplate == null)
            {
                ContentPresenterElement.Visibility = Visibility.Visible;
                CustomHorusContentPresenterElement.Visibility = Visibility.Collapsed;
                UpdateBindingSelectedItemTemplate();
            }

        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size newSize = base.MeasureOverride(constraint);
            if (VerticalAlignment == VerticalAlignment.Stretch)
            {
                if (Double.IsInfinity(constraint.Height))
                {
                    //newSize.Height = this.MinHeight;
                }
            }

            if (HorizontalAlignment == HorizontalAlignment.Stretch)
            {
                if (Double.IsInfinity(constraint.Width))
                {
                    //newSize.Width = this.MinWidth;
                }
            }
            return newSize;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            // Only process preview key events if they going to our editable text box
            if (IsEditable && e.OriginalSource == HorusFilterEditTextBoxElement)
            {
                if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    IsPreviewKeyUpAndDown = true;
                }
                base.OnKeyDown(e);
            }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            // If is not the filter thread call, so do job
            if (!IsFilterUpdatingSelectIndex && IsUsingListFilter)
            {
                bool selectAll = true;
                bool usingTextChangeCallback = true;

                // Change the flag when enter text change event
                IsSelectionChangeEventUpdatingText = true;

                // If user use navigating key to change selection
                if (IsPreviewKeyUpAndDown)
                {
                    UpdateHorusFilterEditableTextBox(selectAll, usingTextChangeCallback);
                    IsPreviewKeyUpAndDown = false;
                }
                // If user use mouse and normal case to change selection
                else
                {
                    UpdateHorusFilterEditableTextBox(selectAll, usingTextChangeCallback);
                    IsDropDownOpen = false;
                }

                IsSelectionChangeEventUpdatingText = false;
            }
            UpdateHorusContentPresenter(true);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            if (IsEditable && IsUsingListFilter)
            {
                HorusFilterEditTextBoxElement.Visibility = Visibility.Visible;
                HorusFilterEditTextBoxElement.Focus();
                e.Handled = true;
            }
        }
        #endregion



        //------------------------------------------



        #region Private methods
        protected void OnDropDownOpenChange(DropDownOpenEventArgs dropDownOpenEventArgs)
        {
            logger.I("DropDownOpen = " + dropDownOpenEventArgs.Value);
            //Changed event
            IsDropDownOpen = dropDownOpenEventArgs.Value;
            RaiseEvent(dropDownOpenEventArgs);
        }
        private void HorusToggle_Checked(object sender, RoutedEventArgs e)
        {
            OnDropDownOpenChange(new DropDownOpenEventArgs(HorusBox.DropDownChangedEvent, true));
        }
        private void HorusToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            OnDropDownOpenChange(new DropDownOpenEventArgs(HorusBox.DropDownChangedEvent, false));
        }

        private void HorusPopup_Opened(object sender, EventArgs e)
        {
            logger.I("Horrus popup opened");
        }


        private void HorusFilterEditTexChangedEvent(object sender, TextChangedEventArgs e)
        {
            TextBox ctrl = sender as TextBox;

            // If the flag is un-executed condition, return here 
            if (IsHorusFilterTextBoxLostFocusEventCallTextChangedEvent)
            {
                return;
            }

            if (!IsSelectionChangeEventUpdatingText && ctrl.IsLoaded)
            {
                try
                {
                    IsDropDownOpen = true;

                    string filterString = ctrl.Text.Substring(0, ctrl.SelectionStart);
                    DoFilter(filterString);
                    UpdateSeletedIndex("filter_text_change");
                }
                catch (Exception ex)
                {
                    logger.E(ex.Message);
                }
            }
        }

        private void HorusFilterEditTextLostFocusEvent(object sender, RoutedEventArgs e)
        {
            UpdateHorusContentPresenter(false);
        }

        private void UpdateHorusContentPresenter(bool IsSelectionChangeCall)
        {
            if (!IsInitialized)
            {
                logger.I("Horus was not initialized");
                return;
            }

            if (IsEditable)
            {
                bool selectAll = false;
                bool usingTextChangeCallback = false;

                // In case using custom template for item view
                if (ItemTemplate != null)
                {
                    if (!IsSelectionChangeCall)
                    {
                        // In case there was not any selected item, the filter edit text will be visible instead of item presenter
                        if (SelectedIndex != -1)
                        {
                            HorusFilterEditTextBoxElement.Visibility = Visibility.Collapsed;
                            CustomHorusContentPresenterElement.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            HorusFilterEditTextBoxElement.Visibility = Visibility.Visible;
                            CustomHorusContentPresenterElement.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        // In case selection change event call update presenter,
                        // if both edit text box and presenter are visible, hide the edit text
                        // show the presenter.
                        if (HorusFilterEditTextBoxElement.Visibility == Visibility.Visible &&
                            CustomHorusContentPresenterElement.Visibility == Visibility.Visible)
                        {
                            HorusFilterEditTextBoxElement.Visibility = Visibility.Collapsed;
                            CustomHorusContentPresenterElement.Visibility = Visibility.Visible;
                        }
                    }

                    SelectionHorusBoxItem = SelectedItem;
                    SelectionHorusBoxItemTemplate = ItemTemplate;
                }
                else
                {
                    // In case there was not any selected item, the filter edit text will be visible instead of item presenter
                    if (SelectedIndex != -1 && !IsSelectionChangeCall)
                    {
                        // copy the text of HorusBox to filter edit text box
                        UpdateHorusFilterEditableTextBox(selectAll, usingTextChangeCallback);
                    }
                }
            }
            logger.I("IsEditable = " + IsEditable + " IsFocused = " + IsFocused + " IsSelectionChangeCall = " + IsSelectionChangeCall);
            logger.I("CustomHorusContentPresenter visibility = " + CustomHorusContentPresenterElement?.Visibility);
            logger.I("HorusFilterEditTextBox visibility = " + HorusFilterEditTextBoxElement?.Visibility);
            logger.I("Text = " + Text);

        }

        private void HorusFilterEditTextGotFocusEvent(object sender, RoutedEventArgs e)
        {
            // When text box got focus, it will return full of items list
            // Also applied in case popup change its focus to text box
            // and first time got focus from user
            DoFilter("");

            if (IsEditable && ItemTemplate != null)
            {
                HorusFilterEditTextBoxElement.Visibility = Visibility.Visible;
                CustomHorusContentPresenterElement.Visibility = Visibility.Collapsed;
            }
        }

        private void HorusFilterEditTexPreviewMouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            HorusToggleButtonElement.IsChecked = !HorusToggleButtonElement.IsChecked;
        }

        private void HorusBoxSizeChangedEvent(object sender, SizeChangedEventArgs e)
        {
            double horusContentViewRatio = 0.89d;
            double horusDropDownIconViewRatio = 1 - horusContentViewRatio;

            if (ContentAreaWidth == 0 && ContentAreaHeight == 0
                && DropDownAreaHeight == 0 && DropDownAreaWidth == 0)
            {

                ContentAreaHeight = e.NewSize.Height;
                DropDownAreaHeight = RemoveDropDownIcon ? 0 : e.NewSize.Height;

                ContentAreaWidth = RemoveDropDownIcon ? e.NewSize.Width : e.NewSize.Width * horusContentViewRatio;
                DropDownAreaWidth = RemoveDropDownIcon ? 0 : e.NewSize.Width * horusDropDownIconViewRatio;
            }
            else
            {
                if (VerticalAlignment == VerticalAlignment.Stretch)
                {
                    ContentAreaWidth = RemoveDropDownIcon ? e.NewSize.Width : e.NewSize.Width * horusContentViewRatio;
                    DropDownAreaWidth = RemoveDropDownIcon ? 0 : e.NewSize.Width * horusDropDownIconViewRatio;
                }
                if (HorizontalAlignment == HorizontalAlignment.Stretch)
                {
                    ContentAreaHeight = e.NewSize.Height;
                    DropDownAreaHeight = RemoveDropDownIcon ? 0 : e.NewSize.Height;
                }
            }
        }

        private void UpdateBindingSelectedItemTemplate()
        {
            if (ContentPresenterElement != null && CustomHorusContentPresenterElement != null)
            {
                if (SelectedItemTemplate != null)
                {

                    var binding = new Binding("SelectedItemTemplate")
                    {
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        RelativeSource = new RelativeSource()
                        {
                            Mode = RelativeSourceMode.TemplatedParent
                        }
                    };
                    ContentPresenterElement.SetBinding(ContentControl.ContentTemplateProperty, binding);
                    CustomHorusContentPresenterElement.SetBinding(ContentControl.ContentTemplateProperty, binding);

                    logger.I("Binded selected item template to custom template");

                }
                else
                {
                    var binding = new Binding("SelectionBoxItemTemplate")
                    {
                        Mode = BindingMode.OneWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                        RelativeSource = new RelativeSource()
                        {
                            Mode = RelativeSourceMode.TemplatedParent
                        }
                    };
                    ContentPresenterElement.SetBinding(ContentControl.ContentTemplateProperty, binding);
                    CustomHorusContentPresenterElement.SetBinding(ContentControl.ContentTemplateProperty, binding);

                }
            }

        }

        private void ValidateUsingListFilterPossibility()
        {
            if (IsUsingListFilter && !IsEditable)
            {
                logger.F("Cannot use list filter when un-editabl");
                throw new InvalidOperationException("Cannot use list filter when un-editable");
            }
        }

        private void UpdateHorusFilterEditableTextBox(bool isSelectAll, bool isUsingTextChangeCallback)
        {
            try
            {
                if (isUsingTextChangeCallback)
                {
                    string text = Text;
                    // Copy ComboBox.Text to the editable TextBox
                    if (text != null && HorusFilterEditTextBoxElement != null && HorusFilterEditTextBoxElement.Text != text)
                    {
                        HorusFilterEditTextBoxElement.Text = text;
                        if (isSelectAll)
                        {
                            HorusFilterEditTextBoxElement.SelectAll();
                        }
                    }
                }
                else
                {
                    // try to copy text of horusbox to filter edit text box if they are different
                    if (!HorusFilterEditTextBoxElement.Text.Equals(Text))
                    {
                        // change the flag when enter text changed event
                        IsHorusFilterTextBoxLostFocusEventCallTextChangedEvent = true;

                        // copy the text
                        HorusFilterEditTextBoxElement.Text = Text;

                        // change the flag when outer text changed event
                        IsHorusFilterTextBoxLostFocusEventCallTextChangedEvent = false;
                    }
                }

            }
            catch (Exception e)
            {
                logger.E(e.Message);
                MessageBox.Show(e.Message);
            }
        }

        private void DoFilter(string filterString)
        {
            try
            {
                // Change the flag when enter selection change event
                // Remember: in filter process (Predicate), the selection change will be called
                // hence should update the flag before doing filter
                IsFilterUpdatingSelectIndex = true;

                if (ItemsSource != null)
                {
                    Items.Filter = new Predicate<object>(o => Filter(o, filterString));
                }
                else
                {
                    Items.Filter = new Predicate<object>(o => Filter(o as ComboBoxItem, filterString));
                }
            }
            finally
            {
                IsFilterUpdatingSelectIndex = false;
            }
        }

        private void UpdateSeletedIndex(string handle)
        {
            switch (handle)
            {
                case "filter_text_change":
                    if (Items.Count > 0)
                    {
                        // If the text of horus was empty, mean user not input anything
                        if (String.IsNullOrEmpty(HorusFilterEditTextBoxElement.Text))
                        {
                            SelectedIndex = -1;
                        }
                        else
                        {
                            // Update the selected index to first item of the list while popup is opening
                            if (IsDropDownOpen)
                            {
                                // Must set this flag to bypass OnselectionChange
                                IsFilterUpdatingSelectIndex = true;

                                SelectedIndex = 0;

                                IsFilterUpdatingSelectIndex = false;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        private bool Filter(ComboBoxItem cbI, string compareString)
        {
            if (cbI == null) return true;

            string v = cbI.Content.ToString();
            if (IsUsingIgnoreLowerUpperCase)
            {
                v = v.ToLower();
                compareString = compareString.ToLower();
            }
            return v.IndexOf(compareString) != -1;
        }

        private bool Filter(object o, string compareString)
        {
            if (String.IsNullOrEmpty(compareString))
            {
                return true;
            }

            string v = "";
            if (!String.IsNullOrEmpty(DisplayMemberPath))
            {
                v = o.GetType().GetProperty(DisplayMemberPath).GetValue(o).ToString();
            }
            else if (!String.IsNullOrEmpty(FilterPathWhileUsingItemTemplate))
            {
                v = o.GetType().GetProperty(FilterPathWhileUsingItemTemplate).GetValue(o).ToString();
            }
            else if (FilterPathItems != null && FilterPathItems.Length >= 0)
            {
                return FilterWithPathItems(o, compareString);
            }

            if (String.IsNullOrEmpty(v))
            {
                return true;
            }

            if (IsUsingIgnoreLowerUpperCase)
            {
                v = v.ToLower();
                compareString = compareString.ToLower();
            }
            return v.IndexOf(compareString) != -1;
        }

        private bool FilterWithPathItems(object o, string compareString)
        {
            int itemsCount = FilterPathItems.Length;
            for (int i = 0; i < itemsCount; i++)
            {
                string v = o.GetType().GetProperty(FilterPathItems[i]).GetValue(o).ToString();
                if (IsUsingIgnoreLowerUpperCase)
                {
                    v = v.ToLower();
                    compareString = compareString.ToLower();
                }
                if (v.IndexOf(compareString) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }

    public class DropDownOpenEventArgs : RoutedEventArgs
    {
        private bool _value;

        public DropDownOpenEventArgs(RoutedEvent id, bool val)
        {
            _value = val;
            RoutedEvent = id;
        }

        public bool Value
        {
            get { return _value; }
        }
    }
    public delegate void DropDownOpenEventHandler(object sender, DropDownOpenEventArgs e);

}
