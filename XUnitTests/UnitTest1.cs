using System;
using Xunit;
using ConsoleInterpretor;
using System.Reflection;
using System.Collections.Generic;

namespace XUnitTests
{
    public class TestingConsoleInterpretor
    {
        [Fact]
        public void ParseEmpty()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Empty(tokens);
        }

        [Fact]
        public void ParseWord()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "Hello";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Single(tokens);
            Assert.Contains(word, tokens);
        }

        [Fact]
        public void ParseWordWithSpaces()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "  Hello  ";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Single(tokens);
            Assert.Contains("Hello", tokens);
        }

        [Fact]
        public void ParseTwoWords()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "Hello  world";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Equal(2, tokens.Count);
            Assert.Contains("Hello", tokens);
            Assert.Contains("world", tokens);
        }

        [Fact]
        public void ParseWordEqualWord()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "Hello=word";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Equal(3, tokens.Count);
            Assert.Contains("Hello", tokens);
            Assert.Contains("=", tokens);
            Assert.Contains("word", tokens);

        }

        [Fact]
        public void ParseQuotes()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "\"Hello world!\"";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Single(tokens);
            Assert.Contains("Hello world!", tokens);
        }

        [Fact]
        public void ParseWordQuotesWord()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "Hi \"Mark Mark\" goodbye";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Equal(3, tokens.Count);
            Assert.Contains("Hi", tokens);
            Assert.Contains("Mark Mark", tokens);
            Assert.Contains("goodbye", tokens);
        }

        [Fact]
        public void ParseQuotesWordEqualWordQuotes()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "\"London\"is=capitalOf\"Great Britain\"";

            var tokens = (List<string>)method.Invoke(commandInterpretor, new object[] { word });
            Assert.Equal(5, tokens.Count);
            Assert.Contains("London", tokens);
            Assert.Contains("is", tokens);
            Assert.Contains("=", tokens);
            Assert.Contains("capitalOf", tokens);
            Assert.Contains("Great Britain", tokens);
        }

        [Fact]
        public void ParseWordUnexpectedChar()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "Hello!";

            Action act = () => method.Invoke(commandInterpretor, new object[] { word });

            var ex = Assert.Throws<TargetInvocationException>(act);
            Assert.IsType<ArgumentException>(ex.InnerException);
        }

        [Fact]
        public void ParseWithoutClosenQuote()
        {
            Type classType = typeof(CommandInterpretor);
            var commandInterpretor = Activator.CreateInstance(classType);
            var method = classType.GetMethod("TokenizeCommand", BindingFlags.NonPublic | BindingFlags.Instance);

            string word = "\"Hi there!";

            Action act = () => method.Invoke(commandInterpretor, new object[] { word });

            var ex = Assert.Throws<TargetInvocationException>(act);
            Assert.IsType<ArgumentException>(ex.InnerException);
        }

    }
}

