using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    [TemplatePart(Name = FinderBox.FinderButtonName, Type = typeof(Button))]
    public class FinderBox : TextBox
    {
        protected const string FinderBoxResourcePath = "/HPSolutionCCDevPackage.netFramework;component/ControlTemplateResources/FinderBox.xaml";

        /// <summary>
        /// Specifies the name of the FinderButtonName.
        /// </summary>
        protected const string FinderButtonName = "PART_btn";

        public FinderBox()
        {
            DefaultStyleKey = typeof(FinderBox);
            this.IsTabStop = true;
            this.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F3F3F3"));
            this.SuggestTextForegroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AA000000"));
            this.CornerRadius = new CornerRadius(6);
        }

        #region CornerRadius
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius",
                typeof(CornerRadius),
                typeof(FinderBox),
                new FrameworkPropertyMetadata(default(CornerRadius),
                                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                                        FrameworkPropertyMetadataOptions.AffectsRender, null), null);

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion

        #region SuggestTextForegroundColor
        public static readonly DependencyProperty SuggestTextForegroundColorProperty =
           DependencyProperty.Register(
                       "SuggestTextForegroundColor",
                       typeof(Brush),
                       typeof(FinderBox),
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

        #region FinderButtonStyle
        public static readonly DependencyProperty FinderButtonStyleProperty =
         DependencyProperty.Register("FinderButtonStyle",
             typeof(Style),
             typeof(FinderBox),
             new FrameworkPropertyMetadata(defaultFinderButtonStyle,
                                     FrameworkPropertyMetadataOptions.AffectsRender, null), null);

        public Style FinderButtonStyle
        {
            get { return (Style)GetValue(FinderButtonStyleProperty); }
            set { SetValue(FinderButtonStyleProperty, value); }
        }
        #endregion

        #region FinderButtonCommand
        public static readonly DependencyProperty FinderButtonCommandProperty =
            DependencyProperty.Register("FinderButtonCommand",
                typeof(ICommand),
                typeof(FinderBox),
                new PropertyMetadata(default(ICommand), new PropertyChangedCallback(FinderButtonCommandPropertyChangedCallback)));

        private static void FinderButtonCommandPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as FinderBox;
            ctrl.OnFinderButtonCommandChanged(e.NewValue as ICommand);
        }

        public ICommand FinderButtonCommand
        {
            get
            {
                return (ICommand)GetValue(FinderButtonCommandProperty);
            }
            set
            {
                SetValue(FinderButtonCommandProperty, value);
            }
        }
        #endregion

        private static Style defaultFinderButtonStyle
        {
            get
            {
                var st = (ResourceDictionary)Application.LoadComponent(new Uri(FinderBoxResourcePath, UriKind.Relative));
                return (Style)st["DefaultFinderBtnStyle"];
            }
        }

        private Button FinderButton { get; set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            FinderButton = GetTemplateChild(FinderButtonName) as Button;
            FinderButton.Command = FinderButtonCommand;
        }


        private void OnFinderButtonCommandChanged(ICommand cmd)
        {
            if (FinderButton != null)
            {
                FinderButton.Command = cmd;
            }
        }
    }
}
