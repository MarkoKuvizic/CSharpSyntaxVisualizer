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
using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CSharp;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SyntaxVisualizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<SyntaxNodeItemViewModel> SyntaxNodes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            UpdateCode();
            AnalyzeButton.Click += UpdateCode;

        }

        private void UpdateCode(object sender, RoutedEventArgs e)
        {
            UpdateCode();
        }

        private void UpdateCode()
        {
            CodeAnalyzer.Analyze(new TextRange(
                CodeBox.Document.ContentStart.GetPositionAtOffset(0),
                CodeBox.Document.ContentEnd.GetPositionAtOffset(-1)
            ).Text);

            SyntaxNode root = CodeAnalyzer.TREE.GetRoot();

            SyntaxNodes = new ObservableCollection<SyntaxNodeItemViewModel>
            {
                new SyntaxNodeItemViewModel(root)
            };
            
            syntaxTreeView.ItemsSource = SyntaxNodes;
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (syntaxTreeView.SelectedItem is SyntaxNodeItemViewModel syntaxNodeItemViewModel)
            {
                int[] span = _spanStringToIndices(syntaxNodeItemViewModel.Span);
                _highlightText(span[0], span[1]);
                e.Handled = true;
                

            }
        }

        private int[] _spanStringToIndices(string span)
        {
            span = span.Trim();
            span = span.Replace("[", "").Replace("]", "").Replace(")", "");
            int[] indices = new int[]
            {
                Convert.ToInt32(span.Split("..")[0]), Convert.ToInt32(span.Split("..")[1])
            };
            return indices;
        }

        private void _highlightText(int startIndex, int endIndex)
        {
            _clearHighlighting();
            TextPointer start = CodeBox.Document.ContentStart.GetPositionAtOffset(startIndex);
            TextPointer end = CodeBox.Document.ContentStart.GetPositionAtOffset(endIndex);
            if (start != null && end != null)
            {
                TextRange range = new TextRange(start, end);

                range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Yellow);
            }
        }

        private void _clearHighlighting()
        {
            TextRange documentRange = new TextRange(CodeBox.Document.ContentStart, CodeBox.Document.ContentEnd);
            documentRange.ClearAllProperties();
        }
    }
}


