//
// AsyncLock.cs
//
// Author:
//       Bart King <bart@cleardemon.com>
//
// Copyright (c) 2016 Bart King
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

/* Wholly based on code from http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266988.aspx */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace ClearDemon.Utility
{
	/// <summary>
	/// Thread-locking mechanism that works in async/await callbacks.
	/// </summary>
	/// <remarks>Wholly based on code from http://blogs.msdn.com/b/pfxteam/archive/2012/02/12/10266988.aspx</remarks>
	/// <example>
	/// var locker = new AsyncLock();
	/// using(await locker.LockAsync())
	/// {
	/// // do something
	/// }
	/// </example>
	public class AsyncLock
	{
		public struct Releaser : IDisposable
		{
			readonly AsyncLock _toRelease;

			internal Releaser(AsyncLock l)
			{
				_toRelease = l;
			}

			public void Dispose()
			{
				if(_toRelease != null)
					_toRelease._semaphore.Release();
			}
		}

		readonly SemaphoreSlim _semaphore;
		readonly Task<Releaser> _releaser;

		public AsyncLock()
		{
			_semaphore = new SemaphoreSlim(1);
			_releaser = Task.FromResult(new Releaser(this));
		}

		public Task<Releaser> LockAsync()
		{
			var wait = _semaphore.WaitAsync();
			return wait.IsCompleted ? _releaser : wait.ContinueWith((_, state) => new Releaser((AsyncLock)state), this, CancellationToken.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}
	}
}

