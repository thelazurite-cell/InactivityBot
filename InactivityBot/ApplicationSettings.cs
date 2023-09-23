// <copyright file="ApplicationSettings.cs" company="SalviePl">
// MIT License
//
// Copyright (c) 2023 Salvie Pluviem
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>

namespace InactivityBot;

/// <summary>
/// The application settings class.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    ///  Gets or sets the database connection configuration.
    /// </summary>
    public DatabaseSettings Database { get; set; } = new();

    /// <summary>
    /// Gets or sets the general settings.
    /// </summary>
    public GeneralConfiguration General { get; set; } = new();
}

/// <summary>
/// The general configuration.
/// </summary>
public class GeneralConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    public string DtoDllName {get;set;} = string.Empty;
}