﻿// Copyright 2012-2013 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;

namespace xFunc.Maths.Tokens
{

    /// <summary>
    /// Represents a user-function token.
    /// </summary>
    public class UserFunctionToken : IToken
    {

        private string function;
        private int countOfParams;

        /// <summary>
        /// Initializes the <see cref="UserFunctionToken"/> class.
        /// </summary>
        /// <param name="variable">A name of function.</param>
        public UserFunctionToken(string function) : this(function, -1) { }

        /// <summary>
        /// Initializes the <see cref="UserFunctionToken"/> class.
        /// </summary>
        /// <param name="variable">A name of function.</param>
        /// <param name="countOfParams">A count of parameters.</param>
        public UserFunctionToken(string function, int countOfParams)
        {
            this.function = function;
            this.countOfParams = countOfParams;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            UserFunctionToken token = obj as UserFunctionToken;
            if (token != null && this.Function == token.Function && this.countOfParams == token.CountOfParams)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "User Function: " + function;
        }

        /// <summary>
        /// Gets a priority of current token.
        /// </summary>
        public int Priority
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// Gets the name of function.
        /// </summary>
        public string Function
        {
            get
            {
                return function;
            }
        }

        /// <summary>
        /// The count of parameters.
        /// </summary>
        public int CountOfParams
        {
            get
            {
                return countOfParams;
            }
            set
            {
                countOfParams = value;
            }
        }

    }

}