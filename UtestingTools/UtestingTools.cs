using System;
using System.Collections;
using System.Collections.Generic;

namespace UtestingTools
{
    public class ResultState
    {
        public bool Result {get; set;}
        public string Text {get; set;}

        public ResultState(bool result, string text)
        {
            this.Result = result;
            this.Text = text;
        }

    }

    public class ResultStates
    {
        public static ResultState Passed {get;} = new ResultState(true, "PASSED");
        public static ResultState Failed {get;} = new ResultState(false, "FAILED");
        public static ResultState Error {get;} = new ResultState(false, "ERROR");
        public static ResultState Time {get;} = new ResultState(true, "TIME");
    }

    // 0 arguments

    public class Unit<TAnsw>
    {
        private Func<TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<TAnsw> func)
        {
            this.Func = func;
        }
    }

    public class TestCase<TAnsw>
    {
        public TAnsw Answer {get; set;}

        public TestCase(TAnsw answer)
        {
            this.Answer = answer;
        }
    }

    public class TestGroup<TAnsw>
    {
        public Func<TestGroup<TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<TAnsw> TUnit {get; set;}
        public TestCase<TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<TAnsw> tUnit, 
                         TestCase<TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<TAnsw> : TestCase<TAnsw>
    {
        public Unit<TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        public long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<TAnsw> tUnit, 
                              TestCase<TAnsw> tCase, 
                              TAnsw? returned,
                              ResultState result, 
                              long workTime) : base(tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }
    }

    // 1 argument

    public class Unit<T1, TAnsw>
    {
        private Func<T1, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, TAnsw>
    {
        public Func<TestGroup<T1, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, TAnsw> TUnit {get; set;}
        public TestCase<T1, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, TAnsw> tUnit, 
                         TestCase<T1, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, TAnsw> : TestCase<T1, TAnsw>
    {
        public Unit<T1, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, TAnsw> tUnit, 
                              TestCase<T1, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 2 arguments

    public class Unit<T1, T2, TAnsw>
    {
        private Func<T1, T2, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, TAnsw>
    {
        public Func<TestGroup<T1, T2, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, TAnsw> tUnit, 
                         TestCase<T1, T2, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, TAnsw> : TestCase<T1, T2, TAnsw>
    {
        public Unit<T1, T2, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, TAnsw> tUnit, 
                              TestCase<T1, T2, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 3 arguments

    public class Unit<T1, T2, T3, TAnsw>
    {
        private Func<T1, T2, T3, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, TAnsw> : TestCase<T1, T2, T3, TAnsw>
    {
        public Unit<T1, T2, T3, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2,
                                                    tCase.Argument3, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 4 arguments

    public class Unit<T1, T2, T3, T4, TAnsw>
    {
        private Func<T1, T2, T3, T4, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3, 
                        T4? argument4, 
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, TAnsw> : TestCase<T1, T2, T3, T4, TAnsw>
    {
        public Unit<T1, T2, T3, T4, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2,
                                                    tCase.Argument3, 
                                                    tCase.Argument4, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 5 arguments

    public class Unit<T1, T2, T3, T4, T5, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3, 
                        T4? argument4, 
                        T5? argument5, 
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, TAnsw> : TestCase<T1, T2, T3, T4, T5, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2,
                                                    tCase.Argument3, 
                                                    tCase.Argument4, 
                                                    tCase.Argument5, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 6 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3, 
                        T4? argument4, 
                        T5? argument5, 
                        T6? argument6, 
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2,
                                                    tCase.Argument3, 
                                                    tCase.Argument4, 
                                                    tCase.Argument5, 
                                                    tCase.Argument6, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 7 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3, 
                        T4? argument4, 
                        T5? argument5, 
                        T6? argument6, 
                        T7? argument7, 
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1, 
                                                    tCase.Argument2,
                                                    tCase.Argument3, 
                                                    tCase.Argument4, 
                                                    tCase.Argument5, 
                                                    tCase.Argument6, 
                                                    tCase.Argument7, 
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 8 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }
    
    // 9 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 10 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 11 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 12 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public T12? Argument12 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        T12? argument12,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Argument12 = argument12;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Argument12 = (T12?)(arguments[11]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Argument12,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 13 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public T12? Argument12 {get; set;}
        public T13? Argument13 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        T12? argument12,
                        T13? argument13,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Argument12 = argument12;
            this.Argument13 = argument13;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Argument12 = (T12?)(arguments[11]);
            this.Argument13 = (T13?)(arguments[12]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Argument12,
                                                    tCase.Argument13,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 14 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public T12? Argument12 {get; set;}
        public T13? Argument13 {get; set;}
        public T14? Argument14 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        T12? argument12,
                        T13? argument13,
                        T14? argument14,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Argument12 = argument12;
            this.Argument13 = argument13;
            this.Argument14 = argument14;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Argument12 = (T12?)(arguments[11]);
            this.Argument13 = (T13?)(arguments[12]);
            this.Argument14 = (T14?)(arguments[13]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Argument12,
                                                    tCase.Argument13,
                                                    tCase.Argument14,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }
    
    // 15 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public T12? Argument12 {get; set;}
        public T13? Argument13 {get; set;}
        public T14? Argument14 {get; set;}
        public T15? Argument15 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        T12? argument12,
                        T13? argument13,
                        T14? argument14,
                        T15? argument15,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Argument12 = argument12;
            this.Argument13 = argument13;
            this.Argument14 = argument14;
            this.Argument15 = argument15;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Argument12 = (T12?)(arguments[11]);
            this.Argument13 = (T13?)(arguments[12]);
            this.Argument14 = (T14?)(arguments[13]);
            this.Argument15 = (T15?)(arguments[14]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Argument12,
                                                    tCase.Argument13,
                                                    tCase.Argument14,
                                                    tCase.Argument15,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }

    // 16 arguments

    public class Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>
    {
        private Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>? _func {get; set;} = null; 
        public string Name {get; set;} = "";
        public Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>? Func 
        {
            get
            {
                return this._func;
            } 
            set
            {
                this._func = value;
                this.Name = value != null ? value.Method.Name : "";
            }
        }
        
        public Unit(Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> func)
        {
            this.Func = func;
        }
    }
    
    public class TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>
    {
        public T1? Argument1 {get; set;}
        public T2? Argument2 {get; set;}
        public T3? Argument3 {get; set;}
        public T4? Argument4 {get; set;}
        public T5? Argument5 {get; set;}
        public T6? Argument6 {get; set;}
        public T7? Argument7 {get; set;}
        public T8? Argument8 {get; set;}
        public T9? Argument9 {get; set;}
        public T10? Argument10 {get; set;}
        public T11? Argument11 {get; set;}
        public T12? Argument12 {get; set;}
        public T13? Argument13 {get; set;}
        public T14? Argument14 {get; set;}
        public T15? Argument15 {get; set;}
        public T16? Argument16 {get; set;}
        public TAnsw Answer {get; set;}

        public TestCase(T1? argument1,
                        T2? argument2,
                        T3? argument3,
                        T4? argument4,
                        T5? argument5,
                        T6? argument6,
                        T7? argument7,
                        T8? argument8,
                        T9? argument9,
                        T10? argument10,
                        T11? argument11,
                        T12? argument12,
                        T13? argument13,
                        T14? argument14,
                        T15? argument15,
                        T16? argument16,
                        TAnsw answer)
        {
            this.Argument1 = argument1;
            this.Argument2 = argument2;
            this.Argument3 = argument3;
            this.Argument4 = argument4;
            this.Argument5 = argument5;
            this.Argument6 = argument6;
            this.Argument7 = argument7;
            this.Argument8 = argument8;
            this.Argument9 = argument9;
            this.Argument10 = argument10;
            this.Argument11 = argument11;
            this.Argument12 = argument12;
            this.Argument13 = argument13;
            this.Argument14 = argument14;
            this.Argument15 = argument15;
            this.Argument16 = argument16;
            this.Answer = answer;
        }

        public TestCase(ArrayList arguments, TAnsw answer)
        {
            this.Argument1 = (T1?)(arguments[0]);
            this.Argument2 = (T2?)(arguments[1]);
            this.Argument3 = (T3?)(arguments[2]);
            this.Argument4 = (T4?)(arguments[3]);
            this.Argument5 = (T5?)(arguments[4]);
            this.Argument6 = (T6?)(arguments[5]);
            this.Argument7 = (T7?)(arguments[6]);
            this.Argument8 = (T8?)(arguments[7]);
            this.Argument9 = (T9?)(arguments[8]);
            this.Argument10 = (T10?)(arguments[9]);
            this.Argument11 = (T11?)(arguments[10]);
            this.Argument12 = (T12?)(arguments[11]);
            this.Argument13 = (T13?)(arguments[12]);
            this.Argument14 = (T14?)(arguments[13]);
            this.Argument15 = (T15?)(arguments[14]);
            this.Argument16 = (T16?)(arguments[15]);
            this.Answer = answer;
        }
    }

    public class TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>
    {
        public Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>, Action<string?>, List<dynamic>> TCall {get; set;}
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> TUnit {get; set;}
        public TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>[] TCases {get; set;}
        public bool OnlyErrors {get; set;}
        public bool OnlyTime {get; set;}
        public bool NoPrint {get; set;}

        public TestGroup(Func<TestGroup<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>, Action<string?>, List<dynamic>> tCall,
                         Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> tUnit, 
                         TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>[] tCases, 
                         bool onlyErrors = false, 
                         bool onlyTime = false,
                         bool noPrint = false)
        {
            this.TCall = tCall;
            this.TUnit = tUnit;
            this.TCases = tCases;
            this.OnlyErrors = onlyErrors;
            this.OnlyTime = onlyTime;
            this.NoPrint = noPrint;
        }
    }

    public class TestCaseResult<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> : TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw>
    {
        public Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> TUnit {get; set;}
        public TAnsw? Returned {get; set;}
        long WorkTime {get; set;}
        public ResultState Result {get; set;}

        public TestCaseResult(Unit<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> tUnit, 
                              TestCase<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TAnsw> tCase, 
                              TAnsw? returned, 
                              ResultState result,
                              long workTime) : base(tCase.Argument1,
                                                    tCase.Argument2,
                                                    tCase.Argument3,
                                                    tCase.Argument4,
                                                    tCase.Argument5,
                                                    tCase.Argument6,
                                                    tCase.Argument7,
                                                    tCase.Argument8,
                                                    tCase.Argument9,
                                                    tCase.Argument10,
                                                    tCase.Argument11,
                                                    tCase.Argument12,
                                                    tCase.Argument13,
                                                    tCase.Argument14,
                                                    tCase.Argument15,
                                                    tCase.Argument16,
                                                    tCase.Answer)
        {
            this.TUnit = tUnit;
            this.Returned = returned;
            this.Result = result;
            this.WorkTime = workTime;
        }        
    }
}