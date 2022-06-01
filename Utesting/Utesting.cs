using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using UtestingTools;

namespace Utesting
{
    public class Test
    {
        public List<dynamic> Groups {get; set;} = new List<dynamic>();
        
        // Output

        private void _OutputLine(string chr, Action<string?> output)
        {
            output(String.Concat(Enumerable.Repeat(chr, 100)));
        }

        private void _OutputUnit(string name, Action<string?> output)
        {
            output($"UNIT: {name}");
        }

        private void _OutputCase(int ind, string arguments, string correctAnswer, string returnedAnswer, string result, long time, Action<string?> output)
        {
            output($"CASE {ind}:");
            output($"   Arguments: {arguments}");
            output($"   Correct answer: {correctAnswer}");
            output($"   Returned answer: {returnedAnswer}");
            output($"   Result: {result}");
            output($"   Time: {time} ms");
        }

        private void _OutputCaseOnly(int ind, string text, Action<string?> output)
        {
            output($"CASE {ind}: {text}");
        }
        
        // TestOne

        public List<dynamic> TestOne<TAnsw>(TestGroup<TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!();
                        timer.Stop();
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!();
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!();
                    timer.Stop();
                    TestCaseResult<TAnsw> result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!();
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    Console.WriteLine(timer.ElapsedMilliseconds);
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"—", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, TAnsw>(TestGroup<T1, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!);
                        timer.Stop();
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!);
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!);
                    timer.Stop();
                    TestCaseResult<T1, TAnsw> result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!);
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, TAnsw>(TestGroup<T1, T2, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!);
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!);
                    timer.Stop();
                    TestCaseResult<T1, T2, TAnsw> result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!);
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, TAnsw>(TestGroup<T1, T2, T3, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!);
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, TAnsw> result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!);
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, TAnsw>(TestGroup<T1, T2, T3, T4, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!);
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!);
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, TAnsw>(TestGroup<T1, T2, T3, T4, T5, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }
        
        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}, {result.Argument12}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}, {result.Argument12}, {result.Argument13}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}, {result.Argument12}, {result.Argument13}, {result.Argument14}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}, {result.Argument12}, {result.Argument13}, {result.Argument14}, {result.Argument15}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        public List<dynamic> TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> group, Action<string?> output)
        {
            List<dynamic> results = new List<dynamic>();
            if (!group!.NoPrint)
            {
                this._OutputLine("*", output);
                this._OutputUnit(group.TUnit.Name, output);
            }
            if (group.OnlyErrors && group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!, tCase.Argument16!);
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        timer.Stop();
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} ), TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyErrors)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> result;
                    try
                    {
                        TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!, tCase.Argument16!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, 0);
                    }
                    catch(Exception err)
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {err.Message} )"), 0);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else if (group.OnlyTime)
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TAnsw returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!, tCase.Argument16!);
                    timer.Stop();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCaseOnly(ind, result.Result.Text, output);
                    }
                    ind ++;
                }
            }
            else
            {
                int ind = 0;
                foreach (var tCase in group.TCases)
                {
                    if (!group!.NoPrint)
                    {
                        this._OutputLine("-", output);
                    }
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> result;
                    TAnsw? returned = default(TAnsw);
                    Exception? error = default(Exception);
                    try
                    {
                        returned = group.TUnit.Func!(tCase.Argument1!, tCase.Argument2!, tCase.Argument3!, tCase.Argument4!, tCase.Argument5!, tCase.Argument6!, tCase.Argument7!, tCase.Argument8!, tCase.Argument9!, tCase.Argument10!, tCase.Argument11!, tCase.Argument12!, tCase.Argument13!, tCase.Argument14!, tCase.Argument15!, tCase.Argument16!);
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(true, $"PASSED, TIME ( {timer.ElapsedMilliseconds} ms )"), timer.ElapsedMilliseconds);
                    }
                    catch(Exception err)
                    {
                        error = err;
                    }
                    timer.Stop();
                    if (error != default(Exception))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), new ResultState(false, $"ERROR ( {error.Message} )"), timer.ElapsedMilliseconds);
                    }
                    else if (returned!.Equals(tCase.Answer))
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Passed, timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        result = new TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(group.TUnit, tCase, default(TAnsw), ResultStates.Failed, timer.ElapsedMilliseconds);
                    }
                    results.Add(result);
                    if (!group!.NoPrint)
                    {
                        this._OutputCase(ind, $"{result.Argument1}, {result.Argument2}, {result.Argument3}, {result.Argument4}, {result.Argument5}, {result.Argument6}, {result.Argument7}, {result.Argument8}, {result.Argument9}, {result.Argument10}, {result.Argument11}, {result.Argument12}, {result.Argument13}, {result.Argument14}, {result.Argument15}, {result.Argument16}", $"{result.Answer}", $"{returned}", result.Result.Text, timer.ElapsedMilliseconds, output);
                    }
                    ind ++;
                }
            }
            if (!group!.NoPrint)
            {
                this._OutputLine("-", output);
            }
            return results;
        }

        // AddTestUnit

        public void AddTestUnit<TAnsw>(Unit<TAnsw> tUnit, TestCase<TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<TAnsw>(this.TestOne<TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, TAnsw>(Unit<T1, TAnsw> tUnit, TestCase<T1, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, TAnsw>(this.TestOne<T1, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, TAnsw>(Unit<T1, T2, TAnsw> tUnit, TestCase<T1, T2, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, TAnsw>(this.TestOne<T1, T2, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, TAnsw>(Unit<T1, T2, T3, TAnsw> tUnit, TestCase<T1, T2, T3, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, TAnsw>(this.TestOne<T1, T2, T3, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, TAnsw>(Unit<T1, T2, T3, T4, TAnsw> tUnit, TestCase<T1, T2, T3, T4, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, TAnsw>(this.TestOne<T1, T2, T3, T4, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, TAnsw>(Unit<T1, T2, T3, T4, T5, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }
        
        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }
        
        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        public void AddTestUnit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> tUnit, TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>[] tCases, bool onlyErrors = false, bool onlyTime = false, bool noPrint = false)
        {
            this.Groups.Add(new TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>(this.TestOne<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>, tUnit, tCases, onlyErrors, onlyTime, noPrint));
        }

        // TestAll

        public Dictionary<string, List<dynamic>> TestAll(string fileName = "")
        {
            Dictionary<string, List<dynamic>> results = new Dictionary<string, List<dynamic>>();
            Action<string?> output = Console.WriteLine;
            StreamWriter? file = default(StreamWriter);
            if (fileName != "")
            {
                file = new StreamWriter(fileName);
                output = file.WriteLine;
            }
            bool allPassed = true;
            foreach (var group in this.Groups)
            {
                List<dynamic> result = group.TCall(group, output);
                foreach(var res in result)
                {
                    allPassed &= res.Result.Result;
                }
                results.Add(group.TUnit.Name, result);
            }
            this._OutputLine("*", output);
            if (allPassed)
            {
                output("ALL CASES PASSED :)");
            }
            else
            {
                output("NOT ALL TESTS PASSED :(");
            }
            this._OutputLine("*", output);
            if (fileName != "")
            {
                file!.Close();
            }
            return results;
        }
    }
}