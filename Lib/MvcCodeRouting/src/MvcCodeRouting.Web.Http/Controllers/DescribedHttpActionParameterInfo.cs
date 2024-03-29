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
using System.Text;
using System.Web.Http.Controllers;

namespace MvcCodeRouting.Controllers {
   
   class DescribedHttpActionParameterInfo : ActionParameterInfo {

      readonly HttpParameterDescriptor paramDescr;
      readonly ReflectedHttpParameterDescriptor reflectedParamDescr;

      public override string Name {
         get { return paramDescr.ParameterName; }
      }

      public override Type Type {
         get { return paramDescr.ParameterType; }
      }

      public override bool IsOptional {
         get {
            return paramDescr.IsOptional
               || paramDescr.DefaultValue != null
               || IsNullableValueType;
         }
      }

      public DescribedHttpActionParameterInfo(HttpParameterDescriptor paramDescr, ActionInfo action) 
         : base(action) {

         this.paramDescr = paramDescr;
         this.reflectedParamDescr = this.paramDescr as ReflectedHttpParameterDescriptor;
      }

      // TODO: Remove ReflectedHttpParameterDescriptor.MethodInfo dependency

      public override object[] GetCustomAttributes(bool inherit) {
         return this.reflectedParamDescr.ParameterInfo.GetCustomAttributes(inherit);
      }

      public override object[] GetCustomAttributes(Type attributeType, bool inherit) {
         return this.reflectedParamDescr.ParameterInfo.GetCustomAttributes(attributeType, inherit);
      }

      public override bool IsDefined(Type attributeType, bool inherit) {
         return this.reflectedParamDescr.ParameterInfo.IsDefined(attributeType, inherit);
      }
   }
}
