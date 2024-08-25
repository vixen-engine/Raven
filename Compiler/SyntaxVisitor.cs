using Vixen.Raven.Syntax;

namespace Vixen.Raven;

public abstract class SyntaxVisitor {
    public virtual void Visit(SyntaxNode? node) {
        node?.Accept(this);
    }

    public virtual void DefaultVisit(SyntaxNode node) { }


    public virtual void VisitPackageDeclaration(PackageDeclarationSyntax node) => DefaultVisit(node);
    public virtual void VisitCompilationUnit(CompilationUnitSyntax node) => DefaultVisit(node);
}

public abstract class SyntaxVisitor<TResult> {
    public virtual TResult? Visit(SyntaxNode? node) {
        if (node != null) {
            return node.Accept(this);
        }

        return default;
    }

    public virtual TResult? DefaultVisit(SyntaxNode node) => default;
    // public virtual TResult? VisitToken(SyntaxToken token) => this.DefaultVisit(token);


    public virtual TResult? VisitPackageDeclaration(PackageDeclarationSyntax node) => DefaultVisit(node);
    public virtual TResult? VisitCompilationUnit(CompilationUnitSyntax node) => DefaultVisit(node);
}
