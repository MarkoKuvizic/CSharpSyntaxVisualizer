using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Windows.Input;

namespace SyntaxVisualizer
{
    public class SyntaxNodeItemViewModel
    {
        private readonly SyntaxNode _syntaxNode;

        public string NodeType => _syntaxNode.Kind().ToString();
        public string Span => _syntaxNode.Span.ToString();
        public ObservableCollection<SyntaxNodeItemViewModel> Children { get; }
        public event EventHandler<SyntaxNode> NodeDoubleClicked;
        public ICommand DoubleClickCommand { get; }



        public SyntaxNodeItemViewModel(SyntaxNode syntaxNode)
        {
            _syntaxNode = syntaxNode;
            Children = new ObservableCollection<SyntaxNodeItemViewModel>(
                syntaxNode.ChildNodes().Select(child => new SyntaxNodeItemViewModel(child)));

            DoubleClickCommand = new RelayCommand(o =>
            {
                NodeDoubleClicked?.Invoke(this, _syntaxNode);
            });
        }
    }
}