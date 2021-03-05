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
    [TemplatePart(Name = SquareGrid.RootGridName, Type = typeof(Grid))]
    [TemplatePart(Name = SquareGrid.BackgroundGridName, Type = typeof(Grid))]
    [TemplatePart(Name = SquareGrid.MainContentPresenterName, Type = typeof(ContentPresenter))]

    [ContentProperty("Content")]
    public class SquareGrid : ContentControl
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

        /// <summary>
        /// Initializes a new instance of the SquareGrid class.
        /// </summary>
        public SquareGrid()
        {
            DefaultStyleKey = typeof(SquareGrid);
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
            if (MainContentPresenter != null)
            {
                SquareUIObject(MainContentPresenter, e.NewSize);
            }

            if (BackgroundGrid != null)
            {
                SquareUIObject(BackgroundGrid, e.NewSize);
            }
        }

        private void SquareUIObject(FrameworkElement UIelement, Size size)
        {
            if (!size.IsNaN() && !size.IsZero())
            {
                var width = size.Width;
                var height = size.Height;

                var selectedSize = width < height ? width : height;

                UIelement.Height = selectedSize;
                UIelement.Width = selectedSize;
            }
        }
    }

}
