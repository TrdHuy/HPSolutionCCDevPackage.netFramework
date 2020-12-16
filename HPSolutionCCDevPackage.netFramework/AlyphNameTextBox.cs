using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HPSolutionCCDevPackage.netFramework
{
    public class AlyphNameTextBox : Control
    {

        public AlyphNameTextBox()
        {
            DefaultStyleKey = typeof(AlyphNameTextBox);
            this.IsTabStop = true;
        }

        #region Public properties

        #region TitleFontSize
        public static readonly DependencyProperty TitleFontSizeProperty =
               DependencyProperty.Register(
                       "TitleFontSize",
                       typeof(double),
                       typeof(AlyphNameTextBox),
                       new FrameworkPropertyMetadata(
                               defaultTitleFontSize,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);
        /// <summary>
        /// Font size of the title
        /// </summary>
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        #endregion

        #region TitleFontFamily
        public static readonly DependencyProperty TitleFontFamilyProperty =
               DependencyProperty.Register(
                       "TitleFontFamily",
                       typeof(FontFamily),
                       typeof(AlyphNameTextBox),
                       new FrameworkPropertyMetadata(
                               defaultTitleFontFamily,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);

        /// <summary>
        /// Font skin of title
        /// </summary>
        public FontFamily TitleFontFamily
        {
            get { return (FontFamily)GetValue(TitleFontFamilyProperty); }
            set { SetValue(TitleFontFamilyProperty, value); }
        }
        #endregion

        #region TitleFontWeight
        public static readonly DependencyProperty TitleFontWeightProperty =
               DependencyProperty.Register(
                       "TitleFontWeight",
                       typeof(FontWeight),
                       typeof(AlyphNameTextBox),
                       new FrameworkPropertyMetadata(
                               defaultTitleFontWeight,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               null),
                       null);
        /// <summary>
        /// Font weight of title
        /// </summary>
        public FontWeight TitleFontWeight
        {
            get { return (FontWeight)GetValue(TitleFontWeightProperty); }
            set { SetValue(TitleFontWeightProperty, value); }
        }
        #endregion

        #region FirstName
        public static readonly DependencyProperty FirstNameProperty =
               DependencyProperty.Register(
                       "FirstName",
                       typeof(string),
                       typeof(AlyphNameTextBox),
                       new FrameworkPropertyMetadata(
                               defaultFirstName,
                               FrameworkPropertyMetadataOptions.AffectsMeasure,
                               new PropertyChangedCallback(NameChangedCallback)),
                       null);
       
        /// <summary>
        /// First name text content
        /// </summary>
        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }
        #endregion

        #region LastName
        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
               "LastName",
               typeof(string),
               typeof(AlyphNameTextBox),
               new FrameworkPropertyMetadata(
                       defaultLastName,
                       FrameworkPropertyMetadataOptions.AffectsMeasure,
                       new PropertyChangedCallback(NameChangedCallback)),
               null);

        /// <summary>
        /// Last name text content
        /// </summary>
        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }
        #endregion

        #region Title
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
               "Title",
               typeof(string),
               typeof(AlyphNameTextBox),
               new FrameworkPropertyMetadata(
                       defaultTitle,
                       FrameworkPropertyMetadataOptions.AffectsMeasure,
                       null),
               null);

        /// <summary>
        /// Title text content
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region StrokeThickess
        public static readonly DependencyProperty StrokeThickessProperty =
            DependencyProperty.Register(
               "StrokeThickess",
               typeof(double),
               typeof(AlyphNameTextBox),
               new FrameworkPropertyMetadata(
                       defaultStrokeThickess,
                       FrameworkPropertyMetadataOptions.AffectsMeasure,
                       null),
               null);

        /// <summary>
        /// Stroke thickness of border
        /// </summary>
        public double StrokeThickess
        {
            get { return (double)GetValue(StrokeThickessProperty); }
            set { SetValue(StrokeThickessProperty, value); }
        }
        #endregion

        #region LineSpace
        public static readonly DependencyProperty LineSpaceProperty =
            DependencyProperty.Register(
               "LineSpace",
               typeof(double),
               typeof(AlyphNameTextBox),
               new FrameworkPropertyMetadata(
                       defaultLineSpace,
                       FrameworkPropertyMetadataOptions.AffectsMeasure,
                       null),
               null);

        /// <summary>
        /// Space between two lines of the border
        /// </summary>
        public double LineSpace
        {
            get { return (double)GetValue(LineSpaceProperty); }
            set { SetValue(LineSpaceProperty, value); }
        }
        #endregion

        #region FirstNameForeground
        public static readonly DependencyProperty FirstNameForegroundProperty =
            DependencyProperty.Register(
                "FirstNameForeground",
                typeof(Brush),
                typeof(AlyphNameTextBox),
                new FrameworkPropertyMetadata(
                        defaultFirstNameForeground,
                        FrameworkPropertyMetadataOptions.AffectsMeasure,
                        null),
                null);

        /// <summary>
        /// Color of the first name content
        /// </summary>
        public Brush FirstNameForeground
        {
            get { return (Brush)GetValue(FirstNameForegroundProperty); }
            set { SetValue(FirstNameForegroundProperty, value); }
        }
        #endregion

        #region LastNameForeground
        public static readonly DependencyProperty LastNameForegroundProperty =
            DependencyProperty.Register(
                "LastNameForeground",
                typeof(Brush),
                typeof(AlyphNameTextBox),
                new FrameworkPropertyMetadata(
                        defaultLastNameForeground,
                        FrameworkPropertyMetadataOptions.AffectsMeasure,
                        null),
                null);

        /// <summary>
        /// Color of the last name content
        /// </summary>
        public Brush LastNameForeground
        {
            get { return (Brush)GetValue(LastNameForegroundProperty); }
            set { SetValue(LastNameForegroundProperty, value); }
        }
        #endregion

        #endregion

        private static FontWeight defaultTitleFontWeight = default(FontWeight);
        private static double defaultTitleFontSize = 20;
        private static FontFamily defaultTitleFontFamily = default(FontFamily);
        private static string defaultFirstName = "John";
        private static string defaultLastName = "Smith";
        private static string defaultTitle = "Software Engineer";
        private static double defaultStrokeThickess = 3;
        private static double defaultLineSpace = 10;
        private static Brush defaultFirstNameForeground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        private static Brush defaultLastNameForeground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        private Canvas _canvasElement;
        private TextBlock _firstNameTextBlockElement;
        private TextBlock _lastNameTextBlockElement;
        private StackPanel _panelTextContainerElement;
        private Line _leftLineElement;
        private Line _rightLineElement;
        private Line _bottomLineLeftElement;
        private Line _bottomLineRightElement;
        private Line _topLineLeftElement;
        private Line _topLineRightElement;
        private StackPanel _tempMeasurePanelElement;

        private bool _isAppliedTemplate = false;

        private TextBlock FirstNameTextBlock
        {
            get
            {
                return _firstNameTextBlockElement;
            }
            set
            {
                if (_firstNameTextBlockElement != null)
                {
                    _firstNameTextBlockElement.SizeChanged -= new SizeChangedEventHandler(NameBoxSizeChange);
                }
                _firstNameTextBlockElement = value;

                if (_firstNameTextBlockElement != null)
                {
                    _firstNameTextBlockElement.SizeChanged += new SizeChangedEventHandler(NameBoxSizeChange);
                }
            }
        }
        private TextBlock LastNameTextBlock
        {
            get
            {
                return _lastNameTextBlockElement;
            }
            set
            {
                if (_lastNameTextBlockElement != null)
                {
                    _lastNameTextBlockElement.SizeChanged -= new SizeChangedEventHandler(NameBoxSizeChange);
                }
                _lastNameTextBlockElement = value;

                if (_lastNameTextBlockElement != null)
                {
                    _lastNameTextBlockElement.SizeChanged += new SizeChangedEventHandler(NameBoxSizeChange);
                }
            }
        }
        public override void OnApplyTemplate()
        {
            _canvasElement = GetTemplateChild("MainCanvas") as Canvas;
            FirstNameTextBlock = GetTemplateChild("FirstNameTextBlock") as TextBlock;
            LastNameTextBlock = GetTemplateChild("LastNameTextBlock") as TextBlock;

            _panelTextContainerElement = GetTemplateChild("TextContainer") as StackPanel;
            _leftLineElement = GetTemplateChild("LeftLine") as Line;
            _rightLineElement = GetTemplateChild("RightLine") as Line;
            _bottomLineLeftElement = GetTemplateChild("LeftBottomLine") as Line;
            _bottomLineRightElement = GetTemplateChild("RightBottomLine") as Line;
            _topLineLeftElement = GetTemplateChild("LeftTopLine") as Line;
            _topLineRightElement = GetTemplateChild("RightTopLine") as Line;

            _tempMeasurePanelElement = GetTemplateChild("TempMeasurePanel") as StackPanel;

            _isAppliedTemplate = true;

            UpdateVisual(false);
        }

        private void NameBoxSizeChange(object sender, SizeChangedEventArgs e)
        {
            UpdateVisual(true);
        }

        private static void NameChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AlyphNameTextBox ctrl = d as AlyphNameTextBox;
            ctrl.UpdateVisual(false);
        }

        private void UpdateVisual(bool updateLine)
        {
            if (!_isAppliedTemplate) return;
            double marginPanelLeft = (_canvasElement.ActualWidth - _panelTextContainerElement.ActualWidth) / 2;
            double marginPanelTop = (_canvasElement.ActualHeight - _panelTextContainerElement.ActualHeight) / 2;
            Canvas.SetLeft(_panelTextContainerElement, marginPanelLeft);
            Canvas.SetTop(_panelTextContainerElement, marginPanelTop);

            if (updateLine)
            {
                UpdateTopLineLeft();
                UpdateTopLineRight();
                UpdateBottomLineLeft();
                UpdateBottomLineRight();
            }
        }

        private void UpdateBottomLineRight()
        {
            _bottomLineRightElement.X1 = _panelTextContainerElement.DesiredSize.Width +
                Canvas.GetLeft(_panelTextContainerElement) +
                LineSpace;
            _bottomLineRightElement.Y1 = DesiredSize.Height;
            _bottomLineRightElement.X2 = DesiredSize.Width + StrokeThickess / 2;
            _bottomLineRightElement.Y2 = DesiredSize.Height;
        }

        private void UpdateBottomLineLeft()
        {
            _bottomLineLeftElement.X1 = 0 - StrokeThickess / 2;
            _bottomLineLeftElement.Y1 = DesiredSize.Height;
            _bottomLineLeftElement.X2 = _panelTextContainerElement.DesiredSize.Width +
                Canvas.GetLeft(_panelTextContainerElement);
            _bottomLineLeftElement.Y2 = DesiredSize.Height;
        }

        private void UpdateTopLineRight()
        {
            _topLineRightElement.X1 = Canvas.GetLeft(_panelTextContainerElement) +
                _tempMeasurePanelElement.DesiredSize.Width +
                LineSpace;
            _topLineRightElement.Y1 = 0;
            _topLineRightElement.X2 = DesiredSize.Width + StrokeThickess / 2;
            _topLineRightElement.Y2 = 0;
        }

        private void UpdateTopLineLeft()
        {
            _topLineLeftElement.X1 = 0 - StrokeThickess / 2;
            _topLineLeftElement.Y1 = 0;
            _topLineLeftElement.X2 = Canvas.GetLeft(_panelTextContainerElement) +
                FirstNameTextBlock.DesiredSize.Width;
            _topLineLeftElement.Y2 = 0;
        }


    }
}
