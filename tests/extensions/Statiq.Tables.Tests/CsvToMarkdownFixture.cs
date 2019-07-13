﻿using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Statiq.Common;
using Statiq.Testing;
using Statiq.Testing.Documents;

namespace Statiq.Tables.Tests
{
    [TestFixture]
    public class CsvToMarkdownFixture : BaseFixture
    {
        public class ExecuteTests : CsvToMarkdownFixture
        {
            [Test]
            public async Task TestWithoutHeader()
            {
                // Given
                string input = string.Empty
        + "\"\",\"A\",\"B\",\"C\",\"D\",\"E\",\"F\",\"G\"\r\n"
+ "\"1\",\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\"\r\n"
+ "\"2\",\"2\",\"4\",\"6\",\"8\",\"10\",\"12\",\"14\"\r\n"
+ "\"3\",\"3\",\"6\",\"9\",\"12\",\"15\",\"18\",\"21\"\r\n"
+ "\"4\",\"4\",\"8\",\"12\",\"16\",\"20\",\"24\",\"28\"\r\n"
+ "\"5\",\"5\",\"10\",\"15\",\"20\",\"25\",\"30\",\"35\"\r\n"
+ "\"6\",\"6\",\"12\",\"18\",\"24\",\"30\",\"36\",\"42\"\r\n"
+ "\"7\",\"7\",\"14\",\"21\",\"28\",\"35\",\"42\",\"49\"\r\n"
+ "\"8\",\"8\",\"16\",\"24\",\"32\",\"40\",\"48\",\"56\"\r\n"
+ "\"9\",\"9\",\"18\",\"27\",\"36\",\"45\",\"54\",\"63\"\r\n"
+ "\"10\",\"10\",\"20\",\"30\",\"40\",\"50\",\"60\",\"70\"\r\n"
+ "\"11\",\"11\",\"22\",\"33\",\"44\",\"55\",\"66\",\"77\"\r\n"
+ "\"12\",\"12\",\"24\",\"36\",\"48\",\"60\",\"72\",\"84\"\r\n"
+ "\"13\",\"13\",\"26\",\"39\",\"52\",\"65\",\"78\",\"91\"\r\n"
+ "\"14\",\"14\",\"28\",\"42\",\"56\",\"70\",\"84\",\"98\"\r\n"
+ "\"15\",\"15\",\"30\",\"45\",\"60\",\"75\",\"90\",\"105\"\r\n"
+ "\"16\",\"16\",\"32\",\"48\",\"64\",\"80\",\"96\",\"112\"\r\n"
+ "\"17\",\"17\",\"34\",\"51\",\"68\",\"85\",\"102\",\"119\"\r\n"
+ "\"18\",\"18\",\"36\",\"54\",\"72\",\"90\",\"108\",\"126\"\r\n"
+ "\"19\",\"19\",\"38\",\"57\",\"76\",\"95\",\"114\",\"133\"\r\n"
+ "\"20\",\"20\",\"40\",\"60\",\"80\",\"100\",\"120\",\"140\"\r\n"
+ "\"21\",\"21\",\"42\",\"63\",\"84\",\"105\",\"126\",\"147\"\r\n"
+ "\"22\",\"22\",\"44\",\"66\",\"88\",\"110\",\"132\",\"154\"\r\n"
+ "\"23\",\"23\",\"46\",\"69\",\"92\",\"115\",\"138\",\"161\"\r\n"
+ "\"24\",\"24\",\"48\",\"72\",\"96\",\"120\",\"144\",\"168\"\r\n"
+ "\"25\",\"25\",\"50\",\"75\",\"100\",\"125\",\"150\",\"175\"\r\n"
+ "\"26\",\"26\",\"52\",\"78\",\"104\",\"130\",\"156\",\"182\"\r\n";

                const string output = @"+----+----+----+----+-----+-----+-----+-----+
|    | A  | B  | C  | D   | E   | F   | G   |
+----+----+----+----+-----+-----+-----+-----+
| 1  | 1  | 2  | 3  | 4   | 5   | 6   | 7   |
+----+----+----+----+-----+-----+-----+-----+
| 2  | 2  | 4  | 6  | 8   | 10  | 12  | 14  |
+----+----+----+----+-----+-----+-----+-----+
| 3  | 3  | 6  | 9  | 12  | 15  | 18  | 21  |
+----+----+----+----+-----+-----+-----+-----+
| 4  | 4  | 8  | 12 | 16  | 20  | 24  | 28  |
+----+----+----+----+-----+-----+-----+-----+
| 5  | 5  | 10 | 15 | 20  | 25  | 30  | 35  |
+----+----+----+----+-----+-----+-----+-----+
| 6  | 6  | 12 | 18 | 24  | 30  | 36  | 42  |
+----+----+----+----+-----+-----+-----+-----+
| 7  | 7  | 14 | 21 | 28  | 35  | 42  | 49  |
+----+----+----+----+-----+-----+-----+-----+
| 8  | 8  | 16 | 24 | 32  | 40  | 48  | 56  |
+----+----+----+----+-----+-----+-----+-----+
| 9  | 9  | 18 | 27 | 36  | 45  | 54  | 63  |
+----+----+----+----+-----+-----+-----+-----+
| 10 | 10 | 20 | 30 | 40  | 50  | 60  | 70  |
+----+----+----+----+-----+-----+-----+-----+
| 11 | 11 | 22 | 33 | 44  | 55  | 66  | 77  |
+----+----+----+----+-----+-----+-----+-----+
| 12 | 12 | 24 | 36 | 48  | 60  | 72  | 84  |
+----+----+----+----+-----+-----+-----+-----+
| 13 | 13 | 26 | 39 | 52  | 65  | 78  | 91  |
+----+----+----+----+-----+-----+-----+-----+
| 14 | 14 | 28 | 42 | 56  | 70  | 84  | 98  |
+----+----+----+----+-----+-----+-----+-----+
| 15 | 15 | 30 | 45 | 60  | 75  | 90  | 105 |
+----+----+----+----+-----+-----+-----+-----+
| 16 | 16 | 32 | 48 | 64  | 80  | 96  | 112 |
+----+----+----+----+-----+-----+-----+-----+
| 17 | 17 | 34 | 51 | 68  | 85  | 102 | 119 |
+----+----+----+----+-----+-----+-----+-----+
| 18 | 18 | 36 | 54 | 72  | 90  | 108 | 126 |
+----+----+----+----+-----+-----+-----+-----+
| 19 | 19 | 38 | 57 | 76  | 95  | 114 | 133 |
+----+----+----+----+-----+-----+-----+-----+
| 20 | 20 | 40 | 60 | 80  | 100 | 120 | 140 |
+----+----+----+----+-----+-----+-----+-----+
| 21 | 21 | 42 | 63 | 84  | 105 | 126 | 147 |
+----+----+----+----+-----+-----+-----+-----+
| 22 | 22 | 44 | 66 | 88  | 110 | 132 | 154 |
+----+----+----+----+-----+-----+-----+-----+
| 23 | 23 | 46 | 69 | 92  | 115 | 138 | 161 |
+----+----+----+----+-----+-----+-----+-----+
| 24 | 24 | 48 | 72 | 96  | 120 | 144 | 168 |
+----+----+----+----+-----+-----+-----+-----+
| 25 | 25 | 50 | 75 | 100 | 125 | 150 | 175 |
+----+----+----+----+-----+-----+-----+-----+
| 26 | 26 | 52 | 78 | 104 | 130 | 156 | 182 |
+----+----+----+----+-----+-----+-----+-----+
";
                TestDocument document = new TestDocument(input);
                CsvToMarkdown module = new CsvToMarkdown();

                // When
                TestDocument result = await ExecuteAsync(document, module).SingleAsync();

                // Then
                result.Content.ShouldBe(output, StringCompareShould.IgnoreLineEndings);
            }

