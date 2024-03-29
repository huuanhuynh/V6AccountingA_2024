﻿// Copyright 2011 Max Toro Q.
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
using System.Text;

namespace MvcCodeRouting {
   
   /// <summary>
   /// Provides data for custom route formatting.
   /// </summary>
   /// <seealso cref="CodeRoutingSettings.RouteFormatter"/>
   public class RouteFormatterArgs {

      /// <summary>
      /// Gets the original segment.
      /// </summary>
      public string OriginalSegment { get; private set; }

      /// <summary>
      /// Gets the segment type.
      /// </summary>
      public RouteSegmentType SegmentType { get; private set; }

      /// <summary>
      /// Gets the controller type.
      /// </summary>
      public Type ControllerType { get; private set; }

      internal RouteFormatterArgs(string originalSegment, RouteSegmentType segmentType, Type controllerType) {

         this.OriginalSegment = originalSegment;
         this.SegmentType = segmentType;
         this.ControllerType = controllerType;
      }
   }
}
