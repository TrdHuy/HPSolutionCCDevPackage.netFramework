using HPSolutionCCDevPackage.netFramework.Utils;
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
    /// <summary>
    /// Represents a control that displays a Chart.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplatePart(Name = RatiateableGrid.RootGridName, Type = typeof(Grid))]
    [TemplatePart(Name = RatiateableGrid.BackgroundGridName, Type = typeof(Grid))]
    [TemplatePart(Name = RatiateableGrid.MainContentPresenterName, Type = typeof(ContentPresenter))]

    [ContentProperty("Content")]
    public class RatiateableGrid : ContentControl
    {
        /// <summary>
        /// Specifies the name of the RootGrid TemplatePart.
        /// </summary>
        protected const string RootGridName = "RootGrid";

        /// <summary>
        /// Specifies the name of the RootGrid TemplatePart.
        /// </summary>
        protected const string BackgroundGridName = "BackgroundGrid";

        /// <summary>
        /// Specifies the name of the MainContentPresenter TemplatePart.
        /// </summary>
        protected const string MainContentPresenterName = "MainContentPresenter";

        #region SizeRatio
        public static readonly DependencyProperty SizeRatioProperty =
                DependencyProperty.Register(
                        "SizeRatio",
                        typeof(double),
                        typeof(RatiateableGrid),
                        new FrameworkPropertyMetadata(
                                1d,
                                FrameworkPropertyMetadataOptions.AffectsMeasure,
                                new PropertyChangedCallback(OnSizeRatioChangedCallback)),
                        null);

        private static void OnSizeRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as RatiateableGrid;
            if (ctrl.RootGrid != null)
            {
                ctrl.Ratiate(ctrl.RootGrid.RenderSize);
            }
        }

        /// <summary>
        /// Set the size ratio for width & height
        /// </summary>
        public double SizeRatio
        {
            get { return (double)GetValue(SizeRatioProperty); }
            set { SetValue(SizeRatioProperty, value); }
        }
        #endregion

        #region Revert
        public static readonly DependencyProperty RevertProperty =
           DependencyProperty.Register(
                       "Revert",
                       typeof(bool),
                       typeof(RatiateableGrid),
                       new PropertyMetadata(
                               false,
                               new PropertyChangedCallback(RevertChangedCallback)));
        /// <summary>
        /// Switch between height & width
        /// </summary>
        public bool Revert
        {
            get { return (bool)GetValue(RevertProperty); }
            set { SetValue(RevertProperty, value); }
        }

        private static void RevertChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var ctrl = d as RatiateableGrid;
            if (ctrl.RootGrid != null)
            {
                ctrl.Ratiate(ctrl.RootGrid.RenderSize);
            }
        }

        #endregion



        /// <summary>
        /// Initializes a new instance of the RatiateableGrid class.
        /// </summary>
        public RatiateableGrid()
        {
            DefaultStyleKey = typeof(RatiateableGrid);
            this.IsTabStop = true;
        }

        private Grid RootGrid { get; set; }
        private Grid BackgroundGrid { get; set; }
        private ContentPresenter MainContentPresenter { get; set; }


        /// <summary>
        /// Builds the visual tree for the SquareGrid control when a new template
        /// is applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RootGrid = GetTemplateChild(RootGridName) as Grid;

            MainContentPresenter = GetTemplateChild(MainContentPresenterName) as ContentPresenter;
            BackgroundGrid = GetTemplateChild(BackgroundGridName) as Grid;

            if (RootGrid != null)
            {
                RootGrid.SizeChanged -= OnRootGridSizeChanged;
                RootGrid.SizeChanged += OnRootGridSizeChanged;

            }
        }

        private void OnRootGridSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var parents = System.Windows.Media.VisualTreeHelper.GetParent(this) as UIElement;
            if (parents != null && parents is ScrollContentPresenter)
            {
                Ratiate(parents.RenderSize);
            }
            else
            {
                Ratiate(e.NewSize);
            }
        }

        private void Ratiate(Size e)
        {
            if (MainContentPresenter != null)
            {
                RatiateUIObject(MainContentPresenter, e);
            }

            if (BackgroundGrid != null)
            {
                RatiateUIObject(BackgroundGrid, e);
            }
        }
        private void RatiateUIObject(FrameworkElement UIelement, Size size)
        {
            if (!size.IsNaN() && !size.IsZero())
            {
                var width = size.Width;
                var height = size.Height;

                var selectedSize = Revert ?
                    width < height ? height : width
                    : width < height ? width : height;

                UIelement.Height = selectedSize;
                UIelement.Width = selectedSize * SizeRatio;
            }
        }
    }
}
