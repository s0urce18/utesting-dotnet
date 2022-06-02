# utesting-dotnet

.NET library for unit testing

## Installation:

Download `.dll` files from `DLLs` folder or download `DLLs.zip`

## Importing:

1. Add `.dll` files to your project

2. Add to your document:
    ```cs
    using UtestingTools;
    using Utesting;
    ```

## Using:

`TestOne<T1, T2, ..., TAnsw>(TestGroup<T1, T2, ..., TAnsw> group, Action<string?> output)` — test one test group

`AddTestUnit<T1, T2, ..., TAnsw>(Unit<T1, T2, ..., TAnsw> tUnit, TestCase<T1, T2, ..., TAnsw>[] tCases, bool onlyErrors, bool onlyTime, bool noPrint)` — add test unit to testing it in future

`TestAll(string fileName)` — running testing all added units
