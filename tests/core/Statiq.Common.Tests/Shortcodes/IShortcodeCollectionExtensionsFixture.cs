﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Statiq.Testing;
using Statiq.Testing.Shortcodes;

namespace Statiq.Common.Tests.Shortcodes
{
    [TestFixture]
    public class IShortcodeCollectionExtensionsFixture : BaseFixture
    {
        public class AddTests : IShortcodeCollectionExtensionsFixture
        {
            [Test]
            public void RemovesShortcodeSuffixForType()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add(typeof(FooShortcode));

                // Then
                shortcodes.Keys.ShouldBe(new[] { "Foo" });
            }

            [Test]
            public void RemovesLowercaseShortcodeSuffixForType()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add(typeof(Barshortcode));

                // Then
                shortcodes.Keys.ShouldBe(new[] { "Bar" });
            }

            [Test]
            public void DoesNotRemoveShortcodeStringForType()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add(typeof(BazShortcodeFoo));

                // Then
                shortcodes.Keys.ShouldBe(new[] { "BazShortcodeFoo" });
            }

            [Test]
            public void RemovesShortcodeSuffixForTypeParam()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add<FooShortcode>();

                // Then
                shortcodes.Keys.ShouldBe(new[] { "Foo" });
            }

            [Test]
            public void RemovesLowercaseShortcodeSuffixForTypeParam()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add<Barshortcode>();

                // Then
                shortcodes.Keys.ShouldBe(new[] { "Bar" });
            }

            [Test]
            public void DoesNotRemoveShortcodeStringForTypeParam()
            {
                // Given
                TestShortcodeCollection shortcodes = new TestShortcodeCollection();

                // When
                shortcodes.Add<BazShortcodeFoo>();

                // Then
                shortcodes.Keys.ShouldBe(new[] { "BazShortcodeFoo" });
            }
        }

        public class FooShortcode : IShortcode
        {
            public Task<IDocument> ExecuteAsync(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
            {
                throw new NotImplementedException();
            }
        }

        public class Barshortcode : IShortcode
        {
            public Task<IDocument> ExecuteAsync(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
            {
                throw new NotImplementedException();
            }
        }

        public class BazShortcodeFoo : IShortcode
        {
            public Task<IDocument> ExecuteAsync(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
