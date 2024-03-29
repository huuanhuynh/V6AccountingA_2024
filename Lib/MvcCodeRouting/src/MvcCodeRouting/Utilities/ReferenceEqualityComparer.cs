﻿// Copyright 2012 Max Toro Q.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcCodeRouting {

   class ReferenceEqualityComparer<T> : IEqualityComparer<T> {

      public static readonly IEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();

      public bool Equals(T x, T y) {
         return Object.ReferenceEquals(x, y);
      }

      public int GetHashCode(T obj) {
         return RuntimeHelpers.GetHashCode(obj);
      }
   }

   static class ReferenceEqualityComparerExtensions {

      public static IEnumerable<TSource> DistinctReference<TSource>(this IEnumerable<TSource> source) {
         return source.Distinct<TSource>(ReferenceEqualityComparer<TSource>.Instance);
      }
   }
}
