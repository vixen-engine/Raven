using Vixen.Raven.Grammar;
using Vixen.Raven.Syntax;

namespace Vixen.Raven.SyntaxBuilder;

public partial class SyntaxAntlrVisitor(SyntaxTree tree) : RavenParser2BaseVisitor<SyntaxNode> {

    public override SyntaxNode VisitCompilation_unit(RavenParser2.Compilation_unitContext context) {
        var package = Visit(context.package_declaration()) as PackageDeclarationSyntax;
        
        // TODO
        return new CompilationUnitSyntax(tree, null);
    }

    public override SyntaxNode VisitPackage_declaration(RavenParser2.Package_declarationContext context) {
        // var identifier = Visit(context.name());//  as Identifier;
        
        
        // TODO
        return new PackageDeclarationSyntax(tree, null);
    }
}