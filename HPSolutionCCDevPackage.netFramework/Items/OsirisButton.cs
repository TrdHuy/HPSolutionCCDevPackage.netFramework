using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    public class OsirisButton : Button
    {
        public OsirisButton()
        {
            DefaultStyleKey = typeof(OsirisButton);
            this.IsTabStop = true;
        }

        #region Public properties

        #region IBConerRadius
        public static readonly DependencyProperty OBCornerRadiusProperty =
            DependencyProperty.Register("IBCornerRadius",
                typeof(CornerRadius),
                typeof(OsirisButton),
                new FrameworkPropertyMetadata(defaultOBCornerRadius,
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender), null);

        public CornerRadius OBCornerRadius
        {
            get { return (CornerRadius)GetValue(OBCornerRadiusProperty); }
            set { SetValue(OBCornerRadiusProperty, value); }
        }
        #endregion

        #region IBContentOrientation
        public static readonly DependencyProperty OBContentOrientationProperty =
                DependencyProperty.Register(
                        "IBContentOrientation",
                        typeof(Orientation),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultOBContentOrientation,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public Orientation OBContentOrientation
        {
            get { return (Orientation)GetValue(OBContentOrientationProperty); }
            set { SetValue(OBContentOrientationProperty, value); }
        }
        #endregion

        #region IconSource
        public static readonly DependencyProperty IconSourceProperty =
                DependencyProperty.Register(
                        "IconSource",
                        typeof(ImageSource),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultIconSource,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        #endregion

        #region IconStretch
        public static readonly DependencyProperty IconStretchProperty =
                DependencyProperty.Register(
                        "IconStretch",
                        typeof(Stretch),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultIconStretch,
                                FrameworkPropertyMetadataOptions.AffectsMeasure |
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null,
                                null),
                        null);

        public Stretch IconStretch
        {
            get { return (Stretch)GetValue(IconStretchProperty); }
            set { SetValue(IconStretchProperty, value); }
        }
        #endregion

        #region IconHeight
        public static readonly DependencyProperty IconHeightProperty =
                DependencyProperty.Register(
                        "IconHeight",
                        typeof(double),
                        typeof(OsirisButton),
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
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultIconWidth,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(OnTransformDirty)),
                        null);

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }


        private static void OnTransformDirty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Callback for MinWidth, MaxWidth, Width, MinHeight, MaxHeight, Height, and RenderTransformOffset
            //fe.AreTransformsClean = false;
            OsirisButton m = (OsirisButton)d;
        }

        #endregion

        #region IconTextGapWidth
        public static readonly DependencyProperty IconTextGapWidthProperty =
                DependencyProperty.Register(
                        "IconTextGapWidth",
                        typeof(double),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultIconTextGapWidth,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public double IconTextGapWidth
        {
            get { return (double)GetValue(IconTextGapWidthProperty); }
            set { SetValue(IconTextGapWidthProperty, value); }
        }
        #endregion

        #region TextAligment
        public static readonly DependencyProperty OBTextHorizontalAlignmentProperty =
               DependencyProperty.Register(
                       "IBTextHorizontalAlignment",
                       typeof(HorizontalAlignment),
                       typeof(OsirisButton),
                       new FrameworkPropertyMetadata(
                               defaultOBTextHorizontalAlignment,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public static readonly DependencyProperty OBTextVerticalAlignmentProperty =
                DependencyProperty.Register(
                        "IBTextVerticalAlignment",
                        typeof(VerticalAlignment),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultOBTextVerticalAlignment,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public HorizontalAlignment OBTextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(OBTextHorizontalAlignmentProperty); }
            set { SetValue(OBTextHorizontalAlignmentProperty, value); }
        }

        public VerticalAlignment OBTextVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(OBTextVerticalAlignmentProperty); }
            set { SetValue(OBTextVerticalAlignmentProperty, value); }
        }
        #endregion

        #region TextContent
        public static readonly DependencyProperty TextContentProperty =
                DependencyProperty.Register(
                        "TextContent",
                        typeof(string),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultTextContent,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }


        #endregion

        #region TextDecor
        public static readonly DependencyProperty TextDecorProperty =
                DependencyProperty.Register(
                        "TextDecor",
                        typeof(TextDecorationCollection),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultTextDecor,
                                FrameworkPropertyMetadataOptions.AffectsRender,
                                null),
                        null);

        public TextDecorationCollection TextDecor
        {
            get { return (TextDecorationCollection)GetValue(TextDecorProperty); }
            set { SetValue(TextDecorProperty, value); }
        }


        #endregion

        #region MouseOverEffectBackgroud
        public static readonly DependencyProperty MouseOverEffectBackgroudProperty =
            DependencyProperty.Register(
                        "MouseOverEffectBackgroud",
                        typeof(Brush),
                        typeof(OsirisButton),
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
                       typeof(OsirisButton),
                       new FrameworkPropertyMetadata(
                               defaultMousePressedEffectBackground,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public Brush MousePressedEffectBackgroud
        {
            get { return (Brush)GetValue(MousePressedEffectBackgroudProperty); }
            set { SetValue(MousePressedEffectBackgroudProperty, value); }
        }
        #endregion

        #region IsUsingDropShadowEffect
        public static readonly DependencyProperty IsUsingDropShadowEffectProperty =
           DependencyProperty.Register(
                       "IsUsingDropShadowEffect",
                       typeof(bool),
                       typeof(OsirisButton),
                       new FrameworkPropertyMetadata(
                               defaultIsUsingDropShadowEffect,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public bool IsUsingDropShadowEffect
        {
            get { return (bool)GetValue(IsUsingDropShadowEffectProperty); }
            set { SetValue(IsUsingDropShadowEffectProperty, value); }
        }
        #endregion

        #region IsBusy
        public static readonly DependencyProperty IsBusyProperty =
           DependencyProperty.Register(
                       "IsBusy",
                       typeof(bool),
                       typeof(OsirisButton),
                       new FrameworkPropertyMetadata(
                               defaultIsBusy,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               new PropertyChangedCallback(BusyChangedCallback)),
                       null);

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly RoutedEvent IsBusyChangedEvent =
            EventManager.RegisterRoutedEvent("IsBusyChanged", RoutingStrategy.Direct,
                          typeof(IsBusyChangedEventHandler), typeof(OsirisButton));

        public event IsBusyChangedEventHandler IsBusyChanged
        {
            add { AddHandler(IsBusyChangedEvent, value); }
            remove { RemoveHandler(IsBusyChangedEvent, value); }
        }

        private static void BusyChangedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            OsirisButton ctl = (OsirisButton)obj;
            bool newValue = (bool)args.NewValue;

            // Call UpdateStates because the Value might have caused the
            // control to change ValueStates.
            ctl.UpdateStates(true);

            // Call OnValueChanged to raise the ValueChanged event.
            ctl.OnBusyChanged(
                new IsBusyChangedEventArgs(OsirisButton.IsBusyChangedEvent,
                    newValue));
        }

        protected virtual void OnBusyChanged(IsBusyChangedEventArgs e)
        {
            RaiseEvent(e);
        }
        #endregion

        #region ProgressSpinnerSize
        public static readonly DependencyProperty ProgressSpinnerSizeProperty =
                DependencyProperty.Register(
                        "ProgressSpinnerSize",
                        typeof(double),
                        typeof(OsirisButton),
                        new FrameworkPropertyMetadata(
                                defaultProgressSpinnerSize,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public double ProgressSpinnerSize
        {
            get { return (double)GetValue(ProgressSpinnerSizeProperty); }
            set { SetValue(ProgressSpinnerSizeProperty, value); }
        }
        #endregion

        #region ProgressSpinnerBackground
        public static readonly DependencyProperty ProgressSpinnerBackgroundProperty =
            DependencyProperty.Register(
                "ProgressSpinnerBackground",
                typeof(Brush),
                typeof(OsirisButton),
                new FrameworkPropertyMetadata(
                        defaultProgressSpinnerBackground,
                        FrameworkPropertyMetadataOptions.AffectsMeasure,
                        null),
                null);

        public Brush ProgressSpinnerBackground
        {
            get { return (Brush)GetValue(ProgressSpinnerBackgroundProperty); }
            set { SetValue(ProgressSpinnerBackgroundProperty, value); }
        }
        #endregion

        #endregion


        #region Private properties

        private static CornerRadius defaultOBCornerRadius = default(CornerRadius);
        private static Orientation defaultOBContentOrientation = Orientation.Horizontal;
        private static ImageSource defaultIconSource = null;
        private static double defaultIconHeight = 0d;
        private static double defaultIconWidth = 0d;
        private static double defaultIconTextGapWidth = Double.NaN;
        private static HorizontalAlignment defaultOBTextHorizontalAlignment = HorizontalAlignment.Center;
        private static VerticalAlignment defaultOBTextVerticalAlignment = VerticalAlignment.Center;
        private static string defaultTextContent = null;
        private static Brush defaultMouseOverEffectBackground = new SolidColorBrush(Color.FromArgb(80, 26, 195, 237));
        private static Brush defaultMousePressedEffectBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static bool defaultIsUsingDropShadowEffect = default(bool);
        private static bool defaultIsBusy = default(bool);
        private static double defaultProgressSpinnerSize = 20d;
        private static Brush defaultProgressSpinnerBackground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        private static Stretch defaultIconStretch = Stretch.None;
        private static TextDecorationCollection defaultTextDecor = TextDecorations.Baseline;
        
        #endregion

        #region Override fields
        protected override void OnClick()
        {
            if (IsBusy)
            {
                return;
            }
            base.OnClick();
        }
        #endregion


        private void UpdateStates(bool useTransitions)
        {

            if (IsBusy)
            {
                VisualStateManager.GoToState(this, "Busy", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "UnBusy", useTransitions);
            }
        }

    }

    public delegate void IsBusyChangedEventHandler(object sender, IsBusyChangedEventArgs e);

    public class IsBusyChangedEventArgs : RoutedEventArgs
    {
        private bool _value;

        public IsBusyChangedEventArgs(RoutedEvent id, bool val)
        {
            _value = val;
            RoutedEvent = id;
        }

        public bool Value
        {
            get { return _value; }
        }
    }
}
