using System;
using System.Diagnostics;
using System.Web;
using Practice.Extension;

namespace Practice.Data
{
    /// <summary>
    /// The query counter.
    /// </summary>
    public sealed class QueryCounter : IDisposable
    {
#if DEBUG

        /// <summary>
        /// Gets Count key.
        /// </summary>
        private const string NumQueries = "NumQueries";

        /// <summary>
        /// Gets Duration key.
        /// </summary>
        private const string TimeQueries = "TimeQueries";

        /// <summary>
        /// Gets Commands key.
        /// </summary>
        private const string CmdQueries = "CmdQueries";


        /// <summary>
        /// The _stop watch.
        /// </summary>
        private readonly Stopwatch _stopWatch = new Stopwatch();

        /// <summary>
        /// The _cmd.
        /// </summary>
        private string _cmd;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCounter"/> class.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        public QueryCounter(string sql)
        {
#if DEBUG
            _cmd = sql;

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[NumQueries] == null)
                {
                    HttpContext.Current.Items[NumQueries] = 1;
                }
                else
                {
                    HttpContext.Current.Items[NumQueries] = 1 + (int)HttpContext.Current.Items[NumQueries];
                }
            }

            _stopWatch.Start();
#endif
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
#if DEBUG
            _stopWatch.Stop();

            double duration = _stopWatch.ElapsedMilliseconds / 1000.0;

            _cmd = "{0}: {1:N3}".FormatWith(_cmd, duration);

            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[TimeQueries] == null)
                {
                    HttpContext.Current.Items[TimeQueries] = duration;
                }
                else
                {
                    HttpContext.Current.Items[TimeQueries] = duration +
                                                               (double)HttpContext.Current.Items[TimeQueries];
                }

                if (HttpContext.Current.Items[CmdQueries] == null)
                {
                    HttpContext.Current.Items[CmdQueries] = _cmd;
                }
                else
                {
                    HttpContext.Current.Items[CmdQueries] += "<br />" + _cmd;
                }
            }

#endif
        }

#if DEBUG

        /// <summary>
        /// The reset.
        /// </summary>
        public static void Reset()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items[NumQueries] = 0;
                HttpContext.Current.Items[TimeQueries] = (double)0;
                HttpContext.Current.Items[CmdQueries] = string.Empty;
            }
        }

        /// <summary>
        /// Gets Count.
        /// </summary>
        public static int Count
        {
            get { return (int)((HttpContext.Current == null) ? 0 : HttpContext.Current.Items[NumQueries]); }
        }

        /// <summary>
        /// Gets Duration.
        /// </summary>
        public static double Duration
        {
            get { return (double)((HttpContext.Current == null) ? 0.0 : HttpContext.Current.Items[TimeQueries]); }
        }

        /// <summary>
        /// Gets Commands.
        /// </summary>
        public static string Commands
        {
            get { return (string)((HttpContext.Current == null) ? string.Empty : HttpContext.Current.Items[CmdQueries]); }
        }
#endif
    }
}