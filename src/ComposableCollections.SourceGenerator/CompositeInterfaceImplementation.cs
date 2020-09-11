using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace ComposableCollections.SourceGenerator
{

    public class CompositeInterfaceImplementation : IFluentApiSourceGenerator
    {
        private List<InterfaceDeclarationSyntax> _interfaces = new List<InterfaceDeclarationSyntax>();

        [XmlAttribute("InterfaceRegex")]
        public string InterfaceRegex {
            get
            {
                return _interfaceRegex.ToString();
            }

            set
            {
                _interfaceRegex = new Regex(value);
            }
        }

        private Regex _interfaceRegex;

        public void AddSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode.IsKind(Microsoft.CodeAnalysis.CSharp.SyntaxKind.InterfaceDeclaration))
            {
                var ifaceDeclSyntax = (InterfaceDeclarationSyntax)syntaxNode;
                _interfaces.Add(ifaceDeclSyntax);
            }
        }

        public void AddSource(Action<string, SourceText> addSource)
        {
            foreach(var iface in _interfaces)
            {
                if (_interfaceRegex.IsMatch(iface.Identifier.ValueText))
                {
                    
                }

                //iface.Members
                foreach (var baseIface in iface.BaseList.ChildNodes())
                {

                }
            }
        }
    }
}