            [Test]
            public async Task TestHeader()
            {
                // Given
                string input = string.Empty
        + "\"\",\"A\",\"B\",\"C\",\"D\",\"E\",\"F\",\"G\"\r\n"
+ "\"1\",\"1\",\"2\",\"3\",\"4\",\"5\",\"6\",\"7\"\r\n"
+ "\"2\",\"2\",\"4\",\"6\",\"8\",\"10\",\"12\",\"14\"\r\n"
+ "\"3\",\"3\",\"6\",\"9\",\"12\",\"15\",\"18\",\"21\"\r\n"
+ "\"4\",\"4\",\"8\",\"12\",\"16\",\"20\",\"24\",\"28\"\r\n"
+ "\"5\",\"5\",\"10\",\"15\",\"20\",\"25\",\"30\",\"35\"\r\n"
+ "\"6\",\"6\",\"12\",\"18\",\"24\",\"30\",\"36\",\"42\"\r\n"
+ "\"7\",\"7\",\"14\",\"21\",\"28\",\"35\",\"42\",\"49\"\r\n"
+ "\"8\",\"8\",\"16\",\"24\",\"32\",\"40\",\"48\",\"56\"\r\n"
+ "\"9\",\"9\",\"18\",\"27\",\"36\",\"45\",\"54\",\"63\"\r\n"
+ "\"10\",\"10\",\"20\",\"30\",\"40\",\"50\",\"60\",\"70\"\r\n"
+ "\"11\",\"11\",\"22\",\"33\",\"44\",\"55\",\"66\",\"77\"\r\n"
+ "\"12\",\"12\",\"24\",\"36\",\"48\",\"60\",\"72\",\"84\"\r\n"
+ "\"13\",\"13\",\"26\",\"39\",\"52\",\"65\",\"78\",\"91\"\r\n"
+ "\"14\",\"14\",\"28\",\"42\",\"56\",\"70\",\"84\",\"98\"\r\n"
+ "\"15\",\"15\",\"30\",\"45\",\"60\",\"75\",\"90\",\"105\"\r\n"
+ "\"16\",\"16\",\"32\",\"48\",\"64\",\"80\",\"96\",\"112\"\r\n"
+ "\"17\",\"17\",\"34\",\"51\",\"68\",\"85\",\"102\",\"119\"\r\n"
+ "\"18\",\"18\",\"36\",\"54\",\"72\",\"90\",\"108\",\"126\"\r\n"
+ "\"19\",\"19\",\"38\",\"57\",\"76\",\"95\",\"114\",\"133\"\r\n"
+ "\"20\",\"20\",\"40\",\"60\",\"80\",\"100\",\"120\",\"140\"\r\n"
+ "\"21\",\"21\",\"42\",\"63\",\"84\",\"105\",\"126\",\"147\"\r\n"
+ "\"22\",\"22\",\"44\",\"66\",\"88\",\"110\",\"132\",\"154\"\r\n"
+ "\"23\",\"23\",\"46\",\"69\",\"92\",\"115\",\"138\",\"161\"\r\n"
+ "\"24\",\"24\",\"48\",\"72\",\"96\",\"120\",\"144\",\"168\"\r\n"
+ "\"25\",\"25\",\"50\",\"75\",\"100\",\"125\",\"150\",\"175\"\r\n"
+ "\"26\",\"26\",\"52\",\"78\",\"104\",\"130\",\"156\",\"182\"\r\n";

                const string output = @"+----+----+----+----+-----+-----+-----+-----+
|    | A  | B  | C  | D   | E   | F   | G   |
+====+====+====+====+=====+=====+=====+=====+
| 1  | 1  | 2  | 3  | 4   | 5   | 6   | 7   |
+----+----+----+----+-----+-----+-----+-----+
| 2  | 2  | 4  | 6  | 8   | 10  | 12  | 14  |
+----+----+----+----+-----+-----+-----+-----+
| 3  | 3  | 6  | 9  | 12  | 15  | 18  | 21  |
+----+----+----+----+-----+-----+-----+-----+
| 4  | 4  | 8  | 12 | 16  | 20  | 24  | 28  |
+----+----+----+----+-----+-----+-----+-----+
| 5  | 5  | 10 | 15 | 20  | 25  | 30  | 35  |
+----+----+----+----+-----+-----+-----+-----+
| 6  | 6  | 12 | 18 | 24  | 30  | 36  | 42  |
+----+----+----+----+-----+-----+-----+-----+
| 7  | 7  | 14 | 21 | 28  | 35  | 42  | 49  |
+----+----+----+----+-----+-----+-----+-----+
| 8  | 8  | 16 | 24 | 32  | 40  | 48  | 56  |
+----+----+----+----+-----+-----+-----+-----+
| 9  | 9  | 18 | 27 | 36  | 45  | 54  | 63  |
+----+----+----+----+-----+-----+-----+-----+
| 10 | 10 | 20 | 30 | 40  | 50  | 60  | 70  |
+----+----+----+----+-----+-----+-----+-----+
| 11 | 11 | 22 | 33 | 44  | 55  | 66  | 77  |
+----+----+----+----+-----+-----+-----+-----+
| 12 | 12 | 24 | 36 | 48  | 60  | 72  | 84  |
+----+----+----+----+-----+-----+-----+-----+
| 13 | 13 | 26 | 39 | 52  | 65  | 78  | 91  |
+----+----+----+----+-----+-----+-----+-----+
| 14 | 14 | 28 | 42 | 56  | 70  | 84  | 98  |
+----+----+----+----+-----+-----+-----+-----+
| 15 | 15 | 30 | 45 | 60  | 75  | 90  | 105 |
+----+----+----+----+-----+-----+-----+-----+
| 16 | 16 | 32 | 48 | 64  | 80  | 96  | 112 |
+----+----+----+----+-----+-----+-----+-----+
| 17 | 17 | 34 | 51 | 68  | 85  | 102 | 119 |
+----+----+----+----+-----+-----+-----+-----+
| 18 | 18 | 36 | 54 | 72  | 90  | 108 | 126 |
+----+----+----+----+-----+-----+-----+-----+
| 19 | 19 | 38 | 57 | 76  | 95  | 114 | 133 |
+----+----+----+----+-----+-----+-----+-----+
| 20 | 20 | 40 | 60 | 80  | 100 | 120 | 140 |
+----+----+----+----+-----+-----+-----+-----+
| 21 | 21 | 42 | 63 | 84  | 105 | 126 | 147 |
+----+----+----+----+-----+-----+-----+-----+
| 22 | 22 | 44 | 66 | 88  | 110 | 132 | 154 |
+----+----+----+----+-----+-----+-----+-----+
| 23 | 23 | 46 | 69 | 92  | 115 | 138 | 161 |
+----+----+----+----+-----+-----+-----+-----+
| 24 | 24 | 48 | 72 | 96  | 120 | 144 | 168 |
+----+----+----+----+-----+-----+-----+-----+
| 25 | 25 | 50 | 75 | 100 | 125 | 150 | 175 |
+----+----+----+----+-----+-----+-----+-----+
| 26 | 26 | 52 | 78 | 104 | 130 | 156 | 182 |
+----+----+----+----+-----+-----+-----+-----+
";
                TestDocument document = new TestDocument(input);
                CsvToMarkdown module = new CsvToMarkdown().WithHeader();

                // When
                TestDocument result = await ExecuteAsync(document, module).SingleAsync();

                // Then
                result.Content.ShouldBe(output, StringCompareShould.IgnoreLineEndings);
            }
        }
    }
}