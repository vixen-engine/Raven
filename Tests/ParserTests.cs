﻿using Antlr4.Runtime;
using Vixen.Raven;
using Vixen.Raven.Antlr;
using Vixen.Raven.Ast;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class ParserTests(ITestOutputHelper log) {
    [Fact]
    void TestAntlrParser() {
        var stream = new AntlrInputStream(File.ReadAllText("../../../../Feed/Example1.rvn"));
        var lexer = new RavenLexer(stream);
        var tokenStream = new CommonTokenStream(lexer);
        var parser = new RavenParser(tokenStream);

        var visitor = new BuildAstVisitor();

        // Entrypoint?
        var tree = parser.compilation_unit();
        var module = tree.Accept(visitor) as Module;

        Assert.Equal("Vixen.Test", module.Package.Name.Text);

        var shader = module.Declarations.OfType<Shader>().First();
        Assert.Equal("TestShader", shader.Name.Text);

        Assert.Equal(2, shader.Declarations.OfType<ConstructorDeclaration>().Count());

        var testMethod = shader.Declarations.OfType<MethodDeclaration>().First(x => x.Name == "TestMethod");
        Assert.Equal("name", testMethod.Parameters[0].Name);
        Assert.Equal("count", testMethod.Parameters[1].Name);

        Assert.Equal(12, shader.Declarations.Count);
    }
}
