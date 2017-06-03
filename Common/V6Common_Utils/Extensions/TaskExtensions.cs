using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace V6Soft.Common.Utils.TaskExtensions
{
    /// <summary>
    ///     Adds more helper methods to Task class to make async usage easier
    ///     Extract those utilities from:
    ///     https://github.com/SignalR/SignalR/blob/master/src/Microsoft.AspNet.SignalR.Core/TaskAsyncHelper.cs
    /// </summary>
    public static class TaskExtensions
    {
        private static readonly Task _emptyTask = MakeTask<object>(null);

        public static Task Empty
        {
            get
            {
                return _emptyTask;
            }
        }


        #region Static methods

        private static Task<T> MakeTask<T>(T value)
        {
            return FromResult<T>(value);
        }

        private static Task Canceled()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetCanceled();
            return tcs.Task;
        }

        private static Task<T> Canceled<T>()
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetCanceled();
            return tcs.Task;
        }

        private static Task RunTask(Task task, Action successor)
        {
            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    tcs.SetUnwrappedException(t.Exception);
                }
                else if (t.IsCanceled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    try
                    {
                        successor();
                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetUnwrappedException(ex);
                    }
                }
            });

            return tcs.Task;
        }

        private static void ExecuteOnFaulted(Action<AggregateException, object> handler, object state, AggregateException exception)
        {
            handler(exception, state);
        }

        private static void AttachFaultedContinuation<TTask>(TTask task, Action<AggregateException, object> handler, object state) where TTask : Task
        {
            task.ContinueWith(innerTask =>
            {
                ExecuteOnFaulted(handler, state, innerTask.Exception);
            },
            TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
        }

        #endregion

        
        #region Catch extensions

        public static TTask Catch<TTask>(this TTask task) where TTask : Task
        {
            return Catch(task, ex => { });
        }

        public static TTask Catch<TTask>(this TTask task, Action<AggregateException> handler) where TTask : Task
        {
            return task.Catch((ex, state) => ((Action<AggregateException>)state).Invoke(ex), handler);
        }

        public static TTask Catch<TTask>(this TTask task, Action<AggregateException, object> handler, object state) where TTask : Task
        {
            if (task != null && task.Status != TaskStatus.RanToCompletion)
            {
                if (task.Status == TaskStatus.Faulted)
                {
                    ExecuteOnFaulted(handler, state, task.Exception);
                }
                else
                {
                    AttachFaultedContinuation<TTask>(task, handler, state);
                }
            }

            return task;
        }

        #endregion


        #region Then extesions

        public static Task Then(this Task task, Action successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);

                default:
                    return RunTask(task, successor);
            }
        }

        public static Task<TResult> Then<TResult>(this Task task, Func<TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);

                case TaskStatus.Canceled:
                    return Canceled<TResult>();

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);

                default:
                    return TaskRunners<object, TResult>.RunTask(task, successor);
            }
        }

        public static Task Then(this Task[] tasks, Action successor)
        {
            if (tasks.Length == 0)
            {
                return FromMethod(successor);
            }

            var tcs = new TaskCompletionSource<object>();
            Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                var faulted = completedTasks.FirstOrDefault(t => t.IsFaulted);
                if (faulted != null)
                {
                    tcs.SetUnwrappedException(faulted.Exception);
                    return;
                }
                var cancelled = completedTasks.FirstOrDefault(t => t.IsCanceled);
                if (cancelled != null)
                {
                    tcs.SetCanceled();
                    return;
                }

                successor();
                tcs.SetResult(null);
            });

            return tcs.Task;
        }

        public static Task<T> Then<T>(this IEnumerable<Task> tasks, Func<T> successor)
        {
            if (tasks.Count() == 0)
            {
                return FromMethod(successor);
            }

            var tcs = new TaskCompletionSource<T>();
            Task.Factory.ContinueWhenAll(tasks.ToArray(), completedTasks =>
            {
                var faulted = completedTasks.FirstOrDefault(t => t.IsFaulted);
                if (faulted != null)
                {
                    tcs.SetUnwrappedException(faulted.Exception);
                    return;
                }
                var cancelled = completedTasks.FirstOrDefault(t => t.IsCanceled);
                if (cancelled != null)
                {
                    tcs.SetCanceled();
                    return;
                }

                tcs.SetResult(successor());
            });

            return tcs.Task;
        }
        
        public static Task Then<T1>(this Task task, Action<T1> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1);

                default:
                    return GenericDelegates<object, object, T1, object>.ThenWithArgs(task, successor, arg1);
            }
        }

        public static Task Then<T1, T2>(this Task task, Action<T1, T2> successor, T1 arg1, T2 arg2)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1, arg2);

                default:
                    return GenericDelegates<object, object, T1, T2>.ThenWithArgs(task, successor, arg1, arg2);
            }
        }

        public static Task Then<T1>(this Task task, Func<T1, Task> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1);

                default:
                    return GenericDelegates<object, Task, T1, object>.ThenWithArgs(task, successor, arg1)
                                                                     .FastUnwrap();
            }
        }

        public static Task Then<T1, T2>(this Task task, Func<T1, T2, Task> successor, T1 arg1, T2 arg2)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, arg1, arg2);

                default:
                    return GenericDelegates<object, Task, T1, T2>.ThenWithArgs(task, successor, arg1, arg2)
                                                                 .FastUnwrap();
            }
        }

        public static Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, Task<TResult>> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);

                case TaskStatus.Canceled:
                    return Canceled<TResult>();

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);

                default:
                    return TaskRunners<T, Task<TResult>>.RunTask(task, t => successor(t.Result))
                                                        .FastUnwrap();
            }
        }

        public static Task<TResult> Then<T, TResult>(this Task<T> task, Func<T, TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);

                case TaskStatus.Canceled:
                    return Canceled<TResult>();

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);

                default:
                    return TaskRunners<T, TResult>.RunTask(task, t => successor(t.Result));
            }
        }

        public static Task<TResult> Then<T, T1, TResult>(this Task<T> task, Func<T, T1, TResult> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);

                case TaskStatus.Canceled:
                    return Canceled<TResult>();

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result, arg1);

                default:
                    return GenericDelegates<T, TResult, T1, object>.ThenWithArgs(task, successor, arg1);
            }
        }

        public static Task Then(this Task task, Func<Task> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);

                default:
                    return TaskRunners<object, Task>.RunTask(task, successor)
                                                    .FastUnwrap();
            }
        }

        public static Task<TResult> Then<TResult>(this Task task, Func<Task<TResult>> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return FromError<TResult>(task.Exception);

                case TaskStatus.Canceled:
                    return Canceled<TResult>();

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);

                default:
                    return TaskRunners<object, Task<TResult>>.RunTask(task, successor)
                                                             .FastUnwrap();
            }
        }

        public static Task Then<TResult>(this Task<TResult> task, Action<TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);

                default:
                    return TaskRunners<TResult, object>.RunTask(task, successor);
            }
        }

        public static Task Then<TResult>(this Task<TResult> task, Func<TResult, Task> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task.Result);

                default:
                    return TaskRunners<TResult, Task>.RunTask(task, t => successor(t.Result))
                                                     .FastUnwrap();
            }
        }

        public static Task<TResult> Then<TResult, T1>(this Task<TResult> task, Func<Task<TResult>, T1, Task<TResult>> successor, T1 arg1)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task, arg1);

                default:
                    return GenericDelegates<TResult, Task<TResult>, T1, object>.ThenWithArgs(task, successor, arg1)
                                                                               .FastUnwrap();
            }
        }

        public static Task Then(this Task task, Action<Task> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task);

                default:
                    return TaskRunners<object, object>.RunTask(task, () => successor(task));
            }
        }

        public static Task<TResult> Then<TResult>(this Task<TResult> task, Func<Task<TResult>, TResult> successor)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor, task);

                default:
                    return TaskRunners<TResult, TResult>.RunTask(task, (Task<TResult> t) => successor(task));
            }
        }

        #endregion


        #region FromMethod extensions

        public static Task FromMethod(Action func)
        {
            try
            {
                func();
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task FromMethod<T1>(Action<T1> func, T1 arg)
        {
            try
            {
                func(arg);
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task FromMethod<T1, T2>(Action<T1, T2> func, T1 arg1, T2 arg2)
        {
            try
            {
                func(arg1, arg2);
                return Empty;
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task FromMethod(Func<Task> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task<TResult> FromMethod<TResult>(Func<Task<TResult>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task<TResult> FromMethod<TResult>(Func<TResult> func)
        {
            try
            {
                return FromResult<TResult>(func());
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task FromMethod<T1>(Func<T1, Task> func, T1 arg)
        {
            try
            {
                return func(arg);
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task FromMethod<T1, T2>(Func<T1, T2, Task> func, T1 arg1, T2 arg2)
        {
            try
            {
                return func(arg1, arg2);
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task<TResult> FromMethod<T1, TResult>(Func<T1, Task<TResult>> func, T1 arg)
        {
            try
            {
                return func(arg);
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task<TResult> FromMethod<T1, TResult>(Func<T1, TResult> func, T1 arg)
        {
            try
            {
                return FromResult<TResult>(func(arg));
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task<TResult> FromMethod<T1, T2, TResult>(Func<T1, T2, Task<TResult>> func, T1 arg1, T2 arg2)
        {
            try
            {
                return func(arg1, arg2);
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task<TResult> FromMethod<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
        {
            try
            {
                return FromResult<TResult>(func(arg1, arg2));
            }
            catch (Exception ex)
            {
                return FromError<TResult>(ex);
            }
        }

        public static Task<T> FromResult<T>(T value)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetResult(value);
            return tcs.Task;
        }

        #endregion

        
        public static Task Return(this Task[] tasks)
        {
            return Then(tasks, () => { });
        }

        public static Task Finally(this Task task, Action<object> next, object state = null)
        {
            try
            {
                switch (task.Status)
                {
                    case TaskStatus.Faulted:
                    case TaskStatus.Canceled:
                        next(state);
                        return task;
                    case TaskStatus.RanToCompletion:
                        return FromMethod(next, state);

                    default:
                        return RunTaskSynchronously(task, next, state, onlyOnSuccess: false);
                }
            }
            catch (Exception ex)
            {
                return FromError(ex);
            }
        }

        public static Task RunSynchronously(this Task task, Action successor, bool onlyOnSuccess = true)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                case TaskStatus.Canceled:
                    return task;

                case TaskStatus.RanToCompletion:
                    return FromMethod(successor);

                default:
                    return RunTaskSynchronously(task, state => ((Action)state).Invoke(), successor, onlyOnSuccess);
            }
        }

        public static Task Delay(TimeSpan timeOut)
        {
            var tcs = new TaskCompletionSource<object>();

            var timer = new Timer(tcs.SetResult,
            null,
            timeOut,
            TimeSpan.FromMilliseconds(-1));

            return tcs.Task.ContinueWith(_ =>
            {
                timer.Dispose();
            },
            TaskContinuationOptions.ExecuteSynchronously);
        }

        public static Task FastUnwrap(this Task<Task> task)
        {
            var innerTask = (task.Status == TaskStatus.RanToCompletion) ? task.Result : null;
            return innerTask ?? task.Unwrap();
        }

        public static Task<T> FastUnwrap<T>(this Task<Task<T>> task)
        {
            var innerTask = (task.Status == TaskStatus.RanToCompletion) ? task.Result : null;
            return innerTask ?? task.Unwrap();
        }

        public static Task<T> ContinueWithResultOrBubleException<T, T1>(this Task<T1> task, Func<T1, T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            task.ContinueWith(antecdent =>
            {
                switch (antecdent.Status)
                {
                    case TaskStatus.Canceled:
                        tcs.SetCanceled();
                        break;
                    case TaskStatus.Faulted:
                        tcs.SetException(antecdent.Exception.InnerExceptions);
                        break;
                    case TaskStatus.RanToCompletion:
                        tcs.SetResult(func(antecdent.Result));
                        break;
                    default:
                        break;
                }
            });
            return tcs.Task;
        }

        internal static Task FromError(Exception e)
        {
            return FromError<object>(e);
        }

        internal static Task<T> FromError<T>(Exception e)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetUnwrappedException<T>(e);
            return tcs.Task;
        }

        internal static void SetUnwrappedException<T>(this TaskCompletionSource<T> tcs, Exception e)
        {
            var aggregateException = e as AggregateException;
            if (aggregateException != null)
            {
                tcs.SetException(aggregateException.InnerExceptions);
            }
            else
            {
                tcs.SetException(e);
            }
        }

        private static Task RunTaskSynchronously(Task task, Action<object> next, object state, bool onlyOnSuccess = true)
        {
            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(t =>
            {
                try
                {
                    if (t.IsFaulted)
                    {
                        if (!onlyOnSuccess)
                        {
                            next(state);
                        }

                        tcs.SetUnwrappedException(t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        if (!onlyOnSuccess)
                        {
                            next(state);
                        }

                        tcs.SetCanceled();
                    }
                    else
                    {
                        next(state);
                        tcs.SetResult(null);
                    }
                }
                catch (Exception ex)
                {
                    tcs.SetUnwrappedException(ex);
                }
            },
            TaskContinuationOptions.ExecuteSynchronously);

            return tcs.Task;
        }

        private static class TaskRunners<T, TResult>
        {
            internal static Task RunTask(Task<T> task, Action<T> successor)
            {
                var tcs = new TaskCompletionSource<object>();
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.SetUnwrappedException(t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            successor(t.Result);
                            tcs.SetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.SetUnwrappedException(ex);
                        }
                    }
                });

                return tcs.Task;
            }

            internal static Task RunTask(Task task, Action successor)
            {
                var tcs = new TaskCompletionSource<object>();
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.SetUnwrappedException(t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            successor();
                            tcs.SetResult(null);
                        }
                        catch (Exception ex)
                        {
                            tcs.SetUnwrappedException(ex);
                        }
                    }
                });

                return tcs.Task;
            }

            internal static Task<TResult> RunTask(Task task, Func<TResult> successor)
            {
                var tcs = new TaskCompletionSource<TResult>();
                task.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        tcs.SetUnwrappedException(t.Exception);
                    }
                    else if (t.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            tcs.SetResult(successor());
                        }
                        catch (Exception ex)
                        {
                            tcs.SetUnwrappedException(ex);
                        }
                    }
                });

                return tcs.Task;
            }

            internal static Task<TResult> RunTask(Task<T> task, Func<Task<T>, TResult> successor)
            {
                var tcs = new TaskCompletionSource<TResult>();
                task.ContinueWith(t =>
                {
                    if (task.IsFaulted)
                    {
                        tcs.SetUnwrappedException(t.Exception);
                    }
                    else if (task.IsCanceled)
                    {
                        tcs.SetCanceled();
                    }
                    else
                    {
                        try
                        {
                            tcs.SetResult(successor(t));
                        }
                        catch (Exception ex)
                        {
                            tcs.SetUnwrappedException(ex);
                        }
                    }
                });

                return tcs.Task;
            }
        }

        private static class GenericDelegates<T, TResult, T1, T2>
        {
            internal static Task ThenWithArgs(Task task, Action<T1> successor, T1 arg1)
            {
                return RunTask(task, () => successor(arg1));
            }

            internal static Task ThenWithArgs(Task task, Action<T1, T2> successor, T1 arg1, T2 arg2)
            {
                return RunTask(task, () => successor(arg1, arg2));
            }

            internal static Task<TResult> ThenWithArgs(Task task, Func<T1, TResult> successor, T1 arg1)
            {
                return TaskRunners<object, TResult>.RunTask(task, () => successor(arg1));
            }

            internal static Task<TResult> ThenWithArgs(Task task, Func<T1, T2, TResult> successor, T1 arg1, T2 arg2)
            {
                return TaskRunners<object, TResult>.RunTask(task, () => successor(arg1, arg2));
            }

            internal static Task<TResult> ThenWithArgs(Task<T> task, Func<T, T1, TResult> successor, T1 arg1)
            {
                return TaskRunners<T, TResult>.RunTask(task, t => successor(t.Result, arg1));
            }

            internal static Task<Task> ThenWithArgs(Task task, Func<T1, Task> successor, T1 arg1)
            {
                return TaskRunners<object, Task>.RunTask(task, () => successor(arg1));
            }

            internal static Task<Task> ThenWithArgs(Task task, Func<T1, T2, Task> successor, T1 arg1, T2 arg2)
            {
                return TaskRunners<object, Task>.RunTask(task, () => successor(arg1, arg2));
            }

            internal static Task<Task<TResult>> ThenWithArgs(Task<T> task, Func<T, T1, Task<TResult>> successor, T1 arg1)
            {
                return TaskRunners<T, Task<TResult>>.RunTask(task, t => successor(t.Result, arg1));
            }

            internal static Task<Task<T>> ThenWithArgs(Task<T> task, Func<Task<T>, T1, Task<T>> successor, T1 arg1)
            {
                return TaskRunners<T, Task<T>>.RunTask(task, t => successor(t, arg1));
            }
        }
    }
}

