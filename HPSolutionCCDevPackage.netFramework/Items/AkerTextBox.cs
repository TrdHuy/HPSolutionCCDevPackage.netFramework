using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace HPSolutionCCDevPackage.netFramework
{
    public class AkerTextBox : TextBox
    {
        public AkerTextBox()
        {
            DefaultStyleKey = typeof(AkerTextBox);
            this.IsTabStop = true;
        }
        private ScrollViewer _scrollViewerElement;
        private FrameworkElement _textBoxViewElement;
        private TextBlock _suggestTextBlockElement;

        private TextBlock SuggestTextBlockElement
        {
            get
            {
                return _suggestTextBlockElement;
            }
            set
            {
                _suggestTextBlockElement = value;
            }
        }

        private ScrollViewer ScrollViewerElement
        {
            get
            {
                return _scrollViewerElement;
            }
            set
            {
                if (_scrollViewerElement != null)
                {
                    _scrollViewerElement.SizeChanged -= new SizeChangedEventHandler(Scroll_SizeChanged);
                }
                _scrollViewerElement = value;
                if (_scrollViewerElement != null)
                {
                    _scrollViewerElement.SizeChanged += new SizeChangedEventHandler(Scroll_SizeChanged);

                }
            }
        }

        private FrameworkElement TextBoxViewElement
        {
            get
            {
                return _textBoxViewElement;
            }
            set
            {
                _textBoxViewElement = value;
            }
        }

        private void Scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSuggestTextLocation();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ScrollViewerElement = GetTemplateChild("PART_ContentHost") as ScrollViewer;
            TextBoxViewElement = ScrollViewerElement.Content as FrameworkElement;
            SuggestTextBlockElement = GetTemplateChild("SugestionContent") as TextBlock;

        }

        private void UpdateSuggestTextLocation()
        {
            SuggestTextBlockElement.Width = TextBoxViewElement.ActualWidth;
        }

    }
}
