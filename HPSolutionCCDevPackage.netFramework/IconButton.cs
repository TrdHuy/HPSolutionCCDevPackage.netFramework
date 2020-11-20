using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    public class IconButton : Button
    {
        public IconButton()
        {
            DefaultStyleKey = typeof(IconButton);
            this.IsTabStop = true;
        }

        #region Public properties

        #region IBConerRadius
        public static readonly DependencyProperty IBCornerRadiusProperty =
            DependencyProperty.Register("IBCornerRadius",
                typeof(CornerRadius),
                typeof(IconButton),
                new FrameworkPropertyMetadata(default(CornerRadius),
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender), null);

        public CornerRadius IBCornerRadius
        {
            get { return (CornerRadius)GetValue(IBCornerRadiusProperty); }
            set { SetValue(IBCornerRadiusProperty, value); }
        }
        #endregion

        #region IBContentOrientation
        public static readonly DependencyProperty IBContentOrientationProperty =
                DependencyProperty.Register(
                        "IBContentOrientation",
                        typeof(Orientation),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                Orientation.Horizontal,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public Orientation IBContentOrientation
        {
            get { return (Orientation)GetValue(IBContentOrientationProperty); }
            set { SetValue(IBContentOrientationProperty, value); }
        }
        #endregion

        #region IconSource
        public static readonly DependencyProperty IconSourceProperty =
                DependencyProperty.Register(
                        "IconSource",
                        typeof(ImageSource),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                null,
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

        #region IconHeight
        public static readonly DependencyProperty IconHeightProperty =
                DependencyProperty.Register(
                        "IconHeight",
                        typeof(double),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                30d,
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
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                30d,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        #endregion

        #region IconTextGapWidth
        public static readonly DependencyProperty IconTextGapWidthProperty =
                DependencyProperty.Register(
                        "IconTextGapWidth",
                        typeof(double),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                0d,
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
        public static readonly DependencyProperty IBTextHorizontalAlignmentProperty =
               DependencyProperty.Register(
                       "IBTextHorizontalAlignment",
                       typeof(HorizontalAlignment),
                       typeof(IconButton),
                       new FrameworkPropertyMetadata(
                               HorizontalAlignment.Center,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public static readonly DependencyProperty IBTextVerticalAlignmentProperty =
                DependencyProperty.Register(
                        "IBTextVerticalAlignment",
                        typeof(VerticalAlignment),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                VerticalAlignment.Center,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public HorizontalAlignment IBTextHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(IBTextHorizontalAlignmentProperty); }
            set { SetValue(IBTextHorizontalAlignmentProperty, value); }
        }

        public VerticalAlignment IBTextVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(IBTextVerticalAlignmentProperty); }
            set { SetValue(IBTextVerticalAlignmentProperty, value); }
        }
        #endregion

        #region TextContent
        public static readonly DependencyProperty TextContentProperty =
                DependencyProperty.Register(
                        "TextContent",
                        typeof(string),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                "",
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                null),
                        null);

        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }


        #endregion

        #region MouseOverEffectBackgroud
        public static readonly DependencyProperty MouseOverEffectBackgroudProperty =
            DependencyProperty.Register(
                        "MouseOverEffectBackgroud",
                        typeof(Brush),
                        typeof(IconButton),
                        new FrameworkPropertyMetadata(
                                new SolidColorBrush(Color.FromArgb(80, 26, 195, 237)),
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
                       typeof(IconButton),
                       new FrameworkPropertyMetadata(
                               new SolidColorBrush(Color.FromArgb(40, 26, 195, 237)),
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
                       typeof(IconButton),
                       new FrameworkPropertyMetadata(
                               default(bool),
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        public bool IsUsingDropShadowEffect
        {
            get { return (bool)GetValue(IsUsingDropShadowEffectProperty); }
            set { SetValue(IsUsingDropShadowEffectProperty, value); }
        }
        #endregion

        #endregion

        #region Private properties

        #endregion

    }
}
