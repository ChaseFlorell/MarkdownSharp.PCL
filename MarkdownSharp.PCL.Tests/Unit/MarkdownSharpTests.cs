using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace MarkdownSharp.PCL.Tests.Unit
{
    [TestFixture]
    public class MarkdownSharpTests
    {
        private Markdown _markdown;

        [SetUp]
        public void Setup()
        {
            _markdown = new Markdown();
        }

        [Test]
        public void ShouldCompileHeaderMarkdown()
        {
            // setup
            const string original = @"#hello world#";
            var expected = @"<p><h1>hello world</h1></p>" + Environment.NewLine;

            // execute
            var actual = _markdown.Transform(original);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCompileBoldMarkdown()
        {
            // setup
            const string original = @"**hello world**";
            var expected = @"<p><strong>hello world</strong></p>" + Environment.NewLine;

            // execute
            var actual = _markdown.Transform(original);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldCompileItalicMarkdown()
        {
            // setup
            const string original = @"*hello world*";
            var expected = @"<p><em>hello world</em></p>" + Environment.NewLine;

            // execute
            var actual = _markdown.Transform(original);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldAutoLinkEmailAddresses()
        {
            // setup
            const string original = @"<address@domain.foo>";
            // this is obfuscated see the `EmailEvaluator` method
            // it changes to different encodings so we can't actually use "actual"
            //     see the `EncodeEmailAddress` method
            // instead we'll make sure the length is right.
            var expected = @"<p><a href=""m&#97;&#x69;&#108;&#x74;&#x6f;:&#97;ddr&#x65;&#115;&#x73;&#64;&#100;&#111;&#109;&#x61;&#105;&#110;&#46;&#x66;o&#x6f;"">&#97;ddr&#x65;&#115;&#x73;&#64;&#100;&#111;&#109;&#x61;&#105;&#110;&#46;&#x66;o&#x6f;</a></p>" + Environment.NewLine;

            // execute
            var actual = _markdown.Transform(original);

            // assert
            Assert.That(actual.Length, new GreaterThanConstraint(200));
        }

        [Test]
        public void ShouldAutoLinkUrls()
        {
            // setup
            const string original = @"http://example.com";
            var expected = @"<p><a href=""http://example.com"">http://example.com</a></p>" + Environment.NewLine;

            // execute
            var actual = _markdown.Transform(original);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}