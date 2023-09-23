// <copyright file="DatabaseSettings.cs" company="SalviePl">
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
/// The configuration settings for a mongo db database.
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// Gets or sets the database connection host.
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the database connection port.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the database requires authentication.
    /// </summary>
    public bool UsesAuthentication { get; set; }

    /// <summary>
    /// Gets or sets the authentication type.
    /// </summary>
    public string AuthenticationType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username for authentication.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user password for authentication.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether insecure TLS connections are allowed.
    /// </summary>
    public bool AllowInsecureTls { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether TLS should be enabled.
    /// </summary>
    public bool UseTls { get; set; }

    /// <summary>
    /// Gets or sets the authentication database name.
    /// </summary>
    public string AuthenticationDatabaseName { get; set; } = string.Empty;
}
