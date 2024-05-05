using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using System.Windows.Controls;
using Microsoft.CodeAnalysis;

namespace SyntaxVisualizer
{
    public partial class SyntaxNodeItemControl : UserControl
    {
        public static readonly DependencyProperty NodeTypeProperty =
            DependencyProperty.Register("NodeType", typeof(string), typeof(SyntaxNodeItemControl), new PropertyMetadata(""));

        public static readonly DependencyProperty SpanProperty =
            DependencyProperty.Register("Span", typeof(string), typeof(SyntaxNodeItemControl), new PropertyMetadata(""));
        public event EventHandler<SyntaxNode> NodeDoubleClicked;

        public string NodeType
        {
            get { return (string)GetValue(NodeTypeProperty); }
            set { SetValue(NodeTypeProperty, value); }
        }

        public string Span
        {
            get { return (string)GetValue(SpanProperty); }
            set { SetValue(SpanProperty, value); }
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            NodeDoubleClicked?.Invoke(this, DataContext as SyntaxNode);
        }

        public SyntaxNodeItemControl()
        {
            InitializeComponent();
        }
    }
}